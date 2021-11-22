using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 1f;
    GameObject player;

    bool bDestroy = false;  //set to true to destroy the object

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //find the player.  This should be the only GameObject tagged as 'Player'
        try
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        catch (System.Exception e)
        {
            //No object with the tag 'Player' was found
            Debug.Log(e.ToString() + "\nNo 'Player' object was found.  Did you forget to tag your player as 'Player'?");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            //rotate ourself to face the player
            transform.LookAt(player.transform);

            //move forward (towards the player) at the user specified speed
            transform.Translate(Time.deltaTime * speed * transform.forward, Space.World);
        }

        //destroy the object after the "zombie-hit" audio clip finishes
        if (bDestroy && !audioSource.isPlaying)
        {
            //destroy the enemy
            Destroy(gameObject);
        }
    }


        private void OnTriggerEnter(Collider other)
    {
        //get the GO associated with the thing we collided with.
        GameObject objectWeCollidedWith = other.gameObject;

        Debug.Log("Enemy Hit " + objectWeCollidedWith.name);

        if (objectWeCollidedWith.tag == "Phaser")
        {
            //play the yell sound
            audioSource.Play();

            //mark ourselves for destruction when the audio clip ends
            bDestroy = true;

        }

    }
}
