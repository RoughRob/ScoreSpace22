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
    public float BoostJumpHeight = 10f;
    bool boosting = false;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;


    public float gravity = -5;//-9.81f;
    Vector3 velocity;
    public bool isGrouned;

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

            if(jumps < 3)
            jumps = 3;

            jumpCount.text = jumps.ToString();
        }

        if(jumps < 0)
        {
            jumps = 0;
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

        if (!boosting)
        {

            if (Input.GetButtonDown("Jump"))
            {
                if (isGrouned || jumps > 0)
                {
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                    jumps--;
                    jumpCount.text = jumps.ToString();
                }

            }
        }

        if (jumps <= 0 && velocity.y < 0)
        {
            velocity.y += (gravity * 2) * Time.deltaTime;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);

         

    }

    public void reduceJumps(int jumpsToReduseBy)
    {
        jumps = jumps - jumpsToReduseBy;
        if(jumps < 0)
        {
            jumpCount.text = "0";
        }
        else
        {
            jumpCount.text = jumps.ToString();
        }

    }

    public void BoostJump()
    {
        boosting = true;
        velocity.y = Mathf.Sqrt(BoostJumpHeight * -2f * gravity);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        StartCoroutine(BoostWait());
    }

    IEnumerator BoostWait()
    {
        yield return new WaitForSeconds(2);
        boosting = false;
    }
}
