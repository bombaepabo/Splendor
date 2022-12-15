using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SaveSlotsMenu : Menu
{
    [Header("Menu Navigation")]
    [SerializeField] private MainMenu mainMenu ;
    [Header("Menu Button")]
    [SerializeField] private Button backButton ;
    private SaveSlot[] saveSlots ; 
    private bool isLoadingGame = false ;
    private void Awake(){
    saveSlots = this.GetComponentsInChildren<SaveSlot>();
   }
  public void OnBackClicked(){
    mainMenu.ActivateMenu();
    this.DeactivateMenu();
  }
  public void OnSaveSlotClicked(SaveSlot saveSlot){
    DisableMenuButtons();

    DataPersistentManager.instance.ChangedSelectedProfileId(saveSlot.GetProfileId());

    if(!isLoadingGame){
        DataPersistentManager.instance.NewGame();
    }

    SceneManager.LoadSceneAsync("Test Scene3");
  }
   public void ActivateMenu(bool isLoadingGame){
    this.gameObject.SetActive(true);

    this.isLoadingGame = isLoadingGame ;
    Dictionary<string,GameData> profilesGameData = DataPersistentManager.instance.GetAllProfilesGameData();
    GameObject firstSelected = backButton.gameObject;
    foreach(SaveSlot saveSlot in saveSlots){
        GameData profileData = null ; 
        profilesGameData.TryGetValue(saveSlot.GetProfileId(), out profileData);
        saveSlot.SetData(profileData);
        if(profileData == null &isLoadingGame)
        {
            saveSlot.SetInteractable(false);
        }
        else{
            saveSlot.SetInteractable(true);
            if(firstSelected.Equals(backButton.gameObject))
            {
                firstSelected = saveSlot.gameObject;
            }
        }
    }
    StartCoroutine(this.SetFirstSelected(firstSelected));
   }
    public void DeactivateMenu(){
    this.gameObject.SetActive(false);
  }
  private void DisableMenuButtons(){
    foreach (SaveSlot saveSlot in saveSlots){
        saveSlot.SetInteractable(false);
    }
    backButton.interactable = false ;
  }
}
