using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFade : MonoBehaviour {

    private Light lt;

    void Start() {
        lt = GetComponent<Light>();
    }
	
	// Update is called once per frame
	void Update () {
        lt.range = Mathf.Lerp(lt.range, 0, Time.deltaTime);
	}
}
