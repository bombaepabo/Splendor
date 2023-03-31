using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Controller : MonoBehaviour
{
        private Animator _anim ; 
        public Rigidbody2D RB {get; private set ;}
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(RB.velocity.x >1f | RB.velocity.x <-1f){
            _anim.SetBool("move",true);
        }
        else{
            _anim.SetBool("move",false);
        }
    }
}
