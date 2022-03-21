How to use
==========
If you are creating a single hand, simply instantiate from the prefab and use.
If you need to instantiate two (or more) hands, instantiate each from the prefab, then duplicate the animation controller for each hand (beyond the first).  Then, in the Animator component, set the "Controller" to one of the newly duplicated controllers.  Most likely, no two hands should use the same controller (unless you want the hands to be synchronized).

Negate the prefab's X scale to generate a left hand.  



How it works
============
The rigged hand comes with a pair of animation poses: a "resting" pose and a "clinched" pose.  A pair of avatar masks are used to layer the clinching animation onto the rest animation.  The "activation" mask, masks everything except the index finger.  Thus, the resulting animation affects only the index finger, corresponding to the VR "activation" action.  The "select" mask masks everything except the middle, ring and pinky fingers.  Applying this mask results in an animation of these fingers, corresponding to the VR "select" action.

"Activation" can be thought of as "triggering" gesture and "Selecting" can be thought of as "gripping" gesture.

The animation of the hand is controlled by changing the weight of each mask.  If, for example, the weight of the activation layer is 0, then the index finger is in the rest position.  If the weight of the activation layer is 1, then the index finger will be in the clinched position.  A weight of 0.5 puts the finger in the midway clinched position.  The selection weight works the same way for the middle, ring and pinky fingers.

The default hand a the right hand.  You can generate a left hand simply by scaling the x axis by -1.  Don't forget to create a new duplicate of the animation controller if you are making a new hand.

A few skin tones are provided in the Materials folder.
