using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChangeTriggered : MonoBehaviour
{
   [Header("Area")]
   [SerializeField] private MusicArea area ;

   private void OnTriggerEnter2D(Collider2D collider){
    if(collider.tag.Equals("Player")){
        AudioManager.instance.SetMusicAreaParameter(area);
   }
   }
}
