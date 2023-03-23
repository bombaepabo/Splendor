using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour,IDataPersistent
{
    // Start is called before the first frame update
    private Animator _anim ; 
    private Player player ; 
    [SerializeField] private Key key ;
    private bool collected = false ; 
    [SerializeField] private string id ;
    [ContextMenu("Generate guid for id")]
    private void GenerateGuid(){
        id = System.Guid.NewGuid().ToString();
    }
     public void LoadData(GameData data){
        data.DoorOpen.TryGetValue(id,out collected);
        Debug.Log(data.DoorOpen.TryGetValue(id,out collected));
        if(collected){
            Open();
        }
    }
    public void SaveData(GameData data){
        if(data.DoorOpen.ContainsKey(id)){
            data.DoorOpen.Remove(id);
        }
        data.DoorOpen.Add(id,collected);
    }
    private void Awake(){
        _anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    [ContextMenu("Open")]
    public void Open(){
        _anim.SetTrigger("Open");
    }
    private void OnTriggerEnter2D(Collider2D other){
         if(other.gameObject.name.Equals("Player")){
            if(player.FollowingKey !=null){
                player.FollowingKey.followTarget = transform;
                player.FollowingKey = null ;
                key.collected = true;
                Debug.Log(player.FollowingKey);
                collected = true;
                Open();

            }
            }
        }
    }
