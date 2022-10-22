using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JumpRest : MonoBehaviour
{
    public Movement playerMovement;
    public TextMeshProUGUI jumpcount;

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

            jumpcount.text = playerMovement.jumps.ToString();
            Destroy(gameObject);
        }
    }


}
