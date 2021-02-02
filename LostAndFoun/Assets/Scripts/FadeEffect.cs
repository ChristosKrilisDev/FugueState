using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEffect : MonoBehaviour
{

    private Sprite _fadeImg;


    static public FadeEffect Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            return;
        }
        //else
        Instance = this;
    }

    void Start()
    {

    }


    void Fade(bool fadeIn)
    {
        if(fadeIn)
        {
            Debug.Log("Fade in");
        }
        else
        {
            Debug.Log("Fade Out");
        }
    }




}
