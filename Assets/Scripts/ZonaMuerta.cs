using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaMuerta : MonoBehaviour
{
    public GameObject jugador;
    private bool juegoTerminado = false;


    public GameObject perdisteLabel;
    public GameObject botonReiniciar;

    public AudioSource audioFondo;      // AudioSource que reproduce la música de fondo
    public AudioClip soundGameOver;     // Clip que quieres reproducir al perder

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (juegoTerminado) return;

        if (collision.gameObject == jugador)
        {
            juegoTerminado = true;

            // Luego de destruir, muestra el mensaje y cambia el sonido
            Perdiste();
        }
    }
    public void Perdiste()
    {
        perdisteLabel.SetActive(true);
        botonReiniciar.SetActive(true);
        Destroy(gameObject);
        if (audioFondo != null && soundGameOver != null)
        {
            audioFondo.Stop();
            audioFondo.clip = soundGameOver;
            audioFondo.loop = false;  // Opcional: no repetir el sonido de game over
            audioFondo.Play();
        }
        //Time.timeScale = 0;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
