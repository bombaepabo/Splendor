using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashReset : MonoBehaviour
{
    Player player;
    private SpriteRenderer visual;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        visual = this.GetComponentInChildren<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D Collider)
    {
        if (Collider.gameObject.name.Equals("Player"))
        {
            player.DashState.ResetCanDash();
            visual.gameObject.SetActive(false);
            Invoke("GetDashResetBack", 5f);
        }
    }

    void GetDashResetBack()
    {
        visual.gameObject.SetActive(true);
    }
}

