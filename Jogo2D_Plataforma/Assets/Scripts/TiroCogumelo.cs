using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroCogumelo : MonoBehaviour
{
    //impulso do tiro
    public float forcaX;
    public float forcaY;

    //tempo de vida
    public float tempoVida;

    void Start()
    {
        Vector2 forca = new Vector2(forcaX, forcaY);
        this.GetComponent<Rigidbody2D>().AddForce(forca, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        tempoVida -= Time.deltaTime;
        if (tempoVida < 0f) Destroy(this.gameObject);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("zumbi"))
        {
            Destroy(col.gameObject);
            Destroy(this.gameObject);
        }
        else if (col.collider.CompareTag("chao")) Destroy(this.gameObject);
    }
}
