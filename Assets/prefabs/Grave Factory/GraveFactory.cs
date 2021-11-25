using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveFactory : MonoBehaviour
{
    public List<GameObject> gravePrefabs;
    public List<Material> graveMaterials;

    //# of graves to spawn
    public IntVariable NumGraves;
    public int DefaultNumGraves = 500;
    IntReference mNumGraves;

    //lower left corner of the play rectangle
    public Vector2Variable PlayAreaMinXY;
    public Vector2 DefaultPlayAreaMinXY = new Vector2(-400, -400);
    Vector2Reference mPlayAreaMinxy;

    //upper right corner of the play rectangle
    public Vector2Variable PlayAreaMaxXY;
    public Vector2 DefaultPlayAreaMaxXY = new Vector2(400, 400);
    Vector2Reference mPlayAreaMaxxy;

    //grave rotation angle ranges
    public Vector3Variable RotationAngleRanges;
    public Vector3 DefaultRotationAngleRanges = new Vector3(30, 360, 30);
    Vector3Reference mRotationAngleRanges;

    public Terrain terrain;

    // Start is called before the first frame update
    void Start()
    {
        //create the scriptable type references
        mNumGraves = new IntReference(NumGraves, DefaultNumGraves);
        mPlayAreaMinxy = new Vector2Reference(PlayAreaMinXY, DefaultPlayAreaMinXY);
        mPlayAreaMaxxy = new Vector2Reference(PlayAreaMaxXY, DefaultPlayAreaMaxXY);
        mRotationAngleRanges = new Vector3Reference(RotationAngleRanges, DefaultRotationAngleRanges);

        GameObject instance;

        //instantiate the enemies
        for (int i = 0; i < mNumGraves.value; i++)
        {
            //pick a random spot on the terrain
            float x = Random.Range(mPlayAreaMinxy.value.x, mPlayAreaMaxxy.value.x);
            float z = Random.Range(mPlayAreaMinxy.value.y, mPlayAreaMaxxy.value.y);

            //don't worry about the vertical placement... the ghosts can walk through walls
            Vector3 position = new Vector3(x, 10f, z);

            //instantiate a random grave at the random location.
            instance = Instantiate(gravePrefabs[Random.Range(0, gravePrefabs.Count)]);
            GraveRender gr = instance.GetComponent<GraveRender>();

            Debug.Log("Instantiated a " + instance.name);
            Debug.Log(gr.name);

            //randomly orient the grave
            gr.RandomOrient(mRotationAngleRanges.value);

            //set the grave down on the terrain
            gr.SetDown(PlayAreaMinXY.value, PlayAreaMaxXY.value, 50f, terrain.name);

            //assign a random material
            gr.SetMaterial(graveMaterials[Random.Range(0, graveMaterials.Count)]);
        }

    }

}
