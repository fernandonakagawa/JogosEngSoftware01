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

    //Andar
    private bool andandoEsquerda;
    private bool andandoDireita;

    //Posição inicial
    private Vector3 posicaoInicial;

    //morreu
    private bool isDead;

    //tiro
    public GameObject tiroCogumelo;
    private GameObject tiroAtual;

    void Start()
    {
        //this.velocidade = 1;
        this.qtdPulos = MAX_PULOS;
        this.qtdCogumelos = 0;
        this.andandoEsquerda = false;
        this.andandoDireita = false;
        this.posicaoInicial = this.transform.position;
        this.isDead = false;
        AtualizarHUD();
    }

    // Update is called once per frame
    void Update()
    {
        VerificaAndar();
        VerificaPular();
        VerificaMorte();
        VerificaAtirar();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("chao"))
        {
            this.qtdPulos = MAX_PULOS;

            this.GetComponent<Animator>().SetBool("estaPulando", false);
        }
        if (col.collider.CompareTag("zumbi")) this.isDead = true;
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
    public void VerificaAndar()
    {
        if (Input.GetKey(KeyCode.D) || andandoDireita) AndarDireita();
        else if (Input.GetKey(KeyCode.A) || andandoEsquerda) AndarEsquerda();
        else this.GetComponent<Animator>().SetBool("estaCorrendo", false);
    }
    public void AndarDireitaEnter() { this.andandoDireita = true; }
    public void AndarDireitaExit() { this.andandoDireita = false; }
    public void AndarDireita()
    {
        Vector3 posicao = this.transform.position;
        posicao.x += velocidade;
        this.transform.position = posicao;

        //Animação
        this.GetComponent<Animator>().SetBool("estaCorrendo", true);
        this.GetComponent<SpriteRenderer>().flipX = false;
    }
    public void AndarEsquerdaEnter(){this.andandoEsquerda = true;}
    public void AndarEsquerdaExit(){this.andandoEsquerda = false;}
    public void AndarEsquerda()
    {
        Vector3 posicao = this.transform.position;
        posicao.x -= velocidade;
        this.transform.position = posicao;

        this.GetComponent<Animator>().SetBool("estaCorrendo", true);
        this.GetComponent<SpriteRenderer>().flipX = true;
    }
    public void VerificaPular()
    {
        if (Input.GetKey(KeyCode.W)) Pular();
    }
    public void Pular()
    {
        if (this.qtdPulos > 0)
        {
            this.qtdPulos--;
            Vector2 forca = new Vector2(0f, this.forcaPulo);
            this.GetComponent<Rigidbody2D>().AddForce(forca, ForceMode2D.Impulse);

            this.GetComponent<Animator>().SetBool("estaPulando", true);
        }
    }
    public void VerificaMorte()
    {
        Vector3 posicaoAtual = this.transform.position;
        if (posicaoAtual.y < -15f) this.isDead = true;

        if (this.isDead)
        {
            this.transform.position = this.posicaoInicial;
            this.isDead = false;
        }
    }
    public void VerificaAtirar()
    {
        if (Input.GetKey(KeyCode.Space) && this.tiroAtual == null)
        {
            Vector3 posicaoTiro = this.transform.position;
            if (this.GetComponent<SpriteRenderer>().flipX) posicaoTiro.x -= 1f;
            else posicaoTiro.x += 1f;
            this.tiroAtual = Instantiate(this.tiroCogumelo, posicaoTiro, this.transform.rotation);
            if (this.GetComponent<SpriteRenderer>().flipX) 
                this.tiroAtual.GetComponent<TiroCogumelo>().forcaX *= -1;

        }
    }
}
