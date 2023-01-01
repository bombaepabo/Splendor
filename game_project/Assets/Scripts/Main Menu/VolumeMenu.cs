using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VolumeMenu : MonoBehaviour
{
     [Header("Menu Button")]
     [SerializeField] private GameObject firstSelected;

    [SerializeField] private OptionsMenu optionsMenu ;
    // Start is called before the first frame update
   
    protected virtual void OnEnable(){
        EventSystem.current.SetSelectedGameObject(firstSelected);
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
}
