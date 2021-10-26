using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject Personagem;
    public GameObject[] Backgrounds;

    private Vector3 PersonagemPos;
    // Start is called before the first frame update
    void Start()
    {
       PersonagemPos = Personagem.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 personagemPosAtual = Personagem.transform.position;
        float diffX = this.PersonagemPos.x - personagemPosAtual.x;
        if (diffX != 0)
        {
            for (int i = 0; i < Backgrounds.Length; i++)
            {
                Vector3 pos = Backgrounds[i].transform.position;
                pos.x = Backgrounds[i].transform.position.x + diffX / (Backgrounds.Length - i);
                Backgrounds[i].transform.position = pos;
            }
        }
        this.PersonagemPos = personagemPosAtual;
    }
}
