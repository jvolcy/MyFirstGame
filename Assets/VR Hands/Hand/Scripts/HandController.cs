using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

//[RequireComponent(typeof(ActionBasedController))]
[RequireComponent(typeof(Animator))]

public class HandController : MonoBehaviour
{
    //weight of the "select" animation layer
    /*
    [Range(0f, 1f)]
    public float xselect;

    //weight of the "activation" animation layer
    [Range(0f, 1f)]
    public float xactivate;
    */

    //boolean: true = right hand; false = left hand
    public bool RightHand = true;

    ActionBasedController controller;
    //public GameObject HandController;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Hand Start");
        animator = GetComponent<Animator>();
        controller = GetComponentInParent<ActionBasedController>();
        transform.localScale = new Vector3(transform.localScale.x * (RightHand ? 1f : -1f), transform.localScale.y, transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        select += 0.01f;
        if (select > 1f) select = 0f;
        */
        float select = controller.selectAction.action.ReadValue<float>();
        float activate = controller.activateAction.action.ReadValue<float>();

        //set the value of the animation layers
        animator.SetLayerWeight(animator.GetLayerIndex("select"), select);
        animator.SetLayerWeight(animator.GetLayerIndex("activate"), activate);
    }

    /*
    public void Select() { select = 1f; }
    public void DeSelect() { select = 0f; }

    public void Activate() { activate = 1f; }
    public void DeActivate() { activate = 0f; }
    */
}
