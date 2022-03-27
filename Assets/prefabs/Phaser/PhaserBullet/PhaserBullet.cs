using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaserBullet : MonoBehaviour
{
    //game stats scriptable types
    public IntVariable Score;

    //how fast the phaser moves (meters/sec)
    public FloatVariable PhaserSpeed;
    public float DefaultPhaserSpeed = 40f;
    NullableReferenceVariable<float> mPhaserSpeed;


    //how far the phaser goes (meters)
    public FloatVariable PhaserRange;
    public float DefaultPhaserRange = 30f;
    NullableReferenceVariable<float> mPhaserRange;

    float distanceTraveled = 0f;

    // Start is called before the first frame update
    void Start()
    {
        mPhaserSpeed = new NullableReferenceVariable<float>(PhaserSpeed, DefaultPhaserSpeed);
        mPhaserRange = new NullableReferenceVariable<float>(PhaserRange, DefaultPhaserRange);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = mPhaserSpeed.value * Time.deltaTime;
        transform.Translate(transform.forward * distance, Space.World);
        distanceTraveled += distance;

        if (distanceTraveled >= mPhaserRange.value)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //get the GO associated with the thing we collided with.
        GameObject objectWeCollidedWith = other.gameObject;

        Debug.Log("Phaser Hit " + objectWeCollidedWith.name);

        if (objectWeCollidedWith.tag == "Enemy")
        {
            //destroy the phaser
            Destroy(gameObject);

            Score.value += 100;
        }
    }
}
