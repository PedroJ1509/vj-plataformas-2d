using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Temporizador : MonoBehaviour
{
    public float tiempoInicial = 120f; // 2 minutos en segundos
    public Text textoTiempo; // Referencia al texto UI que muestra el tiempo
    public GameObject jugador;           // Referencia al jugador para desactivar controles, etc.

    public GameObject perdisteLabel;
    public GameObject botonReiniciar;

    public AudioSource audioFondo;      // AudioSource que reproduce la música de fondo
    public AudioClip soundGameOver;     // Clip que quieres reproducir al perder

    private float tiempoRestante;
    private bool tiempoTerminado = false;

    void Start()
    {
        tiempoRestante = tiempoInicial;
    }

    void Update()
    {
        if (tiempoTerminado) return;

        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            ActualizarTexto();
        }
        else
        {
            tiempoRestante = 0;
            tiempoTerminado = true;
            PerderPorTiempo();
        }
    }

    void ActualizarTexto()
    {
        int minutos = Mathf.FloorToInt(tiempoRestante / 60);
        int segundos = Mathf.FloorToInt(tiempoRestante % 60);
        textoTiempo.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    void PerderPorTiempo()
    {
        // Mostrar mensaje "Perdiste"
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

        // Opcional: desactivar controles del jugador
        // jugador.GetComponent<JugadorMovimiento>().enabled = false;

        // Opcional: pausar el juego
        // Time.timeScale = 0f;
    }
}
