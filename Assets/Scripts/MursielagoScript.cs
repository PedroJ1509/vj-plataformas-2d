using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MursielagoScript : MonoBehaviour
{
    public GameObject Jugador;
    public float speed = 3f;
    public float maxDistance = 5f;
    private int Health = 1;


    private Rigidbody2D rb;
    private bool cayendo = false;

    public AudioClip SoundDestruido;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Sin gravedad al inicio para que no caiga solo
    }

    private void Update()
    {
        if (Jugador == null) return;

        float distance = Vector3.Distance(transform.position, Jugador.transform.position);

        if (distance <= maxDistance)
        {
            // Calcula la dirección hacia el jugador
            Vector3 direction = Jugador.transform.position - transform.position;

            // Normaliza la dirección para usar solo la dirección sin importar la distancia
            Vector3 moveDir = direction.normalized;

            // Mueve el murciélago hacia el jugador suavemente
            transform.position += moveDir * speed * Time.deltaTime;

            // Controla la dirección del sprite según la posición del jugador (flip)
            if (direction.x >= 0.0f)
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            else
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else
        {
            // Opcional: puedes añadir aquí comportamiento cuando no sigue (quedarse quieto, patrullar, etc.)
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        JugadorMovimiento jugador = collision.collider.GetComponent<JugadorMovimiento>();
        MursielagoScript mursielago = collision.collider.GetComponent<MursielagoScript>();

        if (jugador != null)
        {
            Debug.Log($"Jugador Y: {collision.transform.position.y}, Murciélago Y: {transform.position.y + 0.5f}");
            // Detectar si el jugador pisa desde arriba para hacer caer al murciélago
            if (collision.transform.position.y > transform.position.y + 0.5f)
            {
                Debug.Log($"Jugador Y: {collision.transform.position.y}, Murciélago Y: {transform.position.y + 0.5f}");
                Caer();
            }
            else
            {
                jugador.Hit();
            }
        }

        if (mursielago != null)
        {
            mursielago.Hit();
        }
    }


    public void Hit()
    {
        Health = Health - 1;
        if (Health == 0) Destroy(gameObject);
    }
    private void Caer()
    {
        cayendo = true;
        Camera.main.GetComponent<AudioSource>().PlayOneShot(SoundDestruido);
        Destroy(gameObject);
        //rb.gravityScale = 0; // Activar gravedad para que caiga
        // Opcional: puedes desactivar animaciones o scripts de movimiento aquí
    }
    public void ReiniciarJuego()
    {
        // Recarga la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Si pausaste el juego con Time.timeScale = 0; aquí lo vuelves a 1
        //Time.timeScale = 1f;
    }

}
