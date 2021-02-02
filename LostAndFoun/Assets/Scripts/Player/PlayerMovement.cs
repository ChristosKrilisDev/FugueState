using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    private CharacterController _controller;

    [SerializeField] private float _speed = 6f;
    [SerializeField] float _gravity = -9.81f;


    [SerializeField] private Transform _gCheck;
    [SerializeField] private float _gDistancel;
    [SerializeField] private LayerMask _gMask;


    Vector3 velocity;
    [SerializeField] bool _isGrounded;
    public Transform startPos;
    AudioSource audioStep;


    private void Awake()
    {
        _controller = GetComponent<CharacterController>();

        audioStep = GetComponent<AudioSource>();
    }


    void Start()
    {
        //transform.position = startPos.position;

        //FindObjectOfType<PlayerMovement>().enabled = false;

    }

    void Update()
    {
        //Ground Check
        _isGrounded = Physics.CheckSphere(_gCheck.position , _gDistancel , _gMask);

        if(_isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //Input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        //Movement
        Vector3 move = transform.right * x + transform.forward * z;
        _controller.Move(move * _speed * Time.deltaTime);

        velocity.y += _gravity * Time.deltaTime;
        _controller.Move(velocity * Time.deltaTime);

        if(_controller.velocity.magnitude > 0.01f && !audioStep.isPlaying)
        {
            audioStep.volume = Random.Range(0.6f , 1.0f);
            audioStep.pitch = Random.Range(0.8f , 1.0f);
            audioStep.Play();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Block")
        {
            //Dead
            GetComponent<KnightMoves>().enabled = true;
        }

        if(other.gameObject.tag == "Trap")
        {
            //Dead

            GameManager.Instance.OnDeath();
            //transform.position = startPos.position;
        }
        if(other.gameObject.tag == "End")
        {
            //end
            GameManager.Instance.OnWin();
            //transform.position = startPos.position;
        }

    }


    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Block1")
        {
            //Dead
            GetComponent<KnightMoves>().enabled = false;
        }
    }

}