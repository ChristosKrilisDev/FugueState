using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleEvent : MonoBehaviour
{

    public static PuzzleEvent current;
    
    void Awake()
    {
        current = this;
    }


    public event Action<int> onTileTriggerEnter;

    public void TileTriggerEnter(int id)
    {
        if (onTileTriggerEnter != null)
        {
            onTileTriggerEnter(id);
        }
    }
   

   
}
