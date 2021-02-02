using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRaycasting : MonoBehaviour
{
    // Start is called before the first frame update

    public float distanceToSee;
    RaycastHit whatIHit;
    private GameObject player;

    [Header("UI")]
    public GameObject InteractionPanel;
    private Text _interactionTxt;
    private ItemPickup f_item;

    LayerMask interactable;




    const string DoorInteractionTextWithKey = "Press E to use Key";
    const string DoorInteractionTextWithOutKey = "You need a Key...";
    const string ItemInteractionText = "Press E to pick up";
    const string CollectorInteractionText = "Press E to place item";


    [Header("World Canvas UI")]
    public GameObject interCanvasPrefab;
    private GameObject cCanvas;
    bool canCreateCanvas = true;
    float diffDist;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        if(InteractionPanel!= null)
        _interactionTxt = InteractionPanel.transform.GetChild(0).GetComponent<Text>();

        interactable = LayerMask.GetMask("Interactable");
    }

    // Update is called once per frame
    void Update()
    {
        //Just to see it in scene mode - pink
        Debug.DrawRay(this.transform.position , this.transform.forward * distanceToSee , Color.magenta);


        GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Image>().color = Color.white;
        GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Image>().transform.localScale = new Vector3(0.1f , 0.1f , 0.1f);

        //If object is in range (distanceToSee), get whatIHit as output
        if(Physics.Raycast(this.transform.position , this.transform.forward , out whatIHit , distanceToSee , interactable))
        {

            GameObject.FindGameObjectWithTag("Crosshair1").GetComponent<Image>().color = Color.red;
            GameObject.FindGameObjectWithTag("Crosshair1").GetComponent<Image>().transform.localScale = new Vector3(0.25f , 0.25f , 0.25f);

            //if(canCreateCanvas)
            //{
            //    canCreateCanvas = false;
            //    cCanvas = CreateInteractionCanvas(whatIHit.collider.transform);
            //}
            diffDist = Vector3.Distance(transform.position , whatIHit.transform.position);

            //Debug.Log("Dist : " + Vector3.Distance(transform.position , whatIHit.transform.position));
            if(diffDist <= 2)
            {
                #region Note/Artifact/Item
                if(whatIHit.transform.gameObject.tag == "NotesSolution")
                {
                    ShowInteractionPanel("Press E To Use");
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        SFXPlayer notes = whatIHit.transform.gameObject.GetComponent<SFXPlayer>();
                        notes.PlayOnes();
                    }
                }

                if(whatIHit.transform.gameObject.tag == "Item")
                {
                    ShowInteractionPanel(ItemInteractionText);

                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        //Artficat is hitted
                        Item a_Item = whatIHit.transform.gameObject.GetComponent<Item>();
                        player.GetComponent<Inventory>().AddItem(a_Item);

                        a_Item.OnPickUp();
                    }
                }

                if(whatIHit.transform.gameObject.tag == "Artifact")
                {

                    ShowInteractionPanel(CollectorInteractionText);

                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        //Artficat is hitted
                        ArtifactCollector collector = FindObjectOfType<ArtifactCollector>();

                        collector.AddItem(player.GetComponent<Inventory>().items);
                    }

                }
                #endregion

                #region Paintings

                //Painting Logic
                if(whatIHit.collider.gameObject.GetComponent<InteractableManager>())
                {
                    InteractableManager interactable = whatIHit.collider.gameObject.GetComponent<InteractableManager>();

                    //ShowInteractionPanel("Press E to listen");

                    //Audio Painting
                    if(interactable.TYPE == InteractableManager.InteractableType.AudioPainting)
                    {
                        interactable.PlayAudio();
                    }
                    //Shaking Painting
                    else if(interactable.TYPE == InteractableManager.InteractableType.ShakingPainting)
                    {
                        interactable.PlayAudio();
                        interactable.InvokeShakeEffect();
                    }

                    #endregion

                    #region DoorRayCast
                    //else is door?!
                    else if(interactable.TYPE == InteractableManager.InteractableType.Door || interactable.TYPE == InteractableManager.InteractableType.Door1)
                    {
                        Door focus_Door = interactable.gameObject.GetComponent<Door>();

                        if(focus_Door.needKey) //Locked Door
                        {
                            //Show msg
                            if(player.GetComponent<Inventory>().hasKey1)
                                ShowInteractionPanel(DoorInteractionTextWithKey);
                            else
                                ShowInteractionPanel(DoorInteractionTextWithOutKey);

                            if(Input.GetKeyDown(KeyCode.E)) //this is about doors, try to open it
                            {
                                if(focus_Door.needKey)
                                {
                                    if(player.GetComponent<Inventory>().hasKey1)
                                        whatIHit.transform.gameObject.GetComponent<Door>().DoorActivity();

                                }

                            }
                        }
                        else
                        {
                            ShowInteractionPanel("Press E to interact");

                            if(Input.GetKeyDown(KeyCode.E))
                                whatIHit.transform.gameObject.GetComponent<Door>().DoorActivity();
                        }

                    }
                    #endregion
                }

                #region ItemRayCast/General
                if(whatIHit.collider.gameObject.GetComponent<ItemPickup>())
                {
                    f_item = whatIHit.collider.gameObject.GetComponent<ItemPickup>();

                    //f_item.HighLightStatus(true);

                    //show panel
                    ShowInteractionPanel(ItemInteractionText);
                    //If player is pressing E button - Maybe needs to be informed of pickable option - UI
                    if(Input.GetKeyDown(KeyCode.E))  //this is an item, try to pick it up
                    {

                        if(f_item.whatItemAmI == ItemPickup.items.key1)
                            player.GetComponent<Inventory>().hasKey1 = true;

                        else if(f_item.whatItemAmI == ItemPickup.items.torch)
                            player.GetComponent<Inventory>().EquipTorch();

                        f_item.OnPickUp();
                    }
                }
                #endregion
            }


        }
        else if(InteractionPanel.activeInHierarchy)
        {
            //Destroy(cCanvas);
            //cCanvas = null;
            //canCreateCanvas = true;
            HideInteractionPanel();

        }//close panel

    }


    GameObject CreateInteractionCanvas(Transform parentT)
    {
        return Instantiate(interCanvasPrefab, parentT);

    }


    #region InteractionPanelMethods
    void ShowInteractionPanel(string txt)
    {

        if(InteractionPanel == null)
        {
            Debug.LogError("Interaction Panel is missing idiotaaa");
            return;
        }

        //else
        _interactionTxt.text = txt;
        InteractionPanel.gameObject.SetActive(true);
    }

    void HideInteractionPanel()
    {
        InteractionPanel.gameObject.SetActive(false);
    }
    #endregion



}
