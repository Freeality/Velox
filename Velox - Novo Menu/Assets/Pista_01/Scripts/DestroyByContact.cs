using UnityEngine;
using System.Collections;

public class DestroyByContact:MonoBehaviour {
    // public GameObject explosion;
    public float forca = 20.0f;
    public float raioDaExplosao = 5.0f;

    private Rigidbody rb;

    void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other) {

        if(other.tag == "Player") {
            // Instantiate(explosion, transform.position, transform.rotation);
            // Destroy(gameObject);

            Vector3 pontoMaisPerto = other.ClosestPointOnBounds(transform.position);

            rb.AddExplosionForce(forca, pontoMaisPerto, raioDaExplosao, 0.5f, ForceMode.Impulse
                );

        }
    }
}
