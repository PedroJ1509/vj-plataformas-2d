using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EstrellaScript : MonoBehaviour
{
    public GameObject jugador;
    public GameObject mensajeGanaste;    // UI Text o Panel que dice "Ganaste"
    public GameObject botonSiguienteNivel;  // Botón para avanzar nivel
    public AudioSource audioFondo;       // AudioSource de la música de fondo
    public AudioClip sonidoVictoria;     // Clip para la victoria

    private bool juegoTerminado = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (juegoTerminado) return;

        if (collision.gameObject == jugador)
        {
            juegoTerminado = true;

            // Destruye la estrella (este objeto)
            Destroy(gameObject);

            // Luego de destruir, muestra el mensaje y cambia el sonido
            MostrarVictoria();
        }
    }

    private void MostrarVictoria()
    {
        // Mostrar mensaje
        mensajeGanaste.SetActive(true);

        // Mostrar botón
        botonSiguienteNivel.SetActive(true);

        // Cambiar música
        if (audioFondo != null && sonidoVictoria != null)
        {
            audioFondo.Stop();
            audioFondo.clip = sonidoVictoria;
            audioFondo.loop = false;
            audioFondo.Play();
        }

        // Opcional: pausar el juego o desactivar controles del jugador
        // Time.timeScale = 0f;
    }

    // Método para avanzar al siguiente nivel (para asignar al botón)
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
            Debug.Log("No hay más niveles");
            // Aquí puedes hacer algo si ya no hay más niveles
        }
    }
}
