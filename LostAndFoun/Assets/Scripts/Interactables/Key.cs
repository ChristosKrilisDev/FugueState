using UnityEngine;

public class Key : InteractableClass
{

    [Header("Key Members")]
    [SerializeField]private int kID = 0;
    public int KID { get => kID; set => kID = value; }

    [Space]
    [Tooltip("Sigle key : only opens one door")]
    public bool isSigleKey = true;
    public Door linkedDoor;

    [Header("Audio")]
    [SerializeField] private AudioClip sfx;

    public override void OnInteraction()
    {
        base.OnInteraction();

        PlayOneShot(this.sfx);

        transform.GetComponent<SphereCollider>().enabled = false;
        transform.GetComponent<MeshRenderer>().enabled = false;

        FindObjectOfType<Inventory>().AddItem(this);

        Destroy(this.gameObject , 0.25f);
    }


}
