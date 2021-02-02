using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractableManager : MonoBehaviour
{

    public enum InteractableType
    {
        Door,
        Door1,
        ShakingPainting,
        AudioPainting
    };


    public InteractableType TYPE;


    private void Start()
    {

    }


    public void DoorInteraction()
    {

    }

    //Shake
    public void InvokeShakeEffect()
    {
        if(GetComponent<CameraShake>())
        {
            GetComponent<CameraShake>().enabled = true;
        }
    }

    //Audio/General
    public void PlayAudio()
    {
        if(!gameObject.GetComponent<AudioSource>().isPlaying)
            gameObject.GetComponent<AudioSource>().Play();
    }

}
