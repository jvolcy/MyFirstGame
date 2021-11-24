/*
 class FloatReference
This helper class provides a way to leverage the scriptable FloatVariable while
making it possible to use a local float if the scriptable is not connected.
This is helpful for debugging and makes for modular design in the sense that
modules that use this have no absolute dependency on a scripted type that may
or may not be present.
*/
public class FloatReference
{
    float mDefaultValue;        //the local default value used if the scriptable float is null
    public FloatVariable mVariable; //a reference to a scriptable float

    //constructor
    public FloatReference(FloatVariable variable, float defaultValue)
    {
        mVariable = variable;   //FloatVariable
        mDefaultValue = defaultValue;   //local float
    }

    //value property
    public float value
    {
        get
        {
            //if the scriptable is not null, return it.  Otherwise, return the local float
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
