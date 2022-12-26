using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePlatForm : MonoBehaviour
{
    public float timetoEnabled = 3f ; 
    public float TimetoDisabled = 0.5f ;
    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.name.Equals ("Player")) {
			Invoke ("OnDisablePlatform", TimetoDisabled);
		}
	}
    void OnDisablePlatform(){
        this.gameObject.SetActive(false);
        Invoke("OnEnablePlatform", timetoEnabled);

    }
    void OnEnablePlatform(){
        this.gameObject.SetActive(true);
    }

   
    
}
