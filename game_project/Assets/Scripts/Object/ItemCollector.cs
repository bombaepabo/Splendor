using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCollector : MonoBehaviour

{
    private int collectedCount = 0 ; 
    [SerializeField]
    private TMP_Text collectedtext ; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Collectable"))
        {
            Destroy(collision.gameObject);
            collectedCount = collectedCount + 1 ;
            collectedtext.text  = "Collected : " + collectedCount ; 
        }
    }
}
