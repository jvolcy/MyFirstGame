/*
 class IntReference
This helper class provides a way to leverage the scriptable IntVariable while
making it possible to use a local int if the scriptable is not connected.
This is helpful for debugging and makes for modular design in the sense that
modules that use this have no absolute dependency on a scripted type that may
or may not be present.
*/
public class IntReference
{
    int mDefaultValue;        //the local default value used if the scriptable int is null
    public IntVariable mVariable; //a reference to a scriptable int

    //constructor
    public IntReference(IntVariable variable, int defaultValue)
    {
        mVariable = variable;   //IntVariable
        mDefaultValue = defaultValue;   //local int
    }

    //value property
    public int value
    {
        get
        {
            //if the scriptable is not null, return it.  Otherwise, return the local int
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
