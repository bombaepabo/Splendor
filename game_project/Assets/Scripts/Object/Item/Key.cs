using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour,IDataPersistent
{
    private bool isFollowing ; 
    public float followSpeed; 
    public Transform followTarget ;
    private Player player ; 
    private Vector3 initialpoint ; 
    public bool collected = false ;
    private SpriteRenderer visual;

    [SerializeField] private string id ;
    [ContextMenu("Generate guid for id")]
    private void GenerateGuid(){
        id = System.Guid.NewGuid().ToString();
    }
    // Start is called before the first frame update
    void Awake(){
        visual = this.GetComponentInChildren<SpriteRenderer>();

        initialpoint  = transform.position; 
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    // Update is called once per frame
    void Update()
    {
        if(isFollowing){
            transform.position = Vector3.Lerp(transform.position,followTarget.position,followSpeed * Time.deltaTime);
        }
        if(player.DeathState.isDead){
            isFollowing = false ;
            transform.position = Vector3.Lerp(transform.position,initialpoint,6 * Time.deltaTime);

            
        }
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            if(!isFollowing){
                Player player = FindObjectOfType<Player>();
                followTarget = player.Keyfollowpoint;
                isFollowing = true ; 
                player.FollowingKey = this ; 
            }
        }
    }
    public void LoadData(GameData data){
        data.Keycollected.TryGetValue(id,out collected);
        Debug.Log(data.Keycollected.TryGetValue(id,out collected));
        if(collected){
            visual.gameObject.SetActive(false);

        }
    }
    public void SaveData(GameData data){
        if(data.Keycollected.ContainsKey(id)){
            data.Keycollected.Remove(id);
        }
        data.Keycollected.Add(id,collected);
    }
}
