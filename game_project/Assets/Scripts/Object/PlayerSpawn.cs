using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
 private PlayerData playerData ;
 [SerializeField]
    private Transform SpawnPoint ; 
 [SerializeField]
    private GameObject Player ; 
  private void OnCollisionEnter2D(Collision2D Collision){
    if(Collision.transform.CompareTag("Player")){
        Player.transform.position = SpawnPoint.position ;
            Debug.Log(Player.transform);
  }
}
}
