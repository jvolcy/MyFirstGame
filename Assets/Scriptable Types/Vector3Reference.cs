using UnityEngine;

/*
 class Vector3Reference
This helper class provides a way to leverage the scriptable Vector3Variable while
making it possible to use a local Vector3 if the scriptable is not connected.
This is helpful for debugging and makes for modular design in the sense that
modules that use this have no absolute dependency on a scripted type that may
or may not be present.
*/
public class Vector3Reference
{
    Vector3 mDefaultValue;        //the local default value used if the scriptable Vector3 is null
    public Vector3Variable mVariable; //a reference to a scriptable Vector3

    //constructor
    public Vector3Reference(Vector3Variable variable, Vector3 defaultValue)
    {
        mVariable = variable;   //Vector3Variable
        mDefaultValue = defaultValue;   //local Vector3
    }

    //value property
    public Vector3 value
    {
        get
        {
            //if the scriptable is not null, return it.  Otherwise, return the local Vector3
            return mVariable ? mVariable.value : mDefaultValue;
        }
        set
        {
            //set the local value
            mDefaultValue = value;

            //if the scriptable is not null, set it too
            if (mVariable) mVariable.value = value;
        }
    }

}
