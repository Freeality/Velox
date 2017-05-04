using UnityEngine;
using System.Collections;


public class Loader:MonoBehaviour {
    public GameObject gameController;          //GameManager prefab to instantiate.
    public GameObject soundManager; // SoundManager prefab to instantiate

    void Awake() {
        //Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
        if(GameController.instance == null) {
            //Instantiate gameManager prefab
            Instantiate(gameController);
        }

        if (SoundManager.instance == null) {
            Instantiate(soundManager);
        }
    }
}
