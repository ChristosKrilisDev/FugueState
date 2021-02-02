using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{

    private string _Gname;

    public enum GateType 
    {
        Drunk,
            B
    }
    public GateType _type = GateType.Drunk;

    public GameObject interactionPanel;



    void Start()
    {
        
    }


    void Update()
    {
        
    }

    void InteractionPanelActivity()
    {
        interactionPanel.SetActive(interactionPanel.activeSelf);
    }


    public void BlockGate()
    {
        Debug.Log("Gate Blocked");
    }

}
