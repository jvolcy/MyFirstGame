using UnityEngine;


public class GraveRender : MonoBehaviour
{
    public Material material;

    // Start is called before the first frame update
    void Start()
    {
        if (material) SetMaterial(material);
    }


    public void SetMaterial(Material material)
    {
        Transform childTransform = transform.GetChild(0);
        GameObject childObject = childTransform.gameObject;

        //Debug.Log(childObject.name);
        Renderer m = childObject.GetComponent<Renderer>();
        m.material = material;
    }

    public void RandomOrient(Vector3 rotationAngleRanges)
    {
        Vector3 rotationAngles = new Vector3(Random.Range(0f, rotationAngleRanges.x), Random.Range(0f, rotationAngleRanges.y), Random.Range(0f, rotationAngleRanges.z));
        transform.Rotate(rotationAngles, Space.World);

        //we need to lower the grave so that the rotations about X and Z don't
        //leave us floating.  We will assume that the local origin is on the
        //bottom and in the middle of each grave object.
        //Assuming the object is XSize x ZSize, then rotation by x angles
        //around the x-axis
    }

    public bool SetDown(Vector2 minXZ, Vector2 maxXZ, float start_y, string terrainName, int maxNumAttempts=10)
    {
        //We need to randomly set the grave down on an uneven terrain over an
        //area defined by Set MinXZ (lower left corner) and MaxXZ (upper right
        //corner).  Set MinXZ and MaxXZ to
        //the same value if you do not want a random placement.
        //
        //1) do a raycast downward starting from a y value of start-y.
        //2) Verify that we have hit the terrain.  If not, try again until we do
        //or until we exceed maxNumAttempts.
        //We may want to place the grave down slightly below the terrain to give the illusion
        //of an anchored stone marker, but this is not currently implemented.
        //This function returns true if we did a successful placement, flase otherwise.

        bool bHit = false; //assume we were unsuccessful with the placement

        while (!bHit && maxNumAttempts > 0)
        {
            //start at start_y above the terrain
            Vector3 position = new Vector3(Random.Range(minXZ.x, maxXZ.x), start_y, Random.Range(minXZ.y, maxXZ.y));

            RaycastHit hit;
            // Cast a ray downwards
            if (Physics.Raycast(position, Vector3.down, out hit, Mathf.Infinity))
            {
                //Here, we hit something.

                //Debug.DrawRay(position, Vector3.down * hit.distance, Color.green);
                //Debug.Log("Did Hit " + hit.transform.gameObject.name);
                //Debug.Log(hit.distance);

                //did we hit the right object? (the target terrain)
                if (hit.transform.gameObject.name == terrainName)
                {
                    //place the grave on the terrain
                    transform.position = position + Vector3.down * hit.distance;
                    bHit = true;
                }
            }
 
            maxNumAttempts--;
        }

        return bHit;
    }
}
