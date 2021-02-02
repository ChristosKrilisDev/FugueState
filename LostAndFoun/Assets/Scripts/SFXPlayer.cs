using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SFXPlayer : MonoBehaviour
{

    public GameObject sound;
    public bool play;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            sound.SetActive(play);
        }        
    }


    public void PlayOnes()
    {
        sound.GetComponent<AudioSource>().Play();
    }

}
