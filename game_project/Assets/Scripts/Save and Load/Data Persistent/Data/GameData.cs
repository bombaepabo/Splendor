using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData 
{
    public long lastUpdated ;
    public int deathCount ; 
    public Vector3 playerPosition ; 
    public SerializableDictionary<string,bool> ItemCollected;
    public GameData(){
        this.deathCount  = 0;
        playerPosition = Vector3.zero;
        ItemCollected = new SerializableDictionary<string,bool>();
    }
  
}
