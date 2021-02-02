using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    
    
    void Start()
    {
        itemSource = GetComponent<AudioSource>();
    }

    public int id;

    [Header("SFX")]
    private AudioSource itemSource;
    [SerializeField] private AudioClip itemSoundFX;

    public void OnPickUp()
    {
        if(itemSoundFX != null || itemSource != null)
        {
            itemSource.clip = itemSoundFX;
            itemSource.Play();
        }

        transform.GetComponent<SphereCollider>().enabled = false;
        
        transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;

        //Destroy(this.gameObject , 0.2f);

    }
}
