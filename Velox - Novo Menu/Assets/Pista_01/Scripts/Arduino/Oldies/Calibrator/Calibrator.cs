  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class Calibrator : MonoBehaviour
{
    [HideInInspector]
    public bool isRecenter = false;
    // Use this for initialization
    void Start()
    {
        InputTracking.Recenter();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OVRManager.display.RecenterPose();
            InputTracking.Recenter();
            isRecenter = true;
        }

        else
            isRecenter = false;
    }
}
