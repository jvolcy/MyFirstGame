/*
 class BoolReference
This helper class provides a way to leverage the scriptable BoolVariable while
making it possible to use a local bool if the scriptable is not connected.
This is helpful for debugging and makes for modular design in the sense that
modules that use this have no absolute dependency on a scripted type that may
or may not be present.
*/
public class BoolReference
{
    bool mDefaultValue;        //the local default value used if the scriptable bool is null
    public BoolVariable mVariable; //a reference to a scriptable bool

    //constructor
    public BoolReference(BoolVariable variable, bool defaultValue)
    {
        mVariable = variable;   //BoolVariable
        mDefaultValue = defaultValue;   //local bool
    }

    //value property
    public bool value
    {
        get
        {
            //if the scriptable is not null, return it.  Otherwise, return the local bool
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
