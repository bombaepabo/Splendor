using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class FModEvent : MonoBehaviour
{
    [field: Header("Ambience")]
    [field: SerializeField] public EventReference Ambience {get ; private set ;}

    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference playerFootsteps {get ; private set ;}

    [field: Header("Music")]
    [field: SerializeField] public EventReference Music {get ; private set ;}

    [field: Header("Coin SFX")]
    [field: SerializeField] public EventReference CoinCollected{get;private set;}
    [field: SerializeField] public EventReference CoinIdle{get;private set;}

    public static FModEvent instance{get ;private set;}

    private void Awake(){
        if(instance != null){
            Debug.LogError("Found more than one FMod Events Script in the scene.");

        }
        instance = this ; 

    }

}
