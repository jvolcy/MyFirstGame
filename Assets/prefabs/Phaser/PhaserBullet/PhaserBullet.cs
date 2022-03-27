using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaserBullet : MonoBehaviour
{

    public float MaxTravel = 50f;
    public float Speed = 50f;
    float distanceTraveled = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Speed * Time.deltaTime;
        transform.Translate(transform.forward * distance, Space.World);
        distanceTraveled += distance;

        if (distanceTraveled >= MaxTravel)
        {
            Destroy(gameObject);
        }
    }
}
