using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.04f ; 

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText ;
    [SerializeField] private GameObject continueIcon ; 
    [SerializeField] private Animator layoutAnimator;
    [SerializeField] private Animator portraitAnimator; 

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;
    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }
    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";
    private Player player ; 
    private static DialogueManager instance;
    private Coroutine displayLineCoroutine ;
    private bool canContinueToNextLine = false  ;
    private DialogueVariables dialogueVariables ; 
    

    private void Awake() 
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
        dialogueVariables = new DialogueVariables(loadGlobalsJSON); 

    }

    public static DialogueManager GetInstance() 
    {
        return instance;
    }

    private void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        layoutAnimator = dialoguePanel.GetComponent<Animator>();
        // get all of the choices text 
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices) 
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update() 
    {
        // return right away if dialogue isn't playing
        if (!dialogueIsPlaying) 
        {
            return;
        }

        // handle continuing to the next line in the dialogue when submit is pressed
        // NOTE: The 'currentStory.currentChoiecs.Count == 0' part was to fix a bug after the Youtube video was made
        if (canContinueToNextLine && currentStory.currentChoices.Count == 0 && player.inputhandler.GetSubmitPressed())
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON) 
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        dialogueVariables.StartListening(currentStory);
        ContinueStory();
    }

    private IEnumerator ExitDialogueMode() 
    {
        yield return new WaitForSeconds(0.01f);
        dialogueVariables.StopListening(currentStory);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory() 
    {
        if (currentStory.canContinue) 
        {
            if(displayLineCoroutine !=null){
                StopCoroutine(displayLineCoroutine);
            }
            // set text for the current dialogue line
            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));
            // display choices, if any, for this dialogue line
            HandleTags(currentStory.currentTags);
        }
        else 
        {
            StartCoroutine(ExitDialogueMode());
        }
    }
    private IEnumerator DisplayLine(string line){

        dialogueText.text = line; 
        dialogueText.maxVisibleCharacters = 0;
        continueIcon.SetActive(false);
        HideChoices();
        canContinueToNextLine = false ;
        bool isAddingRichTextTag = false ;

        foreach(char letter in line.ToCharArray()){
            if(player.inputhandler.GetSubmitPressed()){
                dialogueText.maxVisibleCharacters = line.Length ; 
                break ; 
            }
            if( letter =='<' ||isAddingRichTextTag){
                isAddingRichTextTag = true ; 
                //dialogueText.text += letter ;
                if(letter == '>'){
                    isAddingRichTextTag = false ;
                }
            }
            else{
            dialogueText.maxVisibleCharacters++;
            yield return new WaitForSeconds(typingSpeed);
            }
            
        }
        continueIcon.SetActive(true);
        DisplayChoices();

        canContinueToNextLine = true ; 

    }
    private void HideChoices(){
        foreach (GameObject choiceButton in choices){
            choiceButton.SetActive(false);
        }
    }
    private void HandleTags(List<string> currentTags){
        foreach(string tag in currentTags){
            string[] splitTag = tag.Split(':');

            if(splitTag.Length !=2){
                Debug.LogError("Tag cound not be appropriately parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim() ;
            string tagValue = splitTag[1].Trim();

            switch(tagKey){
                case SPEAKER_TAG:
                    displayNameText.text = tagValue ; 
                    break ; 
                case PORTRAIT_TAG :
                    portraitAnimator.Play(tagValue);
                    Debug.Log("PORTRAIT = "+ tagValue);
                    break ;
                case LAYOUT_TAG :
                    layoutAnimator.Play(tagValue);
                    break ; 
                default : 
                    Debug.LogWarning("Tag came in but is not currently being handled : " + tag);
                    break ;
            } 
        }
    }

    private void DisplayChoices() 
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // defensive check to make sure our UI can support the number of choices coming in
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: " 
                + currentChoices.Count);
        }

        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach(Choice choice in currentChoices) 
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choices.Length; i++) 
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice() 
    {
        // Event System requires we clear it first, then wait
        // for at least one frame before we set the current selected object.
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        if(canContinueToNextLine){
        currentStory.ChooseChoiceIndex(choiceIndex);
        // NOTE: The below two lines were added to fix a bug after the Youtube video was made
        player.inputhandler.registerSubmitPressed() ;
        ContinueStory();
        }
    }
    public Ink.Runtime.Object GetVariableState(string variableName){
        Ink.Runtime.Object variableValue = null ; 
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if(variableValue ==null){
            Debug.LogWarning("Ink variable was found to be null : " + variableName);
        }
        return variableValue ; 
    }

}