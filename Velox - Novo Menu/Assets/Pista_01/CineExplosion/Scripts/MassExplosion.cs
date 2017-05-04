using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassExplosion : MonoBehaviour {
    public GameObject explosion;
    public float forca = 20.0f;
    public float raioDaExplosao = 5.0f;

    void OnTriggerEnter(Collider other) {

        if(other.tag == "ExplosiveTrigger") {

            Instantiate(explosion, transform.position, transform.rotation);

            Collider[] colliders = Physics.OverlapSphere(other.transform.position, raioDaExplosao);

            foreach(Collider c in colliders) {
                Rigidbody rb = c.GetComponent<Rigidbody>();
                if(rb == null) continue;

                rb.AddExplosionForce(forca, other.transform.position, raioDaExplosao, 0.5f, ForceMode.Impulse);
            }

            Destroy(gameObject);
        }
    }
}
