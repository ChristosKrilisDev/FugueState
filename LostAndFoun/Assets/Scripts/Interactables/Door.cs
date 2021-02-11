using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableClass
{
    #region Members
    [Space]
    [Header("Door Members")]
    public bool _needsKey = true;
    public EquipableItem needkey;
    [SerializeField] private bool _canOpen = true;
    bool _isOpen = true;
    bool tryToOpen = false;

    [Space]
    [Header("Rot Z open/close door")]
    public float doorOpenAgnle;
    public float doorCloseAgnle;

    [Header("Audio sfxs")]
    public AudioClip sfxOpen, sfxClose;

    #endregion

    private void Start()
    {
        //Close door
        if(_isOpen)
        {
            transform.localRotation = Quaternion.Euler(-90 , 0 , doorCloseAgnle);
            _isOpen = false;
        }
    }

    public override void OnInteraction()
    {
        base.OnInteraction();
        DoorActivity();
    }

    public void DoorActivity()
    {
        tryToOpen = true;
        _isOpen = !_isOpen;
    }

    //override public void PlayOneShot(AudioClip sfx)
    //{
    //    base.PlayOneShot(sfx);
    //}

    //RREFACTOR TO COUROUTINE
    private void Update()
    {
        if(!_canOpen)
            return;

        if(_isOpen)
        {

            Quaternion newRot = Quaternion.Euler(-90 , 0 , doorOpenAgnle);
            transform.localRotation = Quaternion.Slerp(transform.localRotation , newRot , 0.150f);
            if(tryToOpen)
            {
                tryToOpen = false;
                PlayOneShot(sfxOpen);

            }
        }
        else
        {
            Quaternion newRot = Quaternion.Euler(-90 , 0 , doorCloseAgnle);
            transform.localRotation = Quaternion.Slerp(transform.localRotation , newRot , 0.150f);
            if(tryToOpen)
            {
                tryToOpen = false;
                PlayOneShot(sfxClose);

            }
        }
    }

}
