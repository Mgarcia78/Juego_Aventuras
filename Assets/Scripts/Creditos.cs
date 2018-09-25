using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creditos : MonoBehaviour {
    private float tiempo = 32;

    // Update is called once per frame
    void Update () {
        tiempo -= Time.deltaTime;
        if (tiempo < 0)
        {
            Debug.Log("Saliendo del juego...");
            Application.Quit();
        }
    }
}
