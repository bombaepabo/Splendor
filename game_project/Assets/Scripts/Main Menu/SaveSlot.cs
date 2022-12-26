using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro ; 
using UnityEngine.UI;
public class SaveSlot : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private string profileId = "";
    [Header("Content")]
    [SerializeField] private GameObject noDataContent ; 
    [SerializeField] private GameObject hasDataContent ; 
    [SerializeField] private TextMeshProUGUI deathCountText ; 
    [Header("Delete Button")]
    [SerializeField] private Button DelButton ; 
    private Button SaveSlotButton;
    public bool hasData {get;private set;} = false  ;
    private void Awake(){
       SaveSlotButton = this.GetComponent<Button>();
    }
    public void SetData(GameData data){
        Debug.Log(data);
        if(data == null){
            hasData = false;
            noDataContent.SetActive(true);
            hasDataContent.SetActive(false);
            DelButton.gameObject.SetActive(false);
        }
        else{
            hasData = true ;
            noDataContent.SetActive(false);
            hasDataContent.SetActive(true);
            DelButton.gameObject.SetActive(true);

            deathCountText.text = "DEATH COUNT: " + data.deathCount;
        }
    }
    public string GetProfileId(){
        return this.profileId;
    }
    public void SetInteractable(bool interactable){
        SaveSlotButton.interactable = interactable ; 
        DelButton.interactable = interactable ; 

    }
}
