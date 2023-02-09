using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events ; 
public class CollisionCheck : MonoBehaviour
{
    [SerializeField]
    private string _colliderScript ; 
    [SerializeField]
    private UnityEvent _collisionEntered ; 
    [SerializeField]
    private UnityEvent _collisionExit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.GetComponent(_colliderScript))
        {
            _collisionEntered?.Invoke();
        }
        
    }
      private void OnCollisionExit2D(Collision2D col){
        if(col.gameObject.GetComponent(_colliderScript))
        {
            _collisionExit?.Invoke();
        }
        
    }
}
