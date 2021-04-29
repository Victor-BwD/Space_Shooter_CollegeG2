using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    float speed;//velocidade da bala
    Vector2 _direction;//a direção da bala
    bool isReady;//para saber quando a bala está pronta

    void Awake()
    {
        speed = 6f;
        isReady = false;
    }

	// Use this for initialization
	void Start () {
		
	}
	

    public void SetDirection(Vector2 direction)
    {
        _direction = direction.normalized;

        isReady = true;
    }

	// Update is called once per frame
	void Update ()
    {
        if (isReady)
        {
            //pega a posição atual da bala
            Vector2 position = transform.position;

            //computa a nova posição da bala
            position += _direction * speed * Time.deltaTime;

            //atualiza a posição da bala
            transform.position = position;

            //remove a bala do game se sair da tela
            //parte de baixo da tela
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            //parte de cima da tela
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

            //se a bala sair da tela, ela é destruida
            if((transform.position.x < min.x) || (transform.position.x > max.x) || (transform.position.y < min.y) || (transform.position.y > max.y))
            {
                Destroy(gameObject);
            }
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        //detecta colisão da bala do inimigo com o jogador
        if (col.tag == "PlayerShipTag")
        {
            Destroy(gameObject);
        }
    }
}
