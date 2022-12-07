// This script is a Manager that controls the the flow and control of the game. It keeps
// track of player data (orb count, death count, total game time) and interfaces with
// the UI Manager. All game commands are issued through the static methods of this class

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;
public class GameManager : MonoBehaviour
{
	//This class holds a static reference to itself to ensure that there will only be
	//one in existence. This is often referred to as a "singleton" design pattern. Other
	//scripts access this one through its public static methods
	static GameManager current;
	public float deathSequenceDuration ;	//How long player death takes before restarting
	SceneFader sceneFader;						//The scene fader
	int numberOfDeaths;							//Number of times player has died
	float totalGameTime;						//Length of the total game time
	bool isGameOver;							//Is the game currently over?
	Vector3 spawnpoint ; 
	Player player ;  

	void Awake()
	{
		//If a Game Manager exists and this isn't it...
		if (current != null && current != this)
		{
			//...destroy this and exit. There can only be one Game Manager
			Destroy(gameObject);
			return;
		}

        player = GameObject.Find("Player").GetComponent("Player") as Player;

		//Set this as the current game manager
		current = this;
		//Create out collection to hold the orbs
		//Persis this object between scene reloads
		DontDestroyOnLoad(gameObject);
		
		}
	void Start(){
		Debug.Log("Start func");

	}
	
	void Update()
	{
		//If the game is over, exit
		if (isGameOver)
			return;

	}
	public static void RegisterPlayer(Player player){
				if (current == null)
			return;

		//Record the scene fader reference
		current.player = player;
	}


	public static void RegisterSceneFader(SceneFader fader)
	{
		//If there is no current Game Manager, exit
		if (current == null)
			return;

		//Record the scene fader reference
		current.sceneFader = fader;
	}
	
	public static void PlayerDied()
	{
		//If there is no current Game Manager, exit
		if (current == null)
			return;

		//Increment the number of player deaths and tell the UIManager
		current.numberOfDeaths++;
		//If we have a scene fader, tell it to fade the scene out
		if(current.sceneFader != null)
			current.sceneFader.FadeSceneOut();
		//Invoke the RestartScene() method after a delay
		current.Invoke("RestartScene", current.deathSequenceDuration);

		//current.Invoke("RespawnPlayer", current.deathSequenceDuration);
		Debug.Log(current.spawnpoint);
	}
	public static void RegisterSpawnPoint(Vector3 spawnpoint){
		if(current == null){
			return ; 
		}
		current.spawnpoint = spawnpoint + new Vector3(1,0,0) ; 
	}
	public static void RespawnPlayer(){
			if(current == null){
			return ; 
			}
			current.player.transform.position = current.spawnpoint ; 
	}
	void RestartScene()
	{
		//Clear the current list of orbs
		//Play the scene restart audio
		//Reload the current scene
		string currentScene = SceneManager.GetActiveScene ().name;
		LoadScene(currentScene);
		player.transform.position = current.spawnpoint ; 

	}
	public void LoadScene(string sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(string sceneId)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
		
        while(!operation.isDone)
        {
            yield return null;
        }


	  

    }
	
	
}
