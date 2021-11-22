using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float speed = 1f;
    public AudioClip ghostSoundClip;
    public AudioClip ghostHitClip;
    public float audioRange = 20f;
    public float ghostFollowRange = 30f;   //ghosts will not follow unless you are within range

    GameObject player;

    bool bDestroy = false;  //set to true to destroy the object

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = ghostSoundClip;
        audioSource.loop = true;
        audioSource.volume = 0f;
        audioSource.Play();

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
        //calculate the distance to the player and set the volume accordingly
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (player != null && distance <= ghostFollowRange)
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
        else
        {
            if (distance < audioRange)
            {
                //when the distance is 0, set the volume to 1 (max).  When the distance = audioRange, set the volume to 0 (min)
                audioSource.volume = (audioRange - distance) / audioRange;
            }
            else
            {
                audioSource.volume = 0f;
            }
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
            audioSource.clip = ghostHitClip;
            audioSource.loop = false;
            audioSource.Play();

            //mark ourselves for destruction when the audio clip ends
            bDestroy = true;

        }

    }
}
