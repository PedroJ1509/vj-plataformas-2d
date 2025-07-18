using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JugadorMovimiento : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    private float Vertical;
    private bool Grounded;
    private int Health = 1;

    public AudioClip SoundJump;
    public AudioClip SoundEspada;

    public GameObject perdisteLabel;
    public GameObject botonReiniciar;

    public AudioSource audioFondo;      // AudioSource que reproduce la música de fondo
    public AudioClip soundGameOver;     // Clip que quieres reproducir al perder

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();

    }

    void Update()
    {

        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");

        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        if (transform.position.y < -10f)  // Ajusta este valor según tu mapa
        {
            Hit();  // O llamar a Hit() varias veces
        }

        Animator.SetBool("running", Horizontal != 0.0f);
        //Animator.SetBool("jumping", Vertical != 0.0f);

        Debug.DrawRay(transform.position, Vector3.down * 0.8f, Color.red);

        if (Physics2D.Raycast(transform.position, Vector3.down,0.8f))
        {
            Grounded = true;
        }
        else Grounded = false;

        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Atacar();
        }
    }
    public void Jump()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(SoundJump);
        Rigidbody2D.AddForce(Vector2.up*JumpForce);
    }
    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal*Speed, Rigidbody2D.velocity.y);
    }
    private void Atacar()
    {
        Animator.SetTrigger("AttackTrigger");
        StartCoroutine(PlaySoundWithDelay(0.5f));
    }
    private IEnumerator PlaySoundWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Camera.main.GetComponent<AudioSource>().PlayOneShot(SoundEspada);
    }
    public void Hit()
    {
        Health = Health - 1;
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
    public void ReiniciarJuego()
    {
        // Recarga la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Si pausaste el juego con Time.timeScale = 0; aquí lo vuelves a 1
        //Time.timeScale = 1f;
    }
}
