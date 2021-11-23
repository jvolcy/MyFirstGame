using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaserController : MonoBehaviour
{
    //game stats scriptable types
    public IntVariable Score;

    public float speed = 40f;   //how fast the phaser moves (meters/sec)
    public float range = 30f;   //how far the phaser goes (meters)

    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //move forward
        transform.position += Time.deltaTime * speed * transform.forward;

        //calculate distance traveled
        float distance = Vector3.Distance(startPosition, transform.position);

        //terminate the phaser if we are past the range
        if (distance > range)
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
