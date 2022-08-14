using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider ; 
    // Start is called before the first frame update
    public void SetMaxHealth(int health){
        slider.maxValue = health ;
        slider.value = health ; 
    }
    public void SetHealth(int Health){
        slider.value = Health ;
    }
}
