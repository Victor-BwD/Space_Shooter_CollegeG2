using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondEnemy : MonoBehaviour {

    public GameObject Explosion;
    GameObject scoreUIText;

    float speed;

	// Use this for initialization
	void Start () {

        speed = 3f;
        scoreUIText = GameObject.FindGameObjectWithTag("ScoreTextTag"); // Get o score text 

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        //Captura a posição do inimigo
        Vector2 position = transform.position;

        //Processa a nova posição nova do inimigo
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);

        //Atualiza a nova posição
        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        //Se o inimigo sair da tela destroi o inimigo
        if(transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        //detecta colisão de um inimigo com o jogador, ou com a bala do jogador
        if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag"))
        {
            PlayExplosion();

            scoreUIText.GetComponent<GameScore>().Score += 50;

            Destroy(gameObject);
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(Explosion);

        //estabelece a posição da explosão
        explosion.transform.position = transform.position;
    }
}
