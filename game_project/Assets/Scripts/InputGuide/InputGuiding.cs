using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputGuiding : MonoBehaviour
{
    private Animator GuideAnimator;
    [SerializeField] private GameObject MoveGuide ; 
    [SerializeField] private GameObject JumpGuide ; 
    [SerializeField] private GameObject ClimbGuide ; 
    [SerializeField] private GameObject DashGuide ; 
    void Start()
    {
        GuideAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
