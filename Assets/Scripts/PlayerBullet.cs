using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    float speed;

	// Use this for initialization
	void Start ()
    {
        speed = 8f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Recebe a posição atual da bala
        Vector2 position = transform.position;

        //computa a nova posição da bala
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);

        //atualiza a posição da bala
        transform.position = position;

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));//parte superior direita da tela

        //se a bala sair para fora da tela pela parte de cima ela é destruida
        if(transform.position.y > max.y)
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        //detecta colisão da bala do jogador com um inimigo
        if(col.tag == "EnemyShipTag")
        {
            Destroy(gameObject);
        }
    }
}
