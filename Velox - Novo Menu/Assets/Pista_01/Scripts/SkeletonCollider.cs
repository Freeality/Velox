using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonCollider : MonoBehaviour {

    public Animator anim;
    public Transform endPoint;
    public AudioClip skeletonGrunt;

    void Awake() {
        anim = GetComponent<Animator>();
    }

    void Start() {
        anim.SetBool("Walking", true);
    }

    void FixedUpdate() {
        float distanciaParaEndPoint = Vector3.Distance(transform.position, endPoint.position);
        if (distanciaParaEndPoint < 0.5f) {
            anim.SetBool("Walking", false);
            anim.SetBool("Idle", true);
        }
    }

    void OnTriggerEnter(Collider other) {
        string tag = other.gameObject.tag;

        if (tag == "Player") {
            SoundManager.instance.PlaySingle(skeletonGrunt);
            anim.SetTrigger("Atack");
        }
    }
}
