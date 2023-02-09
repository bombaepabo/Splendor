using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn_manage : MonoBehaviour
{
     void OnTriggerEnter2D(Collider2D other){
        if(other.transform.tag == "BGtransition"){
            for(int i=0; i< other.transform.childCount; i++) {
                other.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.transform.tag == "BGtransition"){
            for(int i=0 ; i< other.transform.childCount; i++) {
                other.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}