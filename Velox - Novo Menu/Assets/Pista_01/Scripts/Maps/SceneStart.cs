﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SceneStart : MonoBehaviour
{
    public float speed;
    public Transform background;
    float currentSpeed = 0;

    //Use this for initialization
    /*void Start()
    *{
    *
    * }
    */

    // Update is called once per frame
    void Update()
    {
        currentSpeed += speed * Time.deltaTime;

        if (currentSpeed >= 360f)
            currentSpeed = 0;

        else
        {
            Vector3 localRotation = background.eulerAngles;
            localRotation.z = currentSpeed;
            background.eulerAngles = localRotation;
        }
    }
}
