using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LineController : MonoBehaviour
{
    XRInteractorLineVisual interactorLineVisual;
    XRBaseControllerInteractor baseInteractor;

    // Start is called before the first frame update
    void Start()
    {
        interactorLineVisual = GetComponent<XRInteractorLineVisual>();

        //get the base interactor
        baseInteractor = GetComponentInParent<XRBaseControllerInteractor>();
        Debug.Log(baseInteractor);

    }

    // Update is called once per frame
    void Update()
    {
        //if we have an object
        if (baseInteractor.selectTarget)
        {   //check if the object is a phaser
            //turn off the ray interactor visual when we have the phaser.
            //turn on the ray interactor visual when we don't have the phaser.
            interactorLineVisual.enabled = (baseInteractor.selectTarget.name != "PhaserGun");
        }
        else
        {
            //no object, show XR interactable line
            interactorLineVisual.enabled = true;
        }
    }
}
