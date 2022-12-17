using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq ; 
using UnityEngine.SceneManagement;
public class DataPersistentManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool disableDataPersistent = false ;
    [SerializeField] private bool initializeDataIfNull = false ;
    [SerializeField] private bool overrideSelectedProfileId = false ;
    [SerializeField] private string testSelectedProfileId = "test";
    [Header("File storage config")]
    [SerializeField] private string filename ;
    [Header("Auto Saving Configuration")]
    [SerializeField] private float AutoSaveTimeSeconds =60f ; 
    public static DataPersistentManager instance {get;private set;}
    private GameData gameData ;
    private List<IDataPersistent> dataPersistentObject ;
    private FileDataHandler dataHandler ;
    private string selectedProfileId = "";
    private Coroutine autoSaveCoroutine;
    [SerializeField] private bool useEncryption ;
    private void Awake()
    {
    if(instance != null)
    {
        Debug.Log("Found more than one data persistence manager in the scene , Destroying the newest one ");   
        Destroy(this.gameObject);
        return ; 
    }
    instance = this ; 
    if(disableDataPersistent){
        Debug.LogWarning("Data Persistent is currently disabled !");
    }
    DontDestroyOnLoad(this.gameObject);
    this.dataHandler = new FileDataHandler(Application.persistentDataPath,filename,useEncryption);
    InitializeSelectedProfileId();
    }
    private void OnEnable(){
        SceneManager.sceneLoaded += OnSceneLoaded ; 
    }
    private void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded ; 
    }
    public void OnSceneLoaded(Scene scene,LoadSceneMode mode){
        Debug.Log("OnSceneLoaded Called");
        this.dataPersistentObject = FindAllDataPersistentObject();
        LoadGame();

        if(autoSaveCoroutine != null){
            StopCoroutine(autoSaveCoroutine);

        }
        autoSaveCoroutine = StartCoroutine(AutoSave());
    }
    public void ChangedSelectedProfileId(string newProfileId){
        this.selectedProfileId = newProfileId ; 
        LoadGame();
    }
    public void DeleteProfileData(string profileId){
        dataHandler.Delete(profileId);
        InitializeSelectedProfileId();
        LoadGame();
    }
    private void InitializeSelectedProfileId(){
    this.selectedProfileId = dataHandler.GetMostRecentlyUpdatedProfileId();
    if(overrideSelectedProfileId == true){
        this.selectedProfileId = testSelectedProfileId ; 
        Debug.LogWarning("Override selected profile id with test id: " + testSelectedProfileId);
    }
    }
    public void NewGame(){
        this.gameData = new GameData();
    }
    public void LoadGame(){
        if(disableDataPersistent){
            return ; 
        }
        this.gameData = dataHandler.Load(selectedProfileId);

        if(this.gameData == null && initializeDataIfNull){
            NewGame();
        }

        if(this.gameData == null){
          Debug.Log("No Data found, A New Game needs to be started before data can be loaded");  
          return ;
        }
        
        foreach(IDataPersistent dataPersistentObj in dataPersistentObject)
        { 
        dataPersistentObj.LoadData(gameData);
        }
        
        }
    public void SaveGame(){
         if(disableDataPersistent){
            return ; 
        }
        if(this.gameData == null){
            Debug.LogWarning("No data was found, A new Game needs to be started before data can be saved ");
            return ; 
        }
        foreach(IDataPersistent dataPersistentObj in dataPersistentObject){
            dataPersistentObj.SaveData(gameData);
        }
        gameData.lastUpdated = System.DateTime.Now.ToBinary();
        dataHandler.Save(gameData,selectedProfileId);
    }
    private void OnApplicationQuit(){
        SaveGame();
    }
    private List<IDataPersistent> FindAllDataPersistentObject(){
         IEnumerable<IDataPersistent> dataPersistentObject = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistent>();
            return new List<IDataPersistent>(dataPersistentObject);
    }
    public bool HasGameData(){
        return gameData != null ;
    }
    public Dictionary<string,GameData> GetAllProfilesGameData()
    {
        return dataHandler.LoadAllProfiles();
    }
    private IEnumerator AutoSave()
    {
        while (true){
            yield return new WaitForSeconds(AutoSaveTimeSeconds);
            SaveGame();
            Debug.Log("Auto Saved Games");
        }
    }
}
