using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : Menu
{
    [Header("Menu Navigation")]
    [SerializeField] private PlayGameMenu PlayGameMenu  ;
    [SerializeField] private OptionsMenu OptionsMenu ; 
    [Header("Menu Buttoms")]
    [SerializeField] private Button PlayGameButton ; 
    [SerializeField] private Button OptionsButton ;
    [SerializeField] private Button ExitButton ;
  public void Start(){
    
  }
  public void OnPlayGameClicked()
  {
   PlayGameMenu.ActivateMenu();
   this.DeactivateMenu();
  }
  public void OnOptionsClicked()
  {
    OptionsMenu.ActivateMenu(true);
    this.DeactivateMenu();
  }
  public void OnExitGameClicked()
  {
    Application.Quit();
  }
  private void DisableMenuButtons(){
  }
  public void ActivateMenu(){
    this.gameObject.SetActive(true);
    //DisableButtonDependingOnData();

  }
  public void DeactivateMenu(){
    this.gameObject.SetActive(false);
  }
}
