using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{


    public GameObject phaserPrefab;

    AudioSource audioSource;

    //game stats scriptable types
    public IntVariable Health;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Debug.Log("Start!");

    }


    // Update is called once per frame
    void Update()
    {

    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        //get the GO associated with the thing we collided with.
        GameObject objectWeCollidedWith = other.gameObject;

        //Debug.Log("ouch! " + objectWeCollidedWith.name);

        if (objectWeCollidedWith.tag == "Enemy")
        {
            Debug.Log("triggered!");

            //for now, destroy the enemy that touched us
            Destroy(objectWeCollidedWith);

            //play the "ouch!" audio clip
            audioSource.Play();

            //decrease the health
            Health.value -= 10;
        }
    }
    */
    private void OnCollisionEnter(Collision other)
    {
        //get the GO associated with the thing we collided with.
        GameObject objectWeCollidedWith = other.gameObject;

        //don't collide with the phasers we are shooting, nor with the phaser gun.
        //if (objectWeCollidedWith.tag == "Phaser") return;

        //Debug.Log("ouch! " + objectWeCollidedWith.name);

        if (objectWeCollidedWith.tag == "Enemy")
        {
            Debug.Log("collided!");

            //for now, destroy the enemy that touched us
            Destroy(objectWeCollidedWith);

            //play the "ouch!" audio clip
            audioSource.Play();

            //decrease the health
            Health.value -= 10;
        }


    }
    /*
        void OnShoot()
    {
        Debug.Log("Shoot!");
        //instantiate a phaser 0.5 meters up from the floor and 2 meters ahead of the player.  This prevents us from shooting ourself!
        Instantiate(phaserPrefab, transform.position + 0.5f * transform.up + 2.0f * transform.forward, transform.rotation);
    }
    */
    /*
    void OnActivate()
    {
        Debug.Log("Activate!");
        //instantiate a phaser 0.5 meters up from the floor and 2 meters ahead of the player.  This prevents us from shooting ourself!
        Instantiate(phaserPrefab, transform.position + 0.5f * transform.up + 2.0f * transform.forward, transform.rotation);
    }
    */
}