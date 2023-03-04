using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SaveSlotsMenu : Menu
{
    [Header("Menu Navigation")]
    [SerializeField] private PlayGameMenu mainMenu ;
    [Header("Menu Button")]
    [SerializeField] private Button backButton ;
    [Header("Confirmation Popup")]
    [SerializeField] private ConfirmationPopupMenu confirmationPopupMenu ; 
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

    if(isLoadingGame){
      DataPersistentManager.instance.ChangedSelectedProfileId(saveSlot.GetProfileId());
      SaveGameAndLoadScene();
    }
    else if(saveSlot.hasData == true){
      confirmationPopupMenu.ActivateMenu("Starting a New Game with this slot will override the currently saved data. Are you sure?",
      () =>{
          DataPersistentManager.instance.ChangedSelectedProfileId(saveSlot.GetProfileId());
          DataPersistentManager.instance.NewGame();
          SaveGameAndLoadScene();
      },
      () =>{
        this.ActivateMenu(isLoadingGame);
      }
      );
    }
    else{
          DataPersistentManager.instance.ChangedSelectedProfileId(saveSlot.GetProfileId());
          DataPersistentManager.instance.NewGame();
          SaveGameAndLoadScene();
    }

  }
  private void SaveGameAndLoadScene(){
    DataPersistentManager.instance.SaveGame();

    SceneManager.LoadSceneAsync("Intro Scene");
  }
   public void ActivateMenu(bool isLoadingGame){
    this.gameObject.SetActive(true);

    this.isLoadingGame = isLoadingGame ;
    Dictionary<string,GameData> profilesGameData = DataPersistentManager.instance.GetAllProfilesGameData();

    backButton.interactable = true ;
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
    Button firstSelectedButton = firstSelected.GetComponent<Button>();
    this.SetFirstSelected(firstSelectedButton);
   }
   public void OnDeleteClicked(SaveSlot saveSlot){
    DisableMenuButtons();
    confirmationPopupMenu.ActivateMenu("Are you sure you want to delete this saved data ? ",
    () => {
    DataPersistentManager.instance.DeleteProfileData(saveSlot.GetProfileId());
    ActivateMenu(isLoadingGame);
    },
    () => {
    ActivateMenu(isLoadingGame);

    }
    );
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
