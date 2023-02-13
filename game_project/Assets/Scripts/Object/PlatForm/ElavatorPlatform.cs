using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElavatorPlatform : MonoBehaviour
{
    public Transform posA,posB ;
    public float speed ;
    public float ForceBoost = 1000.0f; 
    Vector3 targetPos; 
    Player player ; 
    Vector3 moveDirection;
    float time ; 
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void Start()
    {
        targetPos = posB.position ; 
        DirectionCalculate();
    }

    // Update is called once per frame
    private void Update()
    {
        
        if(player.isOnPlatform){
            time += Time.fixedDeltaTime ;
            if(player.wallGrabState.IsWallGrab |player.wallClimbState.IsWallClimb|player.wallSlideState.IsWallSlide && time<2f)
                {
                transform.position = Vector2.MoveTowards (transform.position,posB.transform.position , 15f * Time.deltaTime);
                if(time >2f){
                    transform.position = Vector2.MoveTowards (transform.position,posA.transform.position , 2f * Time.deltaTime);
                    time = 0 ;
                }
                 if(player.JumpState.isJumping && this.transform.position == posB.transform.position){
                player.RB.AddForce(new Vector2(ForceBoost, 0));
                Debug.Log("Enter");
            }
                
                }    
            else{
                transform.position = Vector2.MoveTowards (transform.position,posA.transform.position , 2f * Time.deltaTime);
                time = 0 ;

                }
        }
        else{
            transform.position = Vector2.MoveTowards (transform.position,posA.transform.position , 2f * Time.deltaTime);
            time = 0 ;

        }
    }
    
    private void FixedUpdate(){
         
    }
    void DirectionCalculate(){
        moveDirection = (targetPos - transform.position).normalized;
    }
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.name.Equals("Player")){
            player.isOnPlatform = true ;
                if(player.wallGrabState.IsWallGrab |player.wallClimbState.IsWallClimb|player.wallSlideState.IsWallSlide&&player.isOnPlatform ){
                    collision.gameObject.transform.parent =this.transform ; 
                }
            
          
        }
    }
    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.name.Equals("Player")){
           
            player.isOnPlatform = false ;
            collision.transform.parent =null ; 

        }

    }
    IEnumerator PlatformBack(float delayTime){
        yield return new WaitForSeconds(delayTime);

        transform.position = Vector2.MoveTowards (transform.position,posA.transform.position , 2f * Time.deltaTime);

    }
    IEnumerator DelayAction(float delayTime){
        yield return new WaitForSeconds(delayTime);
    }
   
    
}
