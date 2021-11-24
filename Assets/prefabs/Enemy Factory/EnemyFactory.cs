using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public List <GameObject> enemyPrefabs;

    //# of simultaneous enemies to spawn
    public IntVariable NumActiveEnemies;
    public int DefaultNumActiveEnemies = 40;
    IntReference mNumActiveEnemies;

    //lower left corner of the play rectangle
    public Vector2Variable PlayAreaMinXY;
    public Vector2 DefaultPlayAreaMinXY = new Vector2(-400, -400);
    Vector2Reference mPlayAreaMinxy;

    //upper right corner of the play rectangle
    public Vector2Variable PlayAreaMaxXY;
    public Vector2 DefaultPlayAreaMaxXY = new Vector2(400, 400);
    Vector2Reference mPlayAreaMaxxy;


    // Start is called before the first frame update
    void Start()
    {
        //create the scriptable type references
        mNumActiveEnemies = new IntReference(NumActiveEnemies, DefaultNumActiveEnemies);
        mPlayAreaMinxy = new Vector2Reference(PlayAreaMinXY, DefaultPlayAreaMinXY);
        mPlayAreaMaxxy = new Vector2Reference(PlayAreaMaxXY, DefaultPlayAreaMaxXY);

        //instantiate the enemies
        for (int i = 0; i < mNumActiveEnemies.value; i++)
        {
            //pick a random spot on the terrain
            float x = Random.Range(mPlayAreaMinxy.value.x, mPlayAreaMaxxy.value.x);
            float z = Random.Range(mPlayAreaMinxy.value.y, mPlayAreaMaxxy.value.y);

            //don't worry about the vertical placement... the ghosts can walk through walls
            Vector3 position = new Vector3(x, 0f, z);

            //instantiate a random enemy at the random location.
            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], position, Quaternion.identity);
        }
        
    }


}
