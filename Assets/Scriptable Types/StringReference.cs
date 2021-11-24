/*
 class StringReference
This helper class provides a way to leverage the scriptable StringVariable while
making it possible to use a local string if the scriptable is not connected.
This is helpful for debugging and makes for modular design in the sense that
modules that use this have no absolute dependency on a scripted type that may
or may not be present.
*/
public class StringReference
{
    string mDefaultValue;        //the local default value used if the scriptable string is null
    public StringVariable mVariable; //a reference to a scriptable string

    //constructor
    public StringReference(StringVariable variable, string defaultValue)
    {
        mVariable = variable;   //StringVariable
        mDefaultValue = defaultValue;   //local string
    }

    //value property
    public string value
    {
        get
        {
            //if the scriptable is not null, return it.  Otherwise, return the local string
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
