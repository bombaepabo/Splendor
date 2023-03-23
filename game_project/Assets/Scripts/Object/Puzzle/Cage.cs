using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    [SerializeField] private GameObject Fin ;   
    [SerializeField] private GameObject cage ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Cage_Open(){
        //Fin.SpriteRenderer.
        cage.SetActive(false);
    }
}
