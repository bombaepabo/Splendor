using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity ; 
[RequireComponent(typeof(StudioEventEmitter))]
public class ItemCollect : MonoBehaviour,IDataPersistent
{
    [SerializeField] private string id ;
    [ContextMenu("Generate guid for id")]
    private void GenerateGuid(){
        id = System.Guid.NewGuid().ToString();
    }
    private bool isFollowing ; 
    public float followSpeed; 
    public Transform followTarget ;
    private SpriteRenderer visual;
    private bool collected = false;
    private StudioEventEmitter emitter ;

    private void Start(){
        emitter = AudioManager.instance.InitializeEventEmitter(FModEvent.instance.CoinIdle,this.gameObject);
        emitter.Play();
    }
    public void Awake(){
        visual = this.GetComponentInChildren<SpriteRenderer>();
    }
    public void LoadData(GameData data){
        data.ItemCollected.TryGetValue(id,out collected);
        if(collected){
            visual.gameObject.SetActive(false);

        }
    }
    public void SaveData(GameData data){
        if(data.ItemCollected.ContainsKey(id)){
            data.ItemCollected.Remove(id);
        }
        data.ItemCollected.Add(id,collected);
    }
   

    // Update is called once per frame
    void Update()
    {
        if(isFollowing){
            transform.position = Vector3.Lerp(transform.position,followTarget.position,followSpeed * Time.deltaTime);
        }
        if(transform.position == followTarget.position){
            visual.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            if(!collected){
            if(!isFollowing){
                Player player = FindObjectOfType<Player>();
                followTarget = player.ItemCollectorfollowpoint;
                isFollowing = true ; 
                Collected();
            }
           
          
            }
        }
    }
    private void Collected(){
                collected = true ;
                visual.gameObject.SetActive(false);
                emitter.Stop();
                AudioManager.instance.PlayOneShot(FModEvent.instance.CoinCollected,this.transform.position);
                GameEventsManager.instance.CoinCollected();

    }
}