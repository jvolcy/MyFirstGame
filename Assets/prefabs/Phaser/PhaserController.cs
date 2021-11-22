using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaserController : MonoBehaviour
{
    public float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move forward
        transform.position += Time.deltaTime * speed * transform.forward;
    }
}
