using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class JumpRest : MonoBehaviour
{
    public Movement playerMovement;
    public string fallenName;
    public TextMeshProUGUI nameText;

    AudioSource Audio;

    public GameObject particles;

    bool triggered = false;

    private void Awake()
    {
        playerMovement = GameObject.Find("/Player").GetComponent<Movement>();


    }

    private void Start()
    {
        nameText.text = fallenName;
        Audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && triggered == false)
        {

            playerMovement.jumps++;

            Audio.Play();

            playerMovement.jumpCount.text = playerMovement.jumps.ToString();

            triggered = true;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            particles.SetActive(false);

            Debug.Log(other.gameObject.name);

            StartCoroutine(WaitForSound());   

        }
    }

    IEnumerator WaitForSound()
    {
        yield return  new WaitForSeconds(1);
        Destroy(gameObject);
    }

}
