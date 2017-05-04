using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanicController : MonoBehaviour {

    // Update is called once per frame
    void Update()
    {
        char command = SerialReader.GetComando();

        if (Input.GetKeyDown(KeyCode.Return) || command == 'r') {
            GameController.instance.MostraMenu();
            SceneController.NextScene("Menu");
        }
    }
}
