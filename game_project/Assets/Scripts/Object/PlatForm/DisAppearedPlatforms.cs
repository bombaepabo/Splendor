using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisAppearedPlatforms : MonoBehaviour
{
     public float TimeToTogglePlatform =2 ;
    public float currentTime = 0 ; 
    private bool enable = true ; 
    // Start is called before the first frame update
    void Start()
    {
        enable = true ; 
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
        enable = !enable ; 
        foreach(Transform child in gameObject.transform)
        {
            if(child.tag !="Player")
            {
            child.gameObject.SetActive(enable);
            }
        }
    }
}
