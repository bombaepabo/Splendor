using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
public class VideoSetting : Menu
{
     [Header("Menu Button")]
    [SerializeField] private OptionsMenu optionsMenu ;
    [SerializeField] public Toggle fullscreentog, VsyncTog ;
    // Start is called before the first frame update
     private void Awake(){
   
   }
   private void Start(){
    fullscreentog.isOn = Screen.fullScreen ;
    if(QualitySettings.vSyncCount == 0 ){
        VsyncTog.isOn = false ;
    }else{
        VsyncTog.isOn = true ; 
    }
   }

    public void OnBackClicked(){
    optionsMenu.ActivateMenu(true);
    this.DeactivateMenu();
    }
    public void ActivateMenu(bool isLoadingGame){
    this.gameObject.SetActive(true);
    
    }
    public void DeactivateMenu(){
    this.gameObject.SetActive(false);
    }
    public void ApplyGraphics()
    {
        Screen.fullScreen = fullscreentog.isOn ; 

        if(VsyncTog.isOn){
            QualitySettings.vSyncCount = 1 ;
        }
        else 
        {
            QualitySettings.vSyncCount = 0 ;
        }
    }
}
