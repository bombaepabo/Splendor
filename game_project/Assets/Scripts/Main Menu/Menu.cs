using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("First Selected Button")]
    [SerializeField] private Button firstSelected; 
    protected virtual void OnEnable(){
        SetFirstSelected(firstSelected);
    }
    public void SetFirstSelected(Button firstSelected){
        firstSelected.Select();
    }
}
