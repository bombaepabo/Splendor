using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
using TMPro ; 
public class VideoSetting : Menu
{
     [Header("Menu Button")]
    [SerializeField] private OptionsMenu optionsMenu ;
    [SerializeField] public Toggle fullscreentog, VsyncTog ;
    [SerializeField] public List<ResItem> resolutionlist  = new List<ResItem>();
    public TMP_Text resolutionLabel ; 
    private int selectedResolution ; 
    // Start is called before the first frame update
     private void Awake(){
   
   }
   private void Start(){
    fullscreentog.isOn = Screen.fullScreen ;
    if(QualitySettings.vSyncCount == 0 ){
        VsyncTog.isOn = false ;
    }
    else{
        VsyncTog.isOn = true ; 
    }
    bool foundRes = false ;
    for(int i=0;i<resolutionlist.Count;i++){
        if(Screen.width == resolutionlist[i].horizontal && Screen.height == resolutionlist[i].vertical)
        {
            foundRes = true; 
            selectedResolution = i ;
            UpdateResolutionLabel();
        }
    }
    if(!foundRes){
        ResItem newRes = new ResItem();
        newRes.horizontal = Screen.width;
        newRes.vertical = Screen.height ;
        resolutionlist.Add(newRes);
        selectedResolution = resolutionlist.Count -1 ;
        UpdateResolutionLabel();
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
    public void ResLeft(){
        selectedResolution --;
        if(selectedResolution <= 0){
            selectedResolution = 0 ; 
        }
        UpdateResolutionLabel();
    }
    public void ResRight(){
        selectedResolution ++ ; 
        if(selectedResolution >= resolutionlist.Count -1){
            selectedResolution = resolutionlist.Count-1 ;
        }
        UpdateResolutionLabel();

    }
    public void UpdateResolutionLabel(){
        resolutionLabel.text = resolutionlist[selectedResolution].horizontal.ToString() +" x " + resolutionlist[selectedResolution].vertical.ToString(); 
    }
    
    public void ApplyGraphics()
    {
      //  Screen.fullScreen = fullscreentog.isOn ; 

        if(VsyncTog.isOn){
            QualitySettings.vSyncCount = 1 ;
        }
        else 
        {
            QualitySettings.vSyncCount = 0 ;
        }
        Screen.SetResolution(resolutionlist[selectedResolution].horizontal,resolutionlist[selectedResolution].vertical,fullscreentog.isOn);
    }
    
}
