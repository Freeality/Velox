using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contem informações sobre as pistas que permanecerão
/// com a mudança das cenas
/// </summary>
public class GameController : MonoBehaviour {

    public static GameController instance = null;

    [HideInInspector] public int pistaAtual = 0;
    [HideInInspector] public bool mostraStart = true;
    [HideInInspector] public bool mostraFim = false;
    [HideInInspector] public bool mostraMenu = false;

    private void Awake() {
        // verifica se uma instancia já existe
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject); // força o singleton
        }

        // não será destruído quando trocar de cena
        DontDestroyOnLoad(gameObject); 
    }

    public void MostraStart() {
        mostraStart = true;
        mostraFim = false;
        mostraMenu = false;
    }

    public void MostraMenu() {
        mostraStart = false;
        mostraFim = false;
        mostraMenu = true;
    }

    public void MostraFim() {
        mostraFim = true;
        mostraStart = false;
        mostraMenu = false;
    }
}
