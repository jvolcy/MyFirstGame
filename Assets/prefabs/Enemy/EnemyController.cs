using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    /*
     * Usage: instantiate an enemy factory for each type of enemy that needs to be mass-produced.
     */

    public float speed = 1f;
    public AudioClip enemySoundClip;
    public AudioClip enemyHitClip;
    public float audioRange = 20f;
    public float enemyFollowRange = 30f;   //enemys will not follow unless you are within range

    GameObject player;

    AudioSource audioSource;

    //enemy state ENUM
    enum EnemyState { IDLE, ACTIVE, DYING, DEAD };
    private EnemyState enemyState;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = enemySoundClip;
        audioSource.loop = true;
        audioSource.volume = 0f;
        audioSource.Play();

        //find the player.  This should be the only GameObject tagged as 'Player'
        try
        {
            player = GameObject.FindGameObjectWithTag("Player");
            enemyState = EnemyState.IDLE;
        }
        catch (System.Exception e)
        {
            //No object with the tag 'Player' was found
            Debug.Log(e.ToString() + "\nNo 'Player' object was found.  Did you forget to tag your player as 'Player'?");
            enemyState = EnemyState.DEAD;
        }

    }


    // Update is called once per frame
    void Update()
    {
        /*
         * The Enemy cycles through a number of states: IDLE (out of range), ACTIVE 
         * (in pursuit of player), DYING and DEAD.  We will assume that enemyFollowRange > audioRange.
         */

        //calculate the distance to the player and set the volume accordingly
        float distance = Vector3.Distance(transform.position, player.transform.position);

        switch (enemyState)
        {
            //we are out of range
            case EnemyState.IDLE:
                if (distance <= enemyFollowRange) enemyState = EnemyState.ACTIVE;
                break;

            case EnemyState.ACTIVE:
                //rotate ourself to face the player
                transform.LookAt(player.transform);

                //move forward (towards the player) at the user specified speed
                transform.Translate(Time.deltaTime * speed * transform.forward, Space.World);

                if (distance < audioRange)
                {
                    //when the distance is 0, set the volume to 1 (max).  When the distance = audioRange, set the volume to 0 (min)
                    audioSource.volume = (audioRange - distance) / audioRange;
                }
                else
                {
                    audioSource.volume = 0f;
                }

                //if the player runs too far away, go back to idleing
                if (distance > enemyFollowRange) enemyState = EnemyState.IDLE;

                break;

            case EnemyState.DYING:
                //destroy the object after the "zombie-hit" audio clip finishes
                if (!audioSource.isPlaying)
                {
                    enemyState = EnemyState.DEAD;
                }
                break;

            case EnemyState.DEAD:
                //destroy the enemy
                Destroy(gameObject);
                break;

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
            audioSource.clip = enemyHitClip;
            audioSource.loop = false;
            audioSource.Play();

            //mark ourselves for destruction when the audio clip ends
            enemyState = EnemyState.DYING;

        }

    }
}
