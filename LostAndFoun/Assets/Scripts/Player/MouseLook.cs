using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]private float _mSensitivity = 100f;
    private Transform player;
    public Transform pTarget;
    [SerializeField]bool findPlayer = true;

    //rot camera member
    float xRot;
    [SerializeField] float _minAngle = -45; //UP-Sky
    [SerializeField] float _maxAngle = 45;  //Down-Floor


    void Start()
    {
        //Get the transfor of the player
        if(findPlayer)
            player = FindObjectOfType<PlayerMovement>().transform;

        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mSensitivity * Time.deltaTime;

        //Up- Down Rot
        xRot -= mouseY;
        //clamp {-90 , 90} !overrotate| look behind
        xRot = Mathf.Clamp(xRot , _minAngle , _maxAngle);
        transform.localRotation = Quaternion.Euler(xRot , 0f , 0f);


        //Left- Right Rot
        if(player != null)
            player.Rotate(Vector3.up * mouseX);
        else
            Debug.LogWarning("Player Is Missing from camera.cs");

    }
}
