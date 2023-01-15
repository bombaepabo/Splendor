using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class OptionsMenu : Menu
{
    [Header("Menu Button")]
    [SerializeField] private MainMenu mainMenu ;
    [SerializeField] private VideoSetting videosettingmenu ;
    [SerializeField] private VolumeMenu volumemenu  ;
    [Header("Menu Buttoms")]
    [SerializeField] private Button VolumeButton ; 
    [SerializeField] private Button VideoSettingButton ;
    [SerializeField] private Button backButton ;
    private void Start(){

    }
    public void OnBackClicked(){
    mainMenu.ActivateMenu();
    this.DeactivateMenu();
    }
    public void OnVideoSettingClick(){
    videosettingmenu.ActivateMenu(true);
    this.DeactivateMenu();

   }
   public void OnVolumeMenuClick(){
    volumemenu.ActivateMenu(true);
    this.DeactivateMenu();
   }
    public void ActivateMenu(bool isLoadingGame){
    this.gameObject.SetActive(true);
    }
    public void DeactivateMenu(){
    this.gameObject.SetActive(false);
    }
}
