using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject phaserPrefab;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //get the GO associated with the thing we collided with.
        GameObject objectWeCollidedWith = other.gameObject;

        //Debug.Log("ouch! " + objectWeCollidedWith.name);

        if (objectWeCollidedWith.tag == "Enemy")
        {
            //Debug.Log("ouch!");

            //for now, destroy the enemy that touched us
            Destroy(objectWeCollidedWith);

            //play the "ouch!" audio clip
            audioSource.Play();
        }
    }

    void OnShoot()
    {
        //Debug.Log("Shoot!");
        //instantiate a phaser 0.5 meters up from the floor and 2 meters ahead of the player.  This prevents us from shooting ourself!
        Instantiate(phaserPrefab, transform.position + 0.5f * transform.up + 2.0f * transform.forward, transform.rotation);
    }

}