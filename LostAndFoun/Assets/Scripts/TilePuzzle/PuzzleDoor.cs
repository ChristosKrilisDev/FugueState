using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDoor : MonoBehaviour
{

    public int id;
    private bool order1;

    private List<int> orderOfTiles = new List<int>();
    public List<TileBehavior> tiles = new List<TileBehavior>();
    public GameObject totemParticles;
    private List<int> correctOrderOfTiles1 = new List<int>() { 1, 2, 3, 4 };

    bool flag = false;

    // Start is called before the first frame update
    void Start()
    {
        PuzzleEvent.current.onTileTriggerEnter += OnTriggerPress;
        
    }

    private void OnTriggerPress(int id)
    {
        orderOfTiles.Add(id);

    }

    private bool CheckIfCorrect()
    {
        if (Enumerable.SequenceEqual(orderOfTiles, correctOrderOfTiles1))
        {
            totemParticles.gameObject.SetActive(true);

            PuzzleEvent.current.onTileTriggerEnter -= OnTriggerPress;

            return true;  
        }

        StartCoroutine(CloseLights());

        orderOfTiles.Clear();
        return false;     
    }


    IEnumerator CloseLights()
    {

        yield return new WaitForSeconds(1.5f);

        foreach(TileBehavior item in tiles)
        {
            item.Reset();
        }

        yield return null;
    }

    private void Update()
    {
              
        if (orderOfTiles.Count == 4)
        {
            
            order1 = CheckIfCorrect();
        }

        if(order1 && !flag)
        {
            flag = true;
            GetComponent<Door>().DoorActivity();
        }  
    }



}
