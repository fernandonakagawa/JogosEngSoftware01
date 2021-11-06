using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour
{
    //tempo
    public GameObject TextoTempo;
    public float Tempo;
    private float TempoAtual;
    public float AumentoTempo;
    private bool Pausado;
    //Panels
    public GameObject PanelGameOver;
    public GameObject TextoGameOver;
    //Posições
    private Vector3 PosicaoInicial;
    private Quaternion RotacaoInicial;
    //Bandeiras
    private List<GameObject> Bandeiras;
    public int QuantidadeBandeiras;
    private int QtdBandeirasColetadas;

    // Start is called before the first frame update
    void Start()
    {
        this.Pausado = false;
        this.PosicaoInicial = this.transform.position;
        this.RotacaoInicial = this.transform.rotation;
        this.TempoAtual = Tempo;
        this.Bandeiras = new List<GameObject>();
        this.QtdBandeirasColetadas = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.Pausado)
        {
            this.TempoAtual -= Time.deltaTime;
            TextoTempo.GetComponent<Text>().text = TempoAtual.ToString("0.0");
            if (this.TempoAtual <= 0f) GameOver();
            if (this.QtdBandeirasColetadas >= this.QuantidadeBandeiras) GameOver(true);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("bandeira"))
        {
            //Destroy(col.gameObject);
            this.Bandeiras.Add(col.gameObject);
            col.gameObject.SetActive(false);
            this.TempoAtual += this.AumentoTempo;
            this.QtdBandeirasColetadas++;
        }
    }

    private void GameOver(bool ganhou = false)
    {
        this.Pausado = true;
        Time.timeScale = 0;
        this.PanelGameOver.SetActive(true);
        if (ganhou) this.TextoGameOver.GetComponent<Text>().text = "Parabéns!";
        else
        {
            this.TextoGameOver.GetComponent<Text>().text = "Game Over";
            this.TextoTempo.GetComponent<Text>().text = "0.0";
        }
    }

    public void ReiniciarClick()
    {
        this.Pausado = false;
        Time.timeScale = 1;
        this.PanelGameOver.SetActive(false);
        this.TempoAtual = this.Tempo;
        this.transform.position = this.PosicaoInicial;
        this.transform.rotation = this.RotacaoInicial;
        foreach(GameObject g in this.Bandeiras)
        {
            g.SetActive(true);
        }
        this.Bandeiras.Clear();
        this.QtdBandeirasColetadas = 0;
    }

}
