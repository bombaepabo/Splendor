using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImageSprite : MonoBehaviour
{
    [SerializeField]
    private float activeTime = 0.1f;
    private float timeActivated;
    private float alpha;
    [SerializeField]
    private float alphaSet = 0.8f;
    [SerializeField]
    private float alphaDecay = 0.85f;
    private Player player ; 
    private TrailRenderer SR;
    //private SpriteRenderer SR;
   // private SpriteRenderer playerSR;

    private Color color;

    private void OnEnable()
    {
        SR = GetComponent<TrailRenderer>();
        //SR = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
       // playerSR = player.GetComponent<SpriteRenderer>();
        alpha = alphaSet;
        //SR.sprite = playerSR.sprite;
        transform.position = player.transform.position;
        transform.rotation = player.transform.rotation;
        timeActivated = Time.time;
    }

    private void Update()
    {
        //alpha -= alphaDecay * Time.deltaTime;
        //color = new Color(1f, 1f, 1f, alpha);
       //SR.color = color;
        if(player.inputhandler.GetDashInput()){
            SR.emitting = true ;
        }
        if(Time.time >= (timeActivated + activeTime))
        {
            PlayerAfterImagePool.Instance.AddToPool(gameObject);
        }

    }
}
