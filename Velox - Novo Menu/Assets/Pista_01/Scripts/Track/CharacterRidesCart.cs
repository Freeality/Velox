/**
 * <copyright>
 * Tracks and Rails Asset Package by Zen Fulcrum
 * Copyright 2015 Zen Fulcrum LLC
 * Usage is subject to Unity's Asset Store EULA (https://unity3d.com/legal/as_terms)
 * </copyright>
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ZenFulcrum.Track
{
    /**
     * Attach to a cart to allow players to ride it.
     */
    [RequireComponent(typeof(Rigidbody))]
    class CharacterRidesCart : MonoBehaviour
    {


        [Tooltip("Character controller for the user. If none is given it will be inferred from the main camera.")]
        public Transform character;

        [Tooltip("How close do we need to be to the cart to get in?")]
        public float enterDistance = 10;

        [Tooltip("When riding, where is the user's head positioned?")]
        public Vector3 headPosition = new Vector3(0, 1.5f, 0);

        [Tooltip("When exiting, how far from the cart should the character be positioned (relative to the cart)?")]
        public Vector3 exitOffset = new Vector3(0, 2.5f, 0);


        [Tooltip("Input axis to listen for to get into the cart")]
        public string enterCartButton = "Fire1";
        [Tooltip("Input axis to listen for to get out of the cart")]
        public string exitCartButton = "Fire1";


        [Tooltip("How strong is the cart's motor? (Track pieces can provide additional boots and brakes.)")]
        public float moveForce = 20;
        [Tooltip("How cast can the cart's motor go? (Track pieces can provide additional boots and brakes.)")]
        public float moveSpeed = 20;

        public float lookSpeed = 10f;


        protected Camera myCamera;
        protected Rigidbody myRigidbody;
        protected Transform normalCameraParent;
        protected Vector3 normalHeadPosition;
        protected bool isInCart;
        protected List<MonoBehaviour> lookScripts = new List<MonoBehaviour>();

        public void Start()
        {
            myRigidbody = GetComponent<Rigidbody>();
            if (!character)
            {
                CharacterController characterController = Camera.main.GetComponentInParent<CharacterController>();
                if (!characterController)
                {
                    Debug.LogWarning("Could not find character controller and none assigned, disabling", this);
                    enabled = false;
                    return;
                }
                character = characterController.transform;
            }

            myCamera = character.GetComponentInChildren<Camera>();

            if (!myCamera)
            {
                Debug.LogWarning("Could not find camera in character, disabling", this);
                enabled = false;
                return;
            }

            normalCameraParent = myCamera.transform.parent;
            normalHeadPosition = myCamera.transform.localPosition;

            var mouseLookClass = Type.GetType("MouseLook");
            if (mouseLookClass != null)
            {
                foreach (Component ml in myCamera.GetComponentsInChildren(mouseLookClass))
                {
                    var mb = ml as MonoBehaviour;
                    if (mb) lookScripts.Add(mb);
                }
            }
        }

        private float lookUpDown;
        public void Update()
        {
            if (!isInCart && !string.IsNullOrEmpty(enterCartButton) && Input.GetButtonDown(enterCartButton))
            {

                //We need to use RaycastAll to make sure we can't hit our own feet
                RaycastHit nearest = new RaycastHit();
                var nearestDist = float.PositiveInfinity;
                foreach (var hit in Physics.RaycastAll(myCamera.transform.position, myCamera.transform.forward, enterDistance))
                {
                    if (hit.transform == character) continue;
                    var d = Vector3.Distance(myCamera.transform.position, hit.point);
                    if (d < nearestDist)
                    {
                        nearest = hit;
                        nearestDist = d;
                    }
                }

                if (nearest.transform == transform) EnterCart();
            }
            else if (isInCart && !string.IsNullOrEmpty(exitCartButton) && Input.GetButtonDown(exitCartButton))
            {
                ExitCart();
            }

            if (isInCart)
            {
                //mouse look
                //This uses the same concepts as Unity's (pre 5.2) included MouseLook script
                var dLeft = lookSpeed * Input.GetAxis("Mouse X");
                var dUp = -lookSpeed * Input.GetAxis("Mouse Y");

                lookUpDown += dUp;
                lookUpDown = Mathf.Clamp(lookUpDown, -90, 90);

                var angles = myCamera.transform.localEulerAngles;
                angles.y += dLeft;
                angles.x = lookUpDown;
                myCamera.transform.localEulerAngles = angles;
            }
        }

        public void FixedUpdate()
        {
            if (!isInCart)
            {
                return;
            }

            var wantDir = Input.GetAxis("Vertical");
            var currentDir = Vector3.Dot(transform.forward, myRigidbody.velocity);
            if (wantDir > 0 && currentDir < moveSpeed)
            {
                myRigidbody.AddForce(transform.forward * wantDir * moveForce);
            }
            else if (wantDir < 0 && currentDir > -moveSpeed)
            {
                myRigidbody.AddForce(transform.forward * wantDir * moveForce);
            }
        }

        public void EnterCart()
        {
            if (isInCart) return;

            //Stop the character controller and move the camera into the cart.
            myCamera.transform.parent = transform;
            myCamera.transform.localPosition = headPosition;
            myCamera.transform.localRotation = Quaternion.identity;
            character.gameObject.SetActive(false);

            //disable any look scripts on the camera.
            lookScripts.ForEach(x => x.enabled = false);


            isInCart = true;
        }

        public void ExitCart()
        {
            if (!isInCart) return;

            //Put the camera back where it belongs
            //Move the char controller to the cart

            myCamera.transform.parent = normalCameraParent;
            myCamera.transform.localPosition = normalHeadPosition;
            character.gameObject.SetActive(true);

            lookScripts.ForEach(x => x.enabled = true);

            //Rotate and teleport things to the right places
            character.transform.position = transform.position + (transform.rotation * exitOffset);
            var camRot = myCamera.transform.eulerAngles;
            myCamera.transform.localEulerAngles = new Vector3(camRot.x, 0, 0);
            character.transform.eulerAngles = new Vector3(0, camRot.y, 0);

            isInCart = false;
        }

    }
}
