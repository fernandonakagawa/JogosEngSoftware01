using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Personagem : MonoBehaviour
{
    //velocidade do andar
    public float velocidade;

    //pulos
    public float forcaPulo;
    const int MAX_PULOS = 1;
    private int qtdPulos;

    //HUD
    public GameObject textoCogumelos;
    private int qtdCogumelos;

    //Sons
    public AudioClip somPegarCogumelo;

    void Start()
    {
        //this.velocidade = 1;
        this.qtdPulos = MAX_PULOS;
        this.qtdCogumelos = 0;
        AtualizarHUD();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 posicao = this.transform.position;
            posicao.x += velocidade;
            this.transform.position = posicao;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 posicao = this.transform.position;
            posicao.x -= velocidade;
            this.transform.position = posicao;
        }
        if (Input.GetKey(KeyCode.W) && this.qtdPulos > 0)
        {
            this.qtdPulos--;
            Vector2 forca = new Vector2(0f, this.forcaPulo);
            this.GetComponent<Rigidbody2D>().AddForce(forca, ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("chao"))
        {
            this.qtdPulos = MAX_PULOS;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("cogumelo"))
        {
            Destroy(col.gameObject);
            this.qtdCogumelos++;
            AtualizarHUD();

            this.GetComponent<AudioSource>().PlayOneShot(somPegarCogumelo);
        }
    }

    public void AtualizarHUD()
    {
        textoCogumelos.GetComponent<Text>().text = this.qtdCogumelos.ToString();
    }
}
