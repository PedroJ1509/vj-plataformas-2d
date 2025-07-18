using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalController : MonoBehaviour
{
    public string nombreEscenaJuego = "Scene1"; // Cambia por el nombre real de la escena de juego

    public void Jugar()
    {
        SceneManager.LoadScene(nombreEscenaJuego);
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Salir del juego"); // Solo visible en editor
    }
}
