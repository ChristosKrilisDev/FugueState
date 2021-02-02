using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehavior : MonoBehaviour
{
    public int id;
    public string notePressed;
    private AudioSource note;

    public GameObject myParticles;

    void Start()
    {
       note = this.GetComponentInParent<AudioSource>();
        myParticles.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && canPush)
        {
            
            note.Play();
            notePressed = note.clip.name;
            Debug.Log("Test Collision with " + notePressed);

            myParticles.gameObject.SetActive(true);

            canPush = false;
            PuzzleEvent.current.TileTriggerEnter(id);


        }
    }

    public void Reset()
    {
        canPush = true;
        myParticles.gameObject.SetActive(false);

    }

    bool canPush = true;


}
