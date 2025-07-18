using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraScripts : MonoBehaviour
{
    public GameObject Jugador;
    public string nombreEscenaJuego = "MenuPrincipal";
    // Update is called once per frame
    void Update()
    {
        if (Jugador != null)
        {
            Vector3 position = transform.position;
            position.x = Jugador.transform.position.x;
            transform.position = position;
        }
    }
    public void ReiniciarJuego()
    {
        // Recarga la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Si pausaste el juego con Time.timeScale = 0; aquí lo vuelves a 1
        //Time.timeScale = 1f;
    }
    public void SiguienteNivel()
    {
        int siguienteIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Verifica que exista la siguiente escena en Build Settings
        if (siguienteIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(siguienteIndex);
            Time.timeScale = 1f;  // Asegura que el tiempo se restablezca si lo pausaste
        }
        else
        {
            SceneManager.LoadScene(nombreEscenaJuego);
            // Aquí puedes hacer algo si ya no hay más niveles
        }
    }
}
