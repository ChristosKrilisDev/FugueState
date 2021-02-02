using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactCollector : MonoBehaviour
{

    public GameObject audio;
    public Door[] doorsToOpen;

    public Item[] items;
    int count = 0;


    void Start()
    {
        foreach(Item item in items)
        {
            item.gameObject.SetActive(false);
        }
    }

    public void AddItem(List<Item> newItem)
    {
        bool dBreak = false;

        // Debug.Log("Find Artifact");
        for(int i = 0; i < newItem.Count; i++)
        {
            for(int j = 0; j < items.Length; j++)
            {
                if(dBreak)
                    break;

                if(newItem[i].id == this.items[j].id)
                {
                    //sfx
                    items[j].gameObject.SetActive(true);

                    FindObjectOfType<Inventory>().RemoveItem(newItem[i]);
                    count++;

                    dBreak = true;

                    break;
                    
                }                
            }
        }
        if(count >= items.Length)
        {
            Debug.Log("Find Artifact");
            audio.SetActive(true);
            doorsToOpen[0].DoorActivity();
            doorsToOpen[1].DoorActivity();
            particle.gameObject.SetActive(true);

            Destroy(this.GetComponent<ArtifactCollector>());
        }
    }


    public GameObject particle;

}
