using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
public class DialogueVariables
{
    public Dictionary<string, Ink.Runtime.Object> variables{get;private set;} 
    private Story globalVariablesStory;
    private const string saveVariablesKey = "INK_VARIABLES" ; 
    public DialogueVariables(TextAsset loadGlobalsJSON){
        globalVariablesStory = new Story(loadGlobalsJSON.text);
        if(PlayerPrefs.HasKey(saveVariablesKey)){
            string JsonState = PlayerPrefs.GetString(saveVariablesKey);
            globalVariablesStory.state.LoadJson(JsonState);
        }
        variables = new Dictionary<string,Ink.Runtime.Object>();
        foreach(string name in globalVariablesStory.variablesState){
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name,value);
            Debug.Log("Initialized global dialogue variable : "+name +" = " + value);
        }
        


    }
    public void SaveVariables(){
        if(globalVariablesStory != null){
            VariablesToStory(globalVariablesStory);
            PlayerPrefs.SetString(saveVariablesKey,globalVariablesStory.state.ToJson());
        }
    }
    public void StartListening(Story story)
    {
        VariablesToStory(story);
       story.variablesState.variableChangedEvent += VariableChanged ; 
    }
    public void StopListening(Story story){
       story.variablesState.variableChangedEvent -= VariableChanged ; 

    }
    public void VariableChanged(string name, Ink.Runtime.Object value){
        if(variables.ContainsKey(name)){
            variables.Remove(name);
            variables.Add(name,value);
        }
    }
    private void VariablesToStory(Story story){
        foreach(KeyValuePair<string,Ink.Runtime.Object> variable in variables){
            story.variablesState.SetGlobal(variable.Key,variable.Value);
        }
    }
}
