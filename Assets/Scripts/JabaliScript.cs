using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JabaliScript : MonoBehaviour
{
    public GameObject Jugador;
    public float speed = 4f;           // Velocidad del enemigo corriendo
    public float maxDistance = 6f;     // Distancia máxima para seguir corriendo

    private void Update()
    {
        if (Jugador == null) return;

        float distance = Vector3.Distance(transform.position, Jugador.transform.position);

        if (distance <= maxDistance)
        {
            // Dirección hacia el jugador
            Vector3 direction = Jugador.transform.position - transform.position;

            // Normaliza la dirección para no moverlo demasiado rápido
            Vector3 moveDir = direction.normalized;

            // Mueve el enemigo hacia el jugador
            transform.position += moveDir * speed * Time.deltaTime;

            // Voltear sprite para que mire hacia el jugador
            if (direction.x >= 0.0f)
                transform.localScale = new Vector3(1f, 1f, 1f);
            else
                transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            // Se alejó, deja de correr (opcional: animación de idle)
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Intenta obtener el componente JugadorMovimiento del objeto con el que chocaste
        JugadorMovimiento jugador = collision.collider.GetComponent<JugadorMovimiento>();
        if (jugador != null)
        {
            jugador.Hit();  // Llama al método que resta vida o maneja el daño
        }
    }
}
