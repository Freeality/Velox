using UnityEngine;
using System.IO.Ports;
using System;

public class SerialReader:MonoBehaviour {

    public static SerialPort sp = new SerialPort("COM3", 38400);

    // Use this for initialization
    void Awake() {
        if (!sp.IsOpen) {
            sp.Open();
            sp.ReadTimeout = 50;
        }
    }

    public static char GetComando() {
        char read = ' ';
        try {
            if(sp.IsOpen) {
                read = Convert.ToChar(sp.ReadByte());
                if (read != ' ') {
                    Debug.Log("comando...." + read);
                }
            }
        }
        catch(Exception) {
            // não faz nada
        }

        return read;
    }
}