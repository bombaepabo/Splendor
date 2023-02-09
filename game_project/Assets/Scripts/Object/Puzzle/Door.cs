using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator _anim ; 
    private Player player ; 
    

    private void Awake(){
        _anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    [ContextMenu("Open")]
    public void Open(){
        _anim.SetTrigger("Open");
    }
    private void OnTriggerEnter2D(Collider2D other){
         if(other.gameObject.name.Equals("Player")){
            if(player.FollowingKey !=null){
                player.FollowingKey.followTarget = transform;

                Open();

            }
            }
        }
    }
