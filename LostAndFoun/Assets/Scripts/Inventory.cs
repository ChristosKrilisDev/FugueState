using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   public bool hasKey1;

    public GameObject torch;

    public List<Item> items = new List<Item>();

    private void Start()
    {
        torch.gameObject.SetActive(false);

    }

    public void EquipTorch()
    {

        torch.gameObject.SetActive(true);
    }

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public void RemoveItem(Item remove)
    {
        items.Remove(remove);
    }
}
