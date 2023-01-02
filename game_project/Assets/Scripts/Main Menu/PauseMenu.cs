using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PauseMenu : Menu
{
  private Player player ;
    [SerializeField] private GameObject PauseMenuUI ;
    [Header("Menu Buttoms")]
    [SerializeField] private Button ResumeButton ; 
    [SerializeField] private Button BackToMenuButton ;
    [SerializeField] private Button ExitButton ;
    public static bool IsPaused = false ; 
    public void Start(){
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            Time.timeScale = 1 ; 

    }
    public void Update(){
            if(PauseMenu.IsPaused){
                ActivateMenu();
                Time.timeScale = 0 ; 
            }
            else{
                Time.timeScale = 1 ; 
                DeactivateMenu();
                
            }
    }
    public void OnResumeClicked(){
        IsPaused = false ;
        Debug.Log("Press resume button" + "Is Paused" + IsPaused);
        DeactivateMenu();
    }
    public void OnBacktoMenuClicked(){
      Debug.Log(IsPaused);
        IsPaused = false ;

        DeactivateMenu();
        SceneManager.LoadSceneAsync("MainMenu");
    }
    public void OnExitClicked(){
        Application.Quit();

    }
    public void ActivateMenu(){
    PauseMenuUI.SetActive(true);

  }
  public void DeactivateMenu(){
    PauseMenuUI.SetActive(false);
  }
  
}
