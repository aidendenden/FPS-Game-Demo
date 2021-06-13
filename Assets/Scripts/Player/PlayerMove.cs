using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed=12f;
    public float jumpHeight = 3f;
    public CharacterController playerController;

   
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
  

    public float gravity = -9.81f;

    public float xpos;
    public float zpos;

    Vector3 velocity;
    bool isGrounded;

    
    public Transform[] playerPosRecord;
    public int nowPos=0;

    // Start is called before the first frame update
    void Start()
    {
        playerController = gameObject.GetComponent<CharacterController>();
        this.transform.position = playerPosRecord[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position,groundDistance,groundMask);

        if (isGrounded && velocity.y<0)
        {
            velocity.y = -2f;
        }

        xpos = Input.GetAxis("Horizontal");
        zpos = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        Vector3 move = transform.right * xpos + transform.forward * zpos;
        playerController.Move(move*speed*Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        playerController.Move(velocity*Time.deltaTime);
    }

    public void ReSetPos()
    {
        this.transform.position = playerPosRecord[nowPos].position;
    }
}


