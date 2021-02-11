using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMoves : MonoBehaviour
{
    public AudioSource theme;
    public Door linkedDoor;

    public Transform initPos;
    private List<string> tile;
    private bool newEntry = false;
    private string target;
    private List<List<string>> chess = new List<List<string>> { new List<string> { "a7", "b7", "c7", "d7", "e7", "f7", "g7" },
                                                                new List<string> { "a6", "b6", "c6", "d6", "e6", "f6", "g6" },
                                                                new List<string> { "a5", "b5", "c5", "d5", "e5", "f5", "g5" },
                                                                new List<string> { "a4", "b4", "c4", "d4", "e4", "f4", "g4" },
                                                                new List<string> { "a3", "b3", "c3", "d3", "e3", "f3", "g3" },
                                                                new List<string> { "a2", "b2", "c2", "d2", "e2", "f2", "g2" },
                                                                new List<string> { "a1", "b1", "c1", "d1", "e1", "f1", "g1" },  };


    private List<string> allowed;
    private bool error = false;
    public AudioSource audio;

    void Start()
    {
        target = "c3";
        allowed = new List<string> { "b1", "b2", "b3", "c3" };
        tile = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        if(newEntry)
        {

            if ( !allowed.Contains(tile[tile.Count-1]))
            {
                Initialize();
            }
                

            FindObjectOfType<PlayerMovement>().enabled = true;
            newEntry = ! newEntry;


            if(tile[tile.Count - 1] == target)
            {
                allowed = NewList(target);
                target = NewTarget(target);
                if(target != null)
                    GameObject.Find(target).GetComponent<Light>().intensity = 200;
                else
                {
                    linkedDoor.DoorActivity();
                    theme.loop = false;

                    Destroy(GetComponent<KnightMoves>());//this.GetComponent<KnightMoves>().enabled = false;
                }
            }
        }
    }

    IEnumerator Respawn()
    {
        {
            float elapsedTime = 0;
            float waitTime = 5f;

            while (elapsedTime < waitTime && Vector3.Magnitude(transform.position - initPos.position) > 0.2f)
            {
                transform.position = Vector3.Lerp(transform.position, initPos.position, elapsedTime / waitTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, initPos.rotation, elapsedTime / waitTime);
                elapsedTime += Time.deltaTime;
                yield return null;

            }
            //transform.rotation = initPos.rotation;
            yield return null;
        }
    }

    void Initialize()
    {
        if(error && audio != null)
            audio.Play();

        GameObject.Find(target).GetComponent<Light>().intensity = 0;
        error = false;
        FindObjectOfType<PlayerMovement>().enabled = false;
        StartCoroutine(Respawn());
        //transform.position = initPos.position;
        target = "c3";
        GameObject.Find(target).GetComponent<Light>().intensity = 200;
        allowed = new List<string> { "c1","c2","b1", "b2", "b3", "c3" };
        tile = new List<string>();
    }
    string NewTarget(string target)
    {
        GameObject.Find(target).GetComponent<Light>().intensity = 0;
        switch (target)
        {
            case "c3":
                return "d5";
            case "d5":
                return "f6";
            case "f6":
                return "g4";
        }
        return null;
    }
    List<string> NewList(string target)
    {
        switch (target)
        {
            case "c3":
                return new List<string> { "c3", "d3", "d4", "d5", "c4", "c5" }; 
            case "d5":
                return new List<string> { "d5", "d6", "e6", "f6", "e5", "f5" }; 
            case "f6":
                return new List<string> { "f6", "f5", "f4", "g4", "g6", "g5" }; 
        }
        return null;
    }

    private void OnTriggerEnter(Collider other)
    {



        if(other.gameObject.transform.parent.name == "Checkerboard")
        {
            if (other.gameObject.name == "InitPos")
            {
                error = true;
                GameObject.Find(target).GetComponent<Light>().intensity = 200;
            }
        }
        else if (other.gameObject.transform.parent.transform.parent.name == "Checkerboard")
        {
            newEntry = true;
            tile.Add(other.gameObject.name + other.gameObject.transform.parent.name);
        }
    }
}
