using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 5f;
    public float jumpHeight = 3f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;


    public float gravity = -9.81f;
    Vector3 velocity;
    bool isGrouned;

    public int jumps = 3;
    public TextMeshProUGUI jumpCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrouned = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrouned && velocity.y < 0)
        {
            velocity.y = -2f;
            jumps = 3;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        //float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, 0f).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);


            controller.Move(direction * speed * Time.deltaTime); 
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrouned || jumps > 0)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                jumps--;
                jumpCount.text = jumps.ToString();
            }
            else if (jumps <= 0)
            {
                //
            }
            
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }
}
