using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData 
{
    public long lastUpdated ;
    public int deathCount ; 
    public Vector3 playerPosition ; 
    public Vector3 EnemySpawnPosition;
    public SerializableDictionary<string,bool> ItemCollected;
    public float masterVolume ;
    public float musicVolume ;
    public float ambienceVolume ;
    public float SFXVolume ;
    public bool isFinishedAbelintro ; 
    public bool isFinished2 ;
    public bool isDestroyintroAbel ; 
    public bool HanCampisFinished ; 
    public bool FinHelpingFinished ; 
    public bool chapter2puzzleFinished ;
    public bool sceneChapter2_4isFinished ; 
    public bool NormanQuiz2isFinished ; 
    public bool NormanQuiz3isFinished ;
    public bool NormanQuiz4isFinished ;
    public bool chapter3_puzzle1;
    public bool scene36_isFinished ; 
    public bool scene5to9isFinished ;
    public bool scene14isFinished;
    public bool scene16isFinished;
    public bool scene17isFinished;
    public bool scene19isFinished;
    public bool sceneChapter2_3isFinished;
    public bool scene20isFinished;
    public bool scene29isFinished;
    public bool scene32isFinished;
    public bool scene39isFinished;
    public bool scene41isFinished;
    public bool enableDash ;
    public bool collectedDoor ; 
    public SerializableDictionary<string,bool> Keycollected;
    public SerializableDictionary<string,bool> DoorOpen;

    public GameData(){
        NormanQuiz4isFinished = false ;
        NormanQuiz3isFinished = false;
        collectedDoor = false;
        enableDash = false ;
        scene41isFinished = false ;
        scene39isFinished = false ;
        scene29isFinished = false;
        scene20isFinished = false;
        sceneChapter2_3isFinished = false;
        scene19isFinished = false ;
        scene17isFinished = false ;
        scene16isFinished = false ;
        scene14isFinished = false;
        scene5to9isFinished = false ;
        scene36_isFinished = false ;
        chapter3_puzzle1 = false ;
        NormanQuiz2isFinished = false ; 
        sceneChapter2_4isFinished = false ;
        chapter2puzzleFinished = false ;
        isDestroyintroAbel = false ;
        isFinished2 = false;
        isFinishedAbelintro = false; 
        HanCampisFinished = false; 
        FinHelpingFinished = false ;
        this.deathCount  = 0;
        masterVolume = 1 ;
        musicVolume = 1 ;
        ambienceVolume = 1 ;
        SFXVolume = 1 ;
        playerPosition =new Vector2 (-129.73f,-2.1f);
        ItemCollected = new SerializableDictionary<string,bool>();
        Keycollected = new SerializableDictionary<string,bool>();
        DoorOpen = new SerializableDictionary<string,bool>();
        EnemySpawnPosition = new Vector2(72.87f,-17.17f);
    }
  
}
