using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonNPC : MonoBehaviour
{
    [SerializeField] private Color defaultColor = Color.white ; 
    [SerializeField] private Color charmanderColor = Color.red ; 
    [SerializeField] private Color bulbasaurColor = Color.green ; 
    [SerializeField] private Color squirtleColor = Color.blue ; 

    private SpriteRenderer spriteRenderer ; 

    private void Start(){
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void Update()
    {
        string pokemonName = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("pokemon_name")).value ; 
        switch(pokemonName)
        {
            case "":
                spriteRenderer.color = defaultColor;
                break ; 
            case "Charmander" :
                spriteRenderer.color = Color.red ;
                break ;
            case "Bulbasaur" :
                spriteRenderer.color = Color.green ; 
                break ; 
            case "Squirtle" :
                spriteRenderer.color = Color.blue ; 
                break ; 
            default:
                Debug.LogWarning("Pokemon name not handled by switch statement: " + pokemonName);
                break;
        }
    }
}
