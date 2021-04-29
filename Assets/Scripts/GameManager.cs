using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //Referencia aos Obejetos
    public GameObject playButton; // Referencia ao botão de playe
    public GameObject playerShip; // Referinca ao jogador
    public GameObject enemySpawner; // Referencia ao spawn de inimigos
    public GameObject SecondenemySpawner; //Segunda nave inimiga
    public GameObject gameOver;//Referencia ao sprite de game over
    public GameObject scoreText;// Referenza ao placar


    //Controle
    public enum GameManagerState
    {
           Opening,
           Gameplay,
           GameOver,


    }

    GameManagerState GMState;


	// Use this for initialization
	void Start () {

        GMState = GameManagerState.Opening;


	}
	
	// Atulizar O nosso gamemanagerstate
	void UpdateGameManegerState () {
        switch (GMState)
        {

            case GameManagerState.Opening:

                //Esconde Game over
                gameOver.SetActive(false);
                
                //Botão de play fica visivel
                playButton.SetActive(true);
                

                break;

            case GameManagerState.Gameplay:

                //Reseta o score
                scoreText.GetComponent<GameScore>().Score = 0;

                //Esconde o butão de play
                playButton.SetActive(false);

                //Ativa a nave do player e init as vidas
                playerShip.GetComponent<PlayerControl>().Init();

                //Inicia o spawn de inimigos
                enemySpawner.GetComponent<EnemySpawn>().SchedulEnemySpawner();

                SecondenemySpawner.GetComponent<SecondEnemySpawn>().SchedulEnemySpawner();


                break;

            case GameManagerState.GameOver:

                //Para o Spawn de inimigos
                enemySpawner.GetComponent<EnemySpawn>().UnschedulEnemySpawner();

                SecondenemySpawner.GetComponent<SecondEnemySpawn>().UnschedulEnemySpawner();

                //Mostra o Game Over 
                gameOver.SetActive(true);

                //Muda para o Estado inicial depois de 20s 
                Invoke("ChangeToOpeningState", 10f);



                break;

            
        }
		
	}


    //Função para configurar estado do GM
    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManegerState();

    }

    /* O botão de playe vai chamar essa função
     * Quando ele for precionado
     */ 
    public void StartGamePlay()
    {
        GMState = GameManagerState.Gameplay;
        UpdateGameManegerState();
    }

    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }
}
