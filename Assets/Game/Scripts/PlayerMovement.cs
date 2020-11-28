using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;  //Reference to the character controller

    [SerializeField] private float moveSpeed, gravity, jumpHeight;


    public Transform groundCheck;           //The transform of the ground check object on the player
    public float groundDistance = 0.4f;     //The radius of the groundCheck sphere
    public LayerMask groundMask;            //The objects (stored in a layer) that the groundCheck sphere will "overlap" with

    Vector3 velocity;                       //The player velocity
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; //Still apply some velocity to ensure player stays on ground
        }

        float xMovement = Input.GetAxis("Horizontal");  //Whether the player is moving left or right
        float zMovement = Input.GetAxis("Vertical");    //Whether the player is moving forward or back

        Vector3 move = transform.right * xMovement + transform.forward * zMovement;
        velocity.y += gravity * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        controller.Move(move * moveSpeed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);

    }
}
