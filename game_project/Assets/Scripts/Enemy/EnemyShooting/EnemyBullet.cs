using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
        Player player ; 
        Rigidbody2D rb ; 
        public float force ; 
        private float timer ;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x,direction.y).normalized *force ;

        float rot =Mathf.Atan2(-direction.y,-direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,rot +90);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime ;
        if(timer > 10){
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.CompareTag("Player")){
            player.playerData.CurrentHealth -= 100 ;
            GameEventsManager.instance.PlayerDeath();
            Destroy(gameObject);

        }
    }
}
