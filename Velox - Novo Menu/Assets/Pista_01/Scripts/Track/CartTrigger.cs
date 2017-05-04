using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "end")
        {
            GameController.instance.MostraFim();
            SceneController.NextScene("Menu");
        }
    }
}
