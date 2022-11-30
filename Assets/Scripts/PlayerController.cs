using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public float velocidad= 3;
    public float salto = 5;
    public int monedas = 0;
    public int vida = 3;
    bool tocarsuelo = true;
    Rigidbody2D player_rb;
    Transform player_tr;
    Animator player_anim;

    void Start()
    {
        player_rb = GetComponent<Rigidbody2D>();
        player_tr = GetComponent<Transform>();
        player_anim = GetComponent<Animator>();

        Text Contadorvidas = GameObject.FindWithTag("vida").GetComponent<Text>();
        Contadorvidas.text = vida + "";


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            player_rb.velocity = new Vector2(velocidad,player_rb.velocity.y);
            player_tr.rotation = new Quaternion(0, 0, 0, 0);
            player_anim.SetBool("run", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            player_rb.velocity = new Vector2(-velocidad, player_rb.velocity.y);
            player_tr.rotation = new Quaternion(0,180f,0,0);
            player_anim.SetBool("run", true);
        }
        else
        {
            player_anim.SetBool("run", false);
        }
        if (Input.GetKey(KeyCode.UpArrow) && tocarsuelo)
        {
            player_rb.velocity = new Vector2(player_rb.velocity.x,salto);
            tocarsuelo = false;
            player_anim.SetBool("Jump", true);
        }
       

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Suelo")
        {
            tocarsuelo = true;
            player_anim.SetBool("Jump", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Moneda")
        {
            monedas++;
            Destroy(collision.gameObject);
            Text ContadorMoneda = GameObject.FindWithTag("ContadorMoneda").GetComponent<Text>();
            ContadorMoneda.text = monedas + "";
        }

        if (collision.gameObject.tag == "ZonaMuerta")
        {
            Destroy(GameObject.FindWithTag("Player"));
        }

        if (collision.gameObject.tag == "Enemigo" || collision.gameObject.tag == "enemigo_obstaculo")
        {
            vida--;
            if (collision.gameObject.tag == "enemigo_obstaculo")
            {
                vida = 0;
            }
            Text Contadorvidas = GameObject.FindWithTag("vida").GetComponent<Text>();
            Contadorvidas.text = vida + "";

            if (vida <= 0)
            {
                Text opacidadGameOver = GameObject.FindWithTag("GameOver").GetComponent<Text>();
                Color coloropacidad = opacidadGameOver.color;
                coloropacidad.a = 1;
                opacidadGameOver.color = coloropacidad;

                Destroy(GameObject.FindWithTag("Player"));
            }

        }

        if (collision.gameObject.tag == "Final")
        {
            SceneManager.LoadScene(0);

        }

    }
}
