using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemPickup : MonoBehaviour
{
    public enum items { key1, key2 ,torch ,nothing};
    public items whatItemAmI;

    public bool hasEvent;
    public UnityEvent eDo;

    [Header("SFX")]
    private AudioSource itemSource;
    [SerializeField] private AudioClip itemSoundFX;

    private void Awake()
    {
        itemSource = GetComponent<AudioSource>();
    }

    public void OnPickUp()
    {
        if(itemSoundFX != null || itemSource != null)
        {
            itemSource.clip = itemSoundFX;
            itemSource.Play();
        }

        GetComponent<MeshRenderer>().enabled = false;

        if(eDo != null)
        {
            eDo.Invoke();
        }

        eDo.RemoveAllListeners();

        Destroy(this.gameObject , 0.2f);
    }


    public GameObject highLight;

    public void HighLightStatus(bool open)
    {
        if(open)
        {
            highLight.gameObject.SetActive(true);
        }
        else
        {
            highLight.gameObject.SetActive(false);

        }
    }
}
