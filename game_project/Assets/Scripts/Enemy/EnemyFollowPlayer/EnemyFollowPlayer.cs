using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    public float speed;
    Player player ; 
    public float lineofsight;
    Vector3 initialposition;
    public float currentTime = 0f ; 
    // Start is called before the first frame update
    void Start()
    {
        initialposition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(player.transform.position,transform.position);
        if(distance < lineofsight){
            transform.position = Vector2.MoveTowards(this.transform.position,player.transform.position,speed *Time.deltaTime);

        }
        if(player.playerData.CurrentHealth <=0 ){
            currentTime += Time.fixedDeltaTime;       
            if(currentTime >=4.5f){
            transform.position = initialposition;

            }
        }
    }

    private void OnDrawGizmosSelected(){
        Gizmos.color =Color.green ;
        Gizmos.DrawWireSphere(transform.position,lineofsight); 
    }
}
