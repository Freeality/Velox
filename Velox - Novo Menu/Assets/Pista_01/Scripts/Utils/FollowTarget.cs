using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    bool lookAtCamera = true;   
    [SerializeField]
    Transform uiElement;      
    [SerializeField]
    Transform myCamera;     
    [SerializeField]
    bool rotateWithCamera;     
    [SerializeField]
    float followSpeed = 10f;
    public Calibrator calibrator;


    float distanceFromCamera;

    // Use this for initialization
    void Start()
    {
        distanceFromCamera = Vector3.Distance(uiElement.position, myCamera.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (lookAtCamera)
            uiElement.rotation = Quaternion.LookRotation(uiElement.position - myCamera.position);

        
        if (rotateWithCamera)
        {
            Vector3 targetDirection = Vector3.ProjectOnPlane(myCamera.forward, Vector3.up).normalized;
            Vector3 targetPosition = myCamera.position + targetDirection * distanceFromCamera;

            targetPosition = Vector3.Lerp(uiElement.position, targetPosition, followSpeed * Time.deltaTime);
            targetPosition.z = 0;

            if (calibrator.isRecenter)
            {
                targetPosition = Vector3.zero;
                targetPosition.y = uiElement.position.y;
                targetPosition.z = 0;
            }

            uiElement.position = targetPosition;
        }
    }
}
