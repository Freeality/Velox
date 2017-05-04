using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour {

    public Transform[] path;
    public float speed = 5.0f;
    public float rotateSpeed = 1.0f;
    public float reachDist = 1.0f;
    public int currentPoint = 0;
    public bool voltaAoInicio = false;

    void Update() {
        Mover();
        Rodar();
    }

    void Rodar() {
        Vector3 targetDir = path[currentPoint].position - transform.position;
        float rotateStep = rotateSpeed * Time.deltaTime;

        // rodando
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotateStep, 0f);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    void Mover() {
        float step = speed * Time.deltaTime;
        
        Vector3 target = path[currentPoint].position;

        // movendo
        float dist = Vector3.Distance(target, transform.position);
        transform.position = Vector3.MoveTowards(transform.position,
            target, step);

        // transform.position += dir * Time.deltaTime * speed;

        if(dist <= reachDist && currentPoint < (path.Length - 1)) {
            currentPoint++;
        }

        // volta para o início
        if(voltaAoInicio && currentPoint == (path.Length - 1)) {
            currentPoint = 0;
        }
    }

    void OnDrawGizmos() {
        if (path.Length > 0) {
            for(int i = 0; i < path.Length; i++) {
                if(path[i] != null) {
                    Gizmos.DrawSphere(path[i].position, reachDist);
                }
            }
        }  
    }
}
