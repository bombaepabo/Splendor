using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : Menu
{
    [Header("Menu Navigation")]
    [SerializeField] private SaveSlotsMenu saveSlotsMenu ; 
    [Header("Menu Buttoms")]
    [SerializeField] private Button newGameButton ; 
    [SerializeField] private Button continueGameButton;
    [SerializeField] private Button loadGameButton ;
  public void Start(){
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
    SceneManager.LoadSceneAsync("Test Scene3");
  }
  private void DisableMenuButtons(){
    newGameButton.interactable = false ;
    continueGameButton.interactable = false ;
  }
  public void ActivateMenu(){
    this.gameObject.SetActive(true);
  }
  public void DeactivateMenu(){
    this.gameObject.SetActive(false);
  }
}
