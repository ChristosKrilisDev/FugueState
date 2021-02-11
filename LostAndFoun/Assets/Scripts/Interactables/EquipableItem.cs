using UnityEngine;

public class EquipableItem : InteractableClass
{
    [Header("SFX")]
    [SerializeField] private AudioClip sfx;

    public void OnPickUp()
    {
        PlayOneShot(sfx);

        transform.GetComponent<SphereCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        Destroy(this.gameObject , 0.2f);

        FindObjectOfType<Inventory>().EquipTorch();
    }


    public override void OnInteraction()
    {
        base.OnInteraction();
        OnPickUp();
    }
}
