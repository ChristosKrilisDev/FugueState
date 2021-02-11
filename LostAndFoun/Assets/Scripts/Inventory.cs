using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   public bool hasKey1;

    public GameObject torch;

    public List<InteractableClass> items = new List<InteractableClass>();

    private void Start()
    {
        torch.gameObject.SetActive(false);

    }

    public void EquipTorch()
    {

        torch.gameObject.SetActive(true);
    }

    public void AddItem(InteractableClass item)
    {
        items.Add(item);
    }

    public void RemoveItem(InteractableClass remove)
    {
        items.Remove(remove);
    }
}
