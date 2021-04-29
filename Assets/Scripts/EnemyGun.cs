using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour {

    public GameObject EnemyBullet;

	// Use this for initialization
	void Start () {

        Invoke("FireEnemyBullet", 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FireEnemyBullet()
    {
        //referencia ao jogador
        GameObject playerShip = GameObject.Find("Player");

        if (playerShip != null)//se a jogador nao está morto
        {
            //spawna uma bala inimiga
            GameObject bullet = (GameObject)Instantiate(EnemyBullet);

            //estabelece a posição inicial da bala
            bullet.transform.position = transform.position;

            //computa a direção da bala em direção ao player
            Vector2 direction = playerShip.transform.position - bullet.transform.position;

            //estabelece a direção da bala
            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }
    }
}
