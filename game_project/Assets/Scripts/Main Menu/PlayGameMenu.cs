using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayGameMenu : Menu
{
    [Header("Menu Navigation")]
    [SerializeField] private SaveSlotsMenu saveSlotsMenu ; 
    [Header("Menu Buttoms")]
    [SerializeField] private Button newGameButton ; 
    [SerializeField] private Button continueGameButton;
    [SerializeField] private Button loadGameButton ;
    [SerializeField] private MainMenu mainMenu ;

  public void Start(){
    DisableButtonDependingOnData();
  }
  private void DisableButtonDependingOnData(){
    if(!DataPersistentManager.instance.HasGameData()){
        continueGameButton.interactable = false ;
        loadGameButton.interactable = false ;
    }
  }
  public void OnNewGameClicked()
  {
   saveSlotsMenu.ActivateMenu(false);
   this.DeactivateMenu();
  }
  public void OnLoadGameClicked()
  {
    saveSlotsMenu.ActivateMenu(true);
    this.DeactivateMenu();
  }
  public void OnContinueGameClicked()
  {
    DisableMenuButtons();
    DataPersistentManager.instance.SaveGame();
    SceneManager.LoadSceneAsync("Introduction");
  }
  private void DisableMenuButtons(){
    newGameButton.interactable = false ;
    continueGameButton.interactable = false ;
  }
  public void ActivateMenu(){
    this.gameObject.SetActive(true);
    DisableButtonDependingOnData();

  }
  public void DeactivateMenu(){
    this.gameObject.SetActive(false);
  }
   public void OnBackClicked(){
    mainMenu.ActivateMenu();
    this.DeactivateMenu();
    }
  
}
