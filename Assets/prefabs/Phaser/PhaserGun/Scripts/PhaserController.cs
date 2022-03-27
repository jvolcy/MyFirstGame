using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.XR.Interaction.Toolkit;

public class PhaserController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject barrel;
    //public GameObject HandController;
    //XRInteractorLineVisual interactorLineVisual;
    //XRBaseControllerInteractor baseInteractor;

    //bool interactorLineVisualState;
    //Rigidbody rigidbody;
    BoxCollider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        //interactorLineVisual = HandController.GetComponentInParent<XRInteractorLineVisual>();

        //get the base interactor
        //baseInteractor = HandController.GetComponentInParent<XRBaseControllerInteractor>();
        //Debug.Log(baseInteractor);
        //rigidbody = GetComponent<Rigidbody>();
        boxCollider = GetComponentInChildren<BoxCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        /*
        //if we have an object
        if (baseInteractor.selectTarget)
        {   //check if the object is a phaser
            //turn off the ray interactor visual when we have the phaser.
            //turn on the ray interactor visual when we don't have the phaser.
            interactorLineVisual.enabled = (baseInteractor.selectTarget.name != "Phaser");
        }
        else
        {
            //no object, show XR interactable line
            interactorLineVisual.enabled = true;
        }
        */
    }

    public void Shoot()
    {
        //Debug.Log("shoot!");
        Instantiate(bulletPrefab, barrel.transform.position, barrel.transform.rotation);
    }

    public void SelectEnter()
    {
        //store the current state of the interactorLineVisual
        //interactorLineVisualState = interactorLineVisual.enabled;

        //turn off the ray interactor visual when we have the phaser.
        //interactorLineVisual.enabled = false;

        //when we grab the phaser gun, make it kinematic
        //rigidbody.isKinematic = true;
        boxCollider.enabled = false;
    }

    public void SelectExit()
    {
        //restore the ray interactor visual when we have the phaser.
        //interactorLineVisual.enabled = interactorLineVisualState;

        //interactorLineVisual.enabled = true;


        //when we let the phaser go, make it non-kinematic
        //rigidbody.isKinematic = false;
        boxCollider.enabled = true;

    }
}
