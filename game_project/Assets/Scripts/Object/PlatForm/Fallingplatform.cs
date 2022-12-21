using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fallingplatform : MonoBehaviour
{
    private float fallDelay =0.2f; 
    private float destroyDelay = 2f; 
    [SerializeField] private Rigidbody2D rb;
   private void Awake(){
    rb = GetComponent<Rigidbody2D>();

   }
   private void OnCollisionEnter2D(Collision2D collision){
    if(collision.gameObject.CompareTag("Player")){
        StartCoroutine(Fall());
    }
   }
   private IEnumerator Fall()
   {
    yield return new WaitForSeconds(fallDelay);
    rb.bodyType = RigidbodyType2D.Dynamic;
    Destroy(gameObject,destroyDelay);
   }
}
