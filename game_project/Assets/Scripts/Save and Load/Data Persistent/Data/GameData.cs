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
    public float masterVolume ;
    public float musicVolume ;
    public float ambienceVolume ;
    public float SFXVolume ;
    public bool isFinishedAbelintro ; 
    public bool isDestroyintroAbel ; 
    public GameData(){
        isDestroyintroAbel = false ;
        isFinishedAbelintro = false; 
        this.deathCount  = 0;
        masterVolume = 1 ;
        musicVolume = 1 ;
        ambienceVolume = 1 ;
        SFXVolume = 1 ;
        playerPosition =new Vector2 (-50.8f,-2.1f);
        ItemCollected = new SerializableDictionary<string,bool>();
    }
  
}
