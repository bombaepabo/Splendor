using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePlatForm : MonoBehaviour
{
    public float TimeToTogglePlatform =2 ;
    public float currentTime = 0 ; 
    public bool enabled = true ; 
    // Start is called before the first frame update
    void Start()
    {
        enabled = true ; 
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime ;
        if(currentTime >= TimeToTogglePlatform)
        {
            currentTime = 0 ;
            TogglingPlatForm();
        }
    }

    void TogglingPlatForm(){
        enabled = !enabled ; 
        foreach(Transform child in gameObject.transform)
        {
            if(child.tag !="Player")
            {
            child.gameObject.SetActive(enabled);
            }
        }
    }
}
