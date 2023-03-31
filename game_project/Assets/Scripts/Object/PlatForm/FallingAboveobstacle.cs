using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingAboveobstacle : MonoBehaviour
{
    	Rigidbody2D rb;
		Vector2 initialPosition;
		private Player player ;

		bool platformMovingBack;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		initialPosition = transform.position;
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

	}
	void Update()
	{
			if (platformMovingBack)
				transform.position = Vector2.MoveTowards (transform.position, initialPosition, 20f * Time.deltaTime);
		
		if (transform.position.y == initialPosition.y)
			platformMovingBack = false;
		if(player.DeathState.CheckIfisDead()){
			GetPlatformBack();
		}
	}
	
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.name.Equals("Player"))
			Invoke ("DropPlatform", 0.5f);
			Invoke ("GetPlatformBack", 10f);



	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.name.Equals("Player")){
			
		}
	}
	void DropPlatform()
	{
		rb.isKinematic = false;
	}
	void GetPlatformBack()
	{
		rb.velocity = Vector2.zero;
		rb.isKinematic = true;
		platformMovingBack = true;
	}

}
