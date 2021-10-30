using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personagem : MonoBehaviour
{
    public GameObject TelaGameOver;
    public GameObject Cubo;
    
    private bool IsDead;
    private Vector3 PosInicial;
    
    void Start()
    {
        this.IsDead = false;
        this.PosInicial = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead)
        {
            //this.transform.position = this.PosInicial;
            TelaGameOver.SetActive(true);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("fatal"))
        {
            this.IsDead = true;
        }
        else if (col.collider.CompareTag("armadilha"))
        {
            Debug.Log("Ativou armadilha");
            this.Cubo.GetComponent<Rigidbody>().AddForce(new Vector3(5f, 7f, 8f), ForceMode.Impulse);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<Collider>().CompareTag("moeda"))
        {
            Destroy(col.gameObject);
        }
    }
}
