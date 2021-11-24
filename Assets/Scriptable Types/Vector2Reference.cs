using UnityEngine;

/*
 class Vector2Reference
This helper class provides a way to leverage the scriptable Vector2Variable while
making it possible to use a local Vector2 if the scriptable is not connected.
This is helpful for debugging and makes for modular design in the sense that
modules that use this have no absolute dependency on a scripted type that may
or may not be present.
*/
public class Vector2Reference
{
    Vector2 mDefaultValue;        //the local default value used if the scriptable Vector2 is null
    public Vector2Variable mVariable; //a reference to a scriptable Vector2

    //constructor
    public Vector2Reference(Vector2Variable variable, Vector2 defaultValue)
    {
        mVariable = variable;   //Vector2Variable
        mDefaultValue = defaultValue;   //local Vector2
    }

    //value property
    public Vector2 value
    {
        get
        {
            //if the scriptable is not null, return it.  Otherwise, return the local Vector2
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
