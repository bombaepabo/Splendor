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
    private Button SaveSlotButton;
    private void Awake(){
       SaveSlotButton = this.GetComponent<Button>();
    }
    public void SetData(GameData data){
        Debug.Log(data);
        if(data == null){
            noDataContent.SetActive(true);
            hasDataContent.SetActive(false);
        }
        else{
            noDataContent.SetActive(false);
            hasDataContent.SetActive(true);
            deathCountText.text = "DEATH COUNT: " + data.deathCount;
        }
    }
    public string GetProfileId(){
        return this.profileId;
    }
    public void SetInteractable(bool interactable){
        SaveSlotButton.interactable = interactable ; 
    }
}
