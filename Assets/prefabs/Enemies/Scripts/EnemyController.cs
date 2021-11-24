using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    /*
     * Usage: instantiate an enemy factory for each type of enemy that needs to be mass-produced.
     */

    //this is bit of a hack.  The # of attack animations should not be fixed.
    public IntVariable NumAttackAnimations;
    public int DefaultNumAttackAnimations = 1;
    IntReference mNumAttackAnimations;

    //seconds for enemy from shot to death
    public FloatVariable DyingTime;
    public float DefaultDyingTime = 5;
    FloatReference mDyingTime;

    //enemy travel speed
    public FloatVariable EnemyTravelSpeed;
    public float DefaultEnemyTravelSpeed = 2;
    FloatReference mEnemyTravelSpeed;

    //enemy audio range
    public FloatVariable EnemyAudioRange;
    public float DefaultEnemyAudioRange = 20f;
    FloatReference mEnemyAudioRange;

    //enemy follow range (how close the player needs to be before the enemy starts to follow)
    //enemys will not follow unless player is within range
    public FloatVariable EnemyFollowRange;
    public float DefaultEnemyFollowRange = 30f;
    FloatReference mEnemyFollowRange;
  
    //sound clips
    public AudioClip enemySoundClip;
    public AudioClip enemyHitClip;

    GameObject player;

    Animator animator;
    AudioSource audioSource;

    float deathTime;    //the future time when the enemy will be removed after being shot

    //enemy state ENUM
    enum EnemyState { IDLE, ACTIVE, DYING, DEAD };
    private EnemyState enemyState;


    // Start is called before the first frame update
    void Start()
    {
        //create the scriptable type references
        mNumAttackAnimations = new IntReference(NumAttackAnimations, DefaultNumAttackAnimations);
        mDyingTime = new FloatReference(DyingTime, DefaultDyingTime);
        mEnemyTravelSpeed = new FloatReference(EnemyTravelSpeed, DefaultEnemyTravelSpeed);
        mEnemyAudioRange = new FloatReference(EnemyAudioRange, DefaultEnemyAudioRange);
        mEnemyFollowRange = new FloatReference(EnemyFollowRange, DefaultEnemyFollowRange);

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = enemySoundClip;
        audioSource.loop = true;
        audioSource.volume = 0f;
        audioSource.Play();

        //get our animator
        animator = GetComponent<Animator>();
        animator.SetInteger("animation", 0);    //set the animation to 'idle'

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
                AdjustAudioVolume(distance);

                if (distance <= mEnemyFollowRange.value)
                {
                    //switch to one of the attack animations
                    animator.SetInteger("animation", 1 + Random.Range(0, mNumAttackAnimations.value));
                    enemyState = EnemyState.ACTIVE;
                }
                break;

            case EnemyState.ACTIVE:
                //rotate ourself to face the player
                transform.LookAt(player.transform);

                //move forward (towards the player) at the user specified speed
                transform.Translate(Time.deltaTime * mEnemyTravelSpeed.value * transform.forward, Space.World);

                AdjustAudioVolume(distance);

                //if the player runs too far away, go back to idling
                if (distance > mEnemyFollowRange.value)
                {
                    animator.SetInteger("animation", 0);    //return to the idle animation
                    enemyState = EnemyState.IDLE;
                }
                break;

            case EnemyState.DYING:
                if (Time.fixedTime > deathTime)
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


    private void AdjustAudioVolume(float distanceToPlayer)
    {
        if (distanceToPlayer < mEnemyAudioRange.value)
        {
            //when the distance is 0, set the volume to 1 (max).  When the distance = audioRange, set the volume to 0 (min)
            audioSource.volume = (mEnemyAudioRange.value - distanceToPlayer) / mEnemyAudioRange.value;
        }
        else
        {
            audioSource.volume = 0f;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        //get the GO associated with the thing we collided with.
        GameObject objectWeCollidedWith = other.gameObject;

        Debug.Log("Enemy Hit " + objectWeCollidedWith.name);

        if (objectWeCollidedWith.tag == "Phaser")
        {
            //stop playing the enemySoundClip and set the volume to 100%
            audioSource.Stop();
            audioSource.volume = 1f;

            //play the yell sound
            audioSource.clip = enemyHitClip;
            audioSource.loop = false;
            audioSource.Play();

            //switch to the dying animation
            animator.SetInteger("animation", -1);

            deathTime = Time.fixedTime + mDyingTime.value;

            //mark ourselves for destruction when the audio clip ends
            enemyState = EnemyState.DYING;

        }

    }
}
