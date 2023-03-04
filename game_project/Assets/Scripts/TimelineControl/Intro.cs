using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Intro : MonoBehaviour
{
   void OnEnable(){
    SceneManager.LoadSceneAsync("Introduction");
   }
}
