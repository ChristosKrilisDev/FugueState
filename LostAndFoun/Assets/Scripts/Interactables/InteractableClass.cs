using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class InteractableClass : MonoBehaviour
{
    public enum TYPE
    {
        UNSPECIFIED,
        KEY,
        DOOR,
        ARTIFACT,
        COLLECTOR,
        PAPPER,
        TORCHE
    }
    public TYPE myType = TYPE.UNSPECIFIED;

    /// <summary>
    /// Single use : Door -> open/close
    /// Collect : Key -> store to inventory
    /// Equip : Torche -> equip to player
    /// </summary>
    public enum USE
    {
        UNSPECIFIED,
        SINGLE,
        COLLECT,
        EQUIP,
        READ
    }
    public USE MyUse = USE.UNSPECIFIED;

    public LayerMask interactableLayerMask;
    [Space]
    //Action
    private Action OnInteractionAction;
    //private void AddListener() => OnInteractionAction += ItemFuctionallity;
    //private void RemoveListener() => OnInteractionAction -= ItemFuctionallity;

    public List<UnityEvent> eventsOnInteraction;

    //my audio
    protected AudioSource inter_AudioSource;

    private void Awake()
    {
        //gameObject.layer = itemLayer;
    }

    #region DefaultMethods
    void Init()
    {
        inter_AudioSource = GetComponent<AudioSource>();
        //AddListener();
    }

    void OnEnable()
    {
        Debug.Log("Action ITem");
        Init();
    }

    void OnDisable()
    {
        //RemoveListener();
    }
    #endregion

    #region Delegates_Actions_Events
    public void ActionCaller()
    {
        if(OnInteractionAction != null)
            OnInteractionAction.Invoke();
    }

    public void EventCaller()
    {
        foreach(UnityEvent e in eventsOnInteraction)
        {
            if(e != null)
                e.Invoke();
        }
    }
    #endregion

    void UsageFlow()
    {
        switch(MyUse)
        {
            case USE.UNSPECIFIED:
            Debug.Log("#Interactable# Usage is unspecified !!");
            break;
            case USE.SINGLE:
            //Do nothing in here
            Debug.Log("#Interactable# Single Use !!");

            break;
            case USE.COLLECT:
            Debug.Log("#Interactable# collect Use !!");

            break;
            case USE.EQUIP:
            Debug.Log("#Interactable# equip Use !!");
            break;

            case USE.READ:
            Debug.Log("#Interactable# read Use !!");
            break;
        }
    }


    /// <summary>
    /// The fuctions below share the same logic with all interactable objs
    /// </summary>
    virtual public void OnInteraction()
    {
        ActionCaller();
        EventCaller();
        UsageFlow();
        //Debug.Log("#ItemBaseClass#Parrent call listener");
    }

    protected void PlayOneShot(AudioClip sfx)
    {
        if(sfx != null || inter_AudioSource != null)
        {
            if(inter_AudioSource.isPlaying)
                inter_AudioSource.Stop();
            //then
            inter_AudioSource.clip = sfx;
            inter_AudioSource.Play();
        }

    }
}

