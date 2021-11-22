using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

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
     
        if (objectWeCollidedWith.name == "EnemyCapsule")
        {
            Debug.Log("ouch!");
        }
    }


}
