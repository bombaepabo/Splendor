using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding ; 
public class EnemyAi : MonoBehaviour
{
    public Transform target ;
    public float speed = 200f ;
    public float nextWaypointDistance = 3f ;
    public Transform enemyGFX ; 
    Path path ;
    Player player ; 
    int currentWaypoint = 0 ;
    bool reachedEndOfPath = false ;
    Seeker seeker ; 
    Rigidbody2D rb ; 
    private Vector2 initspawnEnemy ; 
    private float timer = 0 ; 
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        InvokeRepeating("UpdatePath",0f,.1f);
        
    }
    void UpdatePath(){
        if(seeker.IsDone()){
        seeker.StartPath(rb.position,target.position,OnPathComplete);
        }
    }
    void OnPathComplete(Path p ){
        if(!p.error){
            path = p ; 
            currentWaypoint = 0 ; 
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(path ==null){
            return;
        }
        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true ;
            return ;
        }
        else {
            reachedEndOfPath = false ;
        }
        if(player.DeathState.isDead){
            timer += Time.fixedDeltaTime;
        }
        
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized ;
        Vector2 force = direction * speed * Time.deltaTime ;
        rb.AddForce(force);
        float distance = Vector2.Distance(rb.position,path.vectorPath[currentWaypoint]);
        if(distance < nextWaypointDistance){
            currentWaypoint++ ;
        }
        if(force.x >= 0.01f){
            enemyGFX.localScale = new Vector3(-1f,1f,1f);
        }
        else if(force.x <= -0.01f)
        {
            enemyGFX.localScale = new Vector3(1f,1f,1f);
        }        
    }
      private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player"){
              player.playerData.CurrentHealth -= 100 ;
            GameEventsManager.instance.PlayerDeath();
        }
    }
        
    }
