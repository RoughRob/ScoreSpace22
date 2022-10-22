using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JumpRest : MonoBehaviour
{
    public Movement playerMovement;
    public string fallenName;
    public TextMeshProUGUI nameText;

    private void Awake()
    {
        playerMovement = GameObject.Find("/Player").GetComponent<Movement>();
        
    }

    private void Start()
    {
        nameText.text = fallenName;  
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //if(playerMovement.jumps >= 3)
            //{
                playerMovement.jumps++;
            //}
            //else
            //{
            //    playerMovement.jumps = 3;
            //}

            playerMovement.jumpCount.text = playerMovement.jumps.ToString();
            //jumpcount.text = playerMovement.jumps.ToString();
            Destroy(gameObject);
        }
    }


}
