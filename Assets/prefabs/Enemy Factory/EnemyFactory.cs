using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int numEnemies = 10;
    public float Xmin = -500f;
    public float Xmax = 500f;
    public float Zmin = -500f;
    public float Zmax = 500f;

    // Start is called before the first frame update
    void Start()
    {
        float XRange = Xmax - Xmin;
        float ZRange = Zmax - Zmin;

        for (int i = 0; i < numEnemies; i++)
        {
            //pick a random spot on the terrain
            float x = Random.Range(Xmin, Xmax);
            float z = Random.Range(Zmin, Zmax);

            //don't worry about the vertical placement... the ghosts can walk through walls
            Vector3 position = new Vector3(x, 0f, z);

            //instantiate an enemy at the random location.
            Instantiate(enemyPrefab, position, Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
