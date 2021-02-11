using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRaycasting : MonoBehaviour
{
    #region Members
    [Header("Camera Look")]
    [SerializeField] [Range(2 , 8)] private float _maxDistanceToSee = 6;
    [SerializeField] [Range(0.5f , 4)] private float _minDinstanceToInteract = 2;
    float distanceFromInteractable; //distance transform - target.tranmsform

    [Space]
    public LayerMask interactable;
    RaycastHit hit;

    
    public enum CURSOR
    {
        DEFAULT,
        FOCUS,
        HIDE
    }
    [Space][Header("Cursor State")]
    public CURSOR cursor = CURSOR.DEFAULT;
    private Image crossHair;


    [Space][Header("Interaction UI")]
    [SerializeField] private GameObject _interactionPanel;
    private Text interactionTxt;

    //
    private InteractableClass hitInteractable = null;
    private PlayerMovement playerMovement = null;


    [Header("Pickable Obj")]
    private Transform pickablePaperItem;
    private Vector3 itemOriginPos;
    private Quaternion itemOriginRot;
    public float RotationSpeed;

    #endregion


    private void Awake()
    {
        Init();
    }

    void Start()
    {
        //Init();
    }
    void Init()
    {
        playerMovement = this.transform.parent.gameObject.GetComponent<PlayerMovement>();


        //Find cursor
        cursor = CURSOR.DEFAULT;

        if(_interactionPanel != null)
            interactionTxt = _interactionPanel.transform.GetChild(0).GetComponent<Text>();

        //Define LayerMask
        //interactable = LayerMask.GetMask("Interactable");

        //Set all texts to CAPS
        Text_DoorWithKey = Text_DoorWithKey.ToUpper();
        Text_DoorWithOutKey = Text_DoorWithOutKey.ToUpper();
        Text_CollectableItem = Text_CollectableItem.ToUpper();
        Text_EquipableItem = Text_EquipableItem.ToUpper();
        Text_DropItem = Text_DropItem.ToUpper();
        Text_PlaceItem = Text_PlaceItem.ToUpper();
    }

    /// <summary>
    /// refactor ray casting here ,to many usage and GC status
    /// </summary>
    void Update()
    {
#if UNITY_EDITOR
        //editor mode
        Debug.DrawRay(this.transform.position , this.transform.forward * _maxDistanceToSee , Color.magenta);    //view distance
        Debug.DrawRay(this.transform.position , this.transform.forward * _minDinstanceToInteract , Color.yellow);   //interaction distance
# endif
        //Resize mouse
        cursor = CURSOR.DEFAULT;

        //
        DropPaperItem();

        //If object is in range (distanceToSee) : get whatIHit as output => activate interaction panel
        //if is close enough (minDistance) : let him pick it up<E>
        if(Physics.Raycast(this.transform.position , this.transform.forward , out hit , _maxDistanceToSee , interactable))
        {
            #region OnInteractableHitLogic
            //does hit interactable layer and interactableClass?
            if(hit.collider.gameObject.GetComponent<InteractableClass>())
            {
                cursor = CURSOR.FOCUS;

                distanceFromInteractable = Vector3.Distance(transform.position , hit.transform.position);

                if(distanceFromInteractable <= _minDinstanceToInteract)
                {
                    //if isnt null
                    if(!hitInteractable) //block the code from execute all this over and over, run this only once !
                    {
                        Debug.Log("check hit");
                        hitInteractable = hit.collider.gameObject.GetComponent<InteractableClass>();
                        ShowInteractionPanel("" ,hitInteractable);
                        return;
                    }

                    //else
                    if(Input.GetKeyDown(KeyCode.E))
                        hitInteractable.OnInteraction();

                    pickablePaperItem = hitInteractable.transform;
                    InteractablePaperItem();
                }
            }
            if(distanceFromInteractable >= _minDinstanceToInteract)
            {
                //CursorState();
                HideInteractionPanel();
                hitInteractable = null;
            }
            #endregion
        }
        else if(_interactionPanel.activeInHierarchy)    //Raycast didnt detect any interactable layer
        {
            hitInteractable = null;
            HideInteractionPanel();
        }//close panel


        if(!playerMovement.enabled)//holding readable item
        {
            ShowInteractionPanel("Press Q To Drop");
            pickablePaperItem.transform.Rotate((Input.GetAxis("Mouse Y") * RotationSpeed * Time.deltaTime) , (Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime) , 0 , Space.Self);

        }

        CursorState();

    }


    private bool changeFocusCursor = false;

    void CursorState()
    {
        //if(!changeFocusCursor)
        //    return;

        changeFocusCursor = false;
        switch(cursor)
        {
            case CURSOR.DEFAULT:
            crossHair.color = Color.white;
            crossHair.transform.localScale = new Vector3(0.1f , 0.1f , 0.1f);
            crossHair.gameObject.SetActive(true);
            break;
            case CURSOR.FOCUS:
            crossHair.color = Color.red;
            crossHair.transform.localScale = new Vector3(0.2f , 0.2f , 0.2f);
            crossHair.gameObject.SetActive(true);
            break;
            case CURSOR.HIDE:
            crossHair.gameObject.SetActive(false);
            break;
        }
    }

    #region IntertactionCheckMethods

    void InteractablePaperItem()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerMovement.enabled)
        {
            cursor = CURSOR.HIDE;

            pickablePaperItem = hit.transform;
            itemOriginPos = pickablePaperItem.position;
            itemOriginRot = pickablePaperItem.rotation;
            pickablePaperItem.transform.LookAt(this.transform , Vector3.up);
            pickablePaperItem.transform.parent = this.transform;
            pickablePaperItem.transform.localPosition = new Vector3(0 , 0 , 0.7f);
            playerMovement.enabled = false;


            transform.GetComponent<MouseLook>().enabled = false;
        }

    }
    void DropPaperItem()
    {
        if(Input.GetKeyDown(KeyCode.Q) && !playerMovement.enabled)
        {
            cursor = CURSOR.DEFAULT;

            playerMovement.enabled = true;
            pickablePaperItem.transform.parent = null;
            pickablePaperItem.position = itemOriginPos;
            pickablePaperItem.rotation = itemOriginRot;

            transform.GetComponent<MouseLook>().enabled = true;

        }

    }

    #endregion



    #region InteractionPanelMethods
    [Space]
    [Header("Interaction Texts")]
    [SerializeField] private string Text_SimpleInteraction = " E Interact";
    [SerializeField] private string Text_CollectableItem = "Press E to pick up";
    [SerializeField] private string Text_EquipableItem = "Press E to equip";

    [SerializeField] private string Text_DoorWithKey = "Press E to use Key";
    [SerializeField] private string Text_DoorWithOutKey = "You need a Key...";
    [SerializeField] private string Text_DropItem = "Press Q to drop item";
    [SerializeField] private string Text_PlaceItem = "Press E to place item";
    void ShowInteractionPanel(string txt = " E Interact", InteractableClass interactable = null)
    {
        //Editor only

#if UNITY_EDITOR
        if(_interactionPanel == null)
        {
            Debug.LogError("Interaction Panel is missing idiotaaa");
            return;
        } 
#endif

        switch(interactable.MyUse)
        {
            case InteractableClass.USE.UNSPECIFIED:
            txt = Text_SimpleInteraction;
            break;

            case InteractableClass.USE.SINGLE:
            if(interactable.myType == InteractableClass.TYPE.DOOR)
            {
                Door door = interactable.GetComponent<Door>();
                if(door.needkey)
                    txt = Text_DoorWithOutKey;
                else
                    txt = Text_DoorWithKey;
            }
            else
                txt = Text_SimpleInteraction;
            break;

            case InteractableClass.USE.COLLECT:
            txt = Text_CollectableItem;
            break;

            case InteractableClass.USE.EQUIP:
            txt = Text_EquipableItem;
            break;
        }

        //else
        interactionTxt.text = txt;
        _interactionPanel.gameObject.SetActive(true);
    }

    void HideInteractionPanel()
    {
        interactionTxt.text = "";
        _interactionPanel.gameObject.SetActive(false);
    }
    #endregion

}