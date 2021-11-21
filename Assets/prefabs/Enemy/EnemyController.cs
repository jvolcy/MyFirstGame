using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
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
        }
    }
}
