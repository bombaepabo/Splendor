using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    Player player ;
    private PlatformEffector2D effector ;
    public float waitTime ; 
    
    void Start(){
        effector = GetComponent<PlatformEffector2D>();

    }
    void Update(){
        
       

    }
}