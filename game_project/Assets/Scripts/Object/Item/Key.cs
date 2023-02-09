using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private bool isFollowing ; 
    public float followSpeed; 
    public Transform followTarget ;
    private Player player ; 
    private Vector3 initialpoint ; 

    // Start is called before the first frame update
    void Awake(){
        initialpoint  = transform.position; 
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    // Update is called once per frame
    void Update()
    {
        if(isFollowing){
            transform.position = Vector3.Lerp(transform.position,followTarget.position,followSpeed * Time.deltaTime);
        }
        if(player.DeathState.isDead){
            isFollowing = false ;
            transform.position = Vector3.Lerp(transform.position,initialpoint,6 * Time.deltaTime);

            
        }
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            if(!isFollowing){
                Player player = FindObjectOfType<Player>();
                followTarget = player.Keyfollowpoint;
                isFollowing = true ; 
                player.FollowingKey = this ; 
            }
        }
    }
}
