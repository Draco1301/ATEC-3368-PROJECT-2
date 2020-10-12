using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [SerializeField] CharacterController cc;
    [SerializeField] float speed;
    [SerializeField] float sprintSpeed;

    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;
    private bool grounded;

    [SerializeField] float jumpHeight;
    [SerializeField] float gravity = -9.81f;
    Vector3 velocity;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {

        //Ground Checking
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (grounded && velocity.y < 0) {
            velocity.y = -2;
        }

        //Moving
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        move.Normalize();

        if (Input.GetButton("Sprint")) {
            cc.Move(move * sprintSpeed * Time.deltaTime);
        } else { 
            cc.Move(move * speed * Time.deltaTime);
        }
        //Falling and jumping
        velocity.y += gravity * Time.deltaTime;
        if (Input.GetButtonDown("Jump") && grounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);        
        }

        cc.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheck.position,groundDistance);
    }
}
