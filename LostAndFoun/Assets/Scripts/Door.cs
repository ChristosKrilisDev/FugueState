using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    #region Members
    public bool needKey = true;
    public bool isBlocked = false;
    public ItemPickup needkey;

    public float doorOpenAgnle;
    public float doorCloseAgnle;

    [SerializeField] bool _open = false;
    [SerializeField] private bool canOpen;

    private AudioSource doorSound;
    public AudioClip openSound, close;

    bool flag = true;
    bool blockDoor = false;
    bool tryToOpen = false;

    #endregion

    private void Start()
    {
        doorSound = GetComponent<AudioSource>();
        //Close door
        if(_open)
        {
            transform.localRotation = Quaternion.Euler(-90 , 0 , doorCloseAgnle);
            _open = !_open;
        }
    }

    public void DoorActivity()
    {
        if(canOpen)
        {
            flag = true;
            tryToOpen = true;
            _open = !_open;
        }
    }

    private void Update()
    {
        if(!canOpen)
            return;


        if(_open)
        {

            Quaternion newRot = Quaternion.Euler(-90 , 0 , doorOpenAgnle);
            transform.localRotation = Quaternion.Slerp(transform.localRotation , newRot , 0.150f);

            if(!doorSound.isPlaying && flag && tryToOpen)
            {
                tryToOpen = false;
                if(doorSound.isPlaying)
                {
                    doorSound.Stop();
                }
                doorSound.PlayOneShot(openSound);
                flag = false;
            }
        }
        else
        {
            Quaternion newRot = Quaternion.Euler(-90 , 0 , doorCloseAgnle);
            transform.localRotation = Quaternion.Slerp(transform.localRotation , newRot , 0.150f);

            if(!doorSound.isPlaying && flag && tryToOpen)
            {
                tryToOpen = false;
                if(doorSound.isPlaying)
                {
                    doorSound.Stop();
                }

                doorSound.PlayOneShot(openSound);
                flag = false;


            }
        }
    }




}
