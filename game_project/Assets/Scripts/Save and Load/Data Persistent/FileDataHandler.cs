using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System ; 
using System.IO;
public class FileDataHandler 
{
    private string dataDirPath = "";
    private string dataFileName=  "";
    private bool useEncryption = false ;
    private readonly string encryptionCodeWord = "word";
    public FileDataHandler(string dataDirPath, string dataFileName,bool useEncryption){
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
    }
    public GameData Load(string profileId){
        if(profileId ==null){
            return null;
        }

        string fullPath = Path.Combine(dataDirPath,profileId,dataFileName);
        GameData loadedData = null ;
        if(File.Exists(fullPath)){
            try
            {
                string dataToLoad = "";
                using(FileStream stream = new FileStream(fullPath, FileMode.Open)){
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                if(useEncryption){
                dataToLoad = EncryptDecrypt(dataToLoad);
        }
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);

            }

            catch(Exception e){
                Debug.LogError("Error occured when trying to load data to file:  " + fullPath +  "\n" + e);
            }
        }
        return loadedData;
    }
    public void Save(GameData data,string profileId){
        if(profileId ==null){
            return ;
        }
        string fullPath = Path.Combine(dataDirPath,profileId,dataFileName);
        try{
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
        string dataToStore = JsonUtility.ToJson(data,true);
        if(useEncryption){
            dataToStore = EncryptDecrypt(dataToStore);
        }

        using (FileStream stream = new FileStream(fullPath,FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(dataToStore);
            }
        }
        }
        catch(Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file:  " + fullPath +  "\n" + e);
        }
        Debug.Log(fullPath);
    }
        public Dictionary<string,GameData> LoadAllProfiles(){
        Dictionary<string,GameData> profileDictionary = new Dictionary<string,GameData>();

        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(dataDirPath).EnumerateDirectories();
        foreach(DirectoryInfo dirInfo in dirInfos)
        {
            string profileId = dirInfo.Name ; 

            string fullPath = Path.Combine(dataDirPath,profileId,dataFileName);
            if(!File.Exists(fullPath)){
                Debug.LogWarning("Skipping directory when loading all profiles because if does not contain data : "+ profileId);
                continue ; 
            }
             GameData profileData = Load(profileId);

            if(profileData != null){
                profileDictionary.Add(profileId,profileData);
            }
            else{
                Debug.LogError("Tried to load profile but something went wrong. ProfileId" + profileId);

            }

        }



        return profileDictionary ; 
    }
    public string GetMostRecentlyUpdatedProfileId(){
        string mostRecentProfileId  =null ;

        Dictionary<string,GameData> profilesGameData = LoadAllProfiles();
        foreach(KeyValuePair<string,GameData> pair in profilesGameData){
            string profileId = pair.Key ;
            GameData gameData = pair.Value ; 

            if(gameData == null){
                continue ; 
            }
            if(mostRecentProfileId == null){
                mostRecentProfileId = profileId;
            }
            else{
                DateTime mostRecentDateTime = DateTime.FromBinary(profilesGameData[mostRecentProfileId].lastUpdated);
                DateTime newDateTime = DateTime.FromBinary(gameData.lastUpdated);

                if(newDateTime > mostRecentDateTime){
                    mostRecentProfileId = profileId;
                }
            }

        }
        return mostRecentProfileId ; 
    }
    private string EncryptDecrypt(string data){
        string modifiedData = "" ; 
        for (int i = 0 ; i < data.Length; i++){
            modifiedData +=(char) (data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifiedData ;
    }
}
