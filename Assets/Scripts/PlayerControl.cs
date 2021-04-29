using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

    public GameObject gameManager;// Referencia Ao GM

    public GameObject PlayerBullet;//prefab da bala
    public GameObject BulletPosition01;
    public GameObject BulletPosition02;
    public GameObject Explosion;
    //public GameObject audioObject;

    //Referenciar o Txt de vidas
    public Text LivesUI;

    const int MaxLives = 3;// Maximo de vidas
    int lives;//Vidas atuais

    public void Init() {

        lives = MaxLives; // set o numero de vidas no maximo

        LivesUI.text = lives.ToString(); // Atualiza o text de vidas

        transform.position = new Vector2(0, 0);//Coloca o jogador no centro da tela

        gameObject.SetActive(true); //faz ficar ativo o "gameobeject" do player

    }

    public float speed;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Input.GetKeyDown(KeyCode.Space))
        {

            gameObject.GetComponent<AudioSource>().Play();

            GameObject bullet01 = (GameObject)Instantiate(PlayerBullet);
            bullet01.transform.position = BulletPosition01.transform.position;

            GameObject bullet02 = (GameObject)Instantiate(PlayerBullet);
            bullet02.transform.position = BulletPosition02.transform.position;

        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        //baseado no input, computamos a direção do vetor...
        Vector2 direction = new Vector2(x, y).normalized;
        //chamar a função que vai computar e atualizar a posição do Player
        Move(direction);
	}

    void Move(Vector2 direction)
    {
        //encontrar o limite da tela para o movimento do Player
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); //canto inferior esquerdo
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); //canto superior direito

        max.x = max.x - 0.2225f; //subtrair a metade do tamanho do sprite do jogador
        min.x = min.x + 0.225f; //adiciona metade do tamano do sprite do jogador

        max.y = max.y - 0.285f; //subtrair ao sprite meia altura
        min.y = min.y + 0.285f; //adiciona ao sprite meia altura

        //recebe a posição atual do player
        Vector2 pos = transform.position;

        //calcula a nova posição
        pos += direction * speed * Time.deltaTime;

        //Prova real para que a nova posição não esteja fora da tela
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        //atualiza a posição do player
        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //detecta colisão do jogador com um inimigo, ou com uma bala inimiga
        if((col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag"))
        {
            PlayExplosion();

            lives--;//Retira uma vida
            LivesUI.text = lives.ToString(); //Atualiza o Text de vidas

            if (lives == 0)//Se o jogador perder todas as vidas
            {
                //Mudar o game manager para game over
                gameManager.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);

                //escode a nave
                gameObject.SetActive(false);

                //Destroy(gameObject);
            }
        }
    }

    //função para chamar a explosão
    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(Explosion);

        //estabelece a posição da explosão
        explosion.transform.position = transform.position;
    }
}
