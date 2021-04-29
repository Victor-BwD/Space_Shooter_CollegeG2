using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

	public GameObject Enemy;
    
	float maxSpawnRateInSeconds = 2f;

	// Use this for initialization
	void Start () 
	{
		// Invoke ("SpawnEnemy", maxSpawnRateInSeconds);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}


	void SpawnEnemy()
	{
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));

		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		GameObject anEnemy = (GameObject)Instantiate (Enemy);
		anEnemy.transform.position = new Vector2 (Random.Range (min.x, max.x), max.y);

		ScheduleNextEnemySpawn ();
	}

	void ScheduleNextEnemySpawn(){
		float SpawnInNSeconds;

		if (maxSpawnRateInSeconds > 1f) {
			SpawnInNSeconds = Random.Range (1f, maxSpawnRateInSeconds);
		} else
			SpawnInNSeconds = 1f;

		Invoke ("SpawnEnemy", SpawnInNSeconds);
	}


     //Aumentar a dificuldade
     void IncreaseSpawnRate()
     {
        if (maxSpawnRateInSeconds > 1f)
        {
            maxSpawnRateInSeconds--;
        }

        if (maxSpawnRateInSeconds == 1f)
        {
            CancelInvoke("IncreaseSpawnRate");
        }
     
      }


    //Iniciar o Spawn de enemy
    public void SchedulEnemySpawner()
    {
        Invoke("SpawnEnemy", maxSpawnRateInSeconds);

        //Aumenta o spawn rate a cada 30s
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);

    }

    //Parar o Spawn de inimigos
    public void UnschedulEnemySpawner()
    {
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");


    }

}
