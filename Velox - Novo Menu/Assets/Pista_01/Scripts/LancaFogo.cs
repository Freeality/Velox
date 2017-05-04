using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancaFogo : MonoBehaviour {

    public GameObject fogoDoDragao;

    void LancarFogo() {
        Instantiate(fogoDoDragao, transform.position, transform.rotation);
    }

    void OnTriggerEnter(Collider other) {
        string tag = other.gameObject.tag;

        if(tag == "DragonRugido") {
            LancarFogo();
        }
    }
}
