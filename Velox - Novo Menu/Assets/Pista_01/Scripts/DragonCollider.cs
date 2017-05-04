using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonCollider : MonoBehaviour {

    public Animator anim;
    public AudioSource rugidoDoDragao;

    void Awake() {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other) {
        string tag = other.gameObject.tag;

        if(tag == "DragonAtack") {
            Atack();
        }

        if(tag == "DragonRugido") {
            rugidoDoDragao.Play();
        }
    }

    void OnTriggerExit(Collider other) {
        anim.SetBool("IsFlying", true);
    }

    void Atack() {

        anim.SetBool("IsFlying", false);
        anim.SetTrigger("Atack");
    }
}
