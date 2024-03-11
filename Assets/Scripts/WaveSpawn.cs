using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveSpawn : MonoBehaviour
{

	public GameManager gm;
	public int WaveSize;
	public GameObject EnemyPrefab;
	public float EnemyInterval;
	public List<Transform> spawnPoints;
	public float startTime;
	public Transform[] WayPoints;
	public int enemyCount = 0;
	public bool waveInProgress = false;
	public bool needToWait = false;
	public bool startSpawning = false;
	public Transform spawnPoint;
	public List<GameObject> wave1;
	public List<GameObject> wave2;
	public List<GameObject> wave3;
	public List<GameObject> boss;
	public Transform[] way1;
	public Transform[] way2;
	public Transform[] way3;
	public Transform[] way4;
	public Transform[] way5;
	public Transform[] way6;
	Transform[][] allWays = new Transform[6][];
	
	void Start()
	{
		//InvokeRepeating("SpawnEnemy", startTime, EnemyInterval);
		gm.waveCount = 0;
		gm.newWaveCount = 0;
		allWays[0] = way1;
		allWays[1] = way2;
		allWays[2] = way3;
		allWays[3] = way4;
		allWays[4] = way5;
		allWays[5] = way6;
	}

	void Update()
	{
		if (needToWait)
		{
			StartCoroutine(Wait()); // Start the coroutine
		}
		if (gm.waveCount < gm.newWaveCount && !waveInProgress && !needToWait)
		{
			StartCoroutine(WaitFor());
			enemyCount = 0;
			gm.waveCount++;
			Debug.Log("wave " + gm.waveCount);
			switch (gm.waveCount)
			{
				case 1:
				case 2:
				case 3:
					gm.UpdateEnemyHP(300);
					gm.UpdateSpeedMultiplier(1);
					break;
				case 4:
				case 5:
				case 6:
					gm.UpdateEnemyHP(600);
					gm.UpdateSpeedMultiplier(0.9);
					break;
				case 7:
					gm.UpdateEnemyHP(5400);
					gm.UpdateSpeedMultiplier(0.5);
					break;
				case 8:
				case 9:
				case 10:
					gm.UpdateEnemyHP(900);
					gm.UpdateSpeedMultiplier(1.5);
					break;
				default:
					break;
			}
			switch (gm.waveCount)
			//case int n when n >= 80:
			//case var expression when (value >= 0 && value < 5):
			{
				case 1:
				case 4:
				case 8:
					WaveSize = 6;
					for (int i = 0; i < 6; i++)
					{
						GenerateWave(wave1);
						StartCoroutine(Wait()); // Start the coroutine
						StartCoroutine(WaitFor());
					}
					break;
				case 2:
				case 5:
				case 9:
					WaveSize = 6;
					for (int i = 0; i < 6; i++)
					{
						GenerateWave(wave2);
						StartCoroutine(Wait()); // Start the coroutine
						StartCoroutine(WaitFor());
					}
					break;
				case 3:
				case 6:
				case 10:
					WaveSize = 6;
					for (int i = 0; i < 6; i++)
					{
						GenerateWave(wave3);
						StartCoroutine(Wait()); // Start the coroutine
						StartCoroutine(WaitFor());
					}
					break;
				case 7:
					WaveSize = 1;
					GenerateWave(boss);
					StartCoroutine(Wait()); // Start the coroutine
					StartCoroutine(WaitFor());
					break;
				default:
					gm.AllWavesPassed();
					break;
			}
			StartCoroutine(WaitFor());
		}
		if (enemyCount == WaveSize && waveInProgress)
		{
			waveInProgress = false; // Mark the wave as finishing
									//Debug.Log("waveInProgress = true");
			StopCoroutine("SpawnEnemies"); // Stop the coroutine (if it's running)
										   //Debug.Log("Stop Coroutine(SpawnEnemies) after spawning " + EnemyPrefab);
			StartCoroutine(WaitFor()); // Start the coroutine
									   //Debug.Log("Coroutine started after stoping spawning!");
		}
	}

	public void GenerateWave(List<GameObject> enemies)
	{
		needToWait = true;
		WaveSize = 1;
		EnemyPrefab = enemies[Random.Range(0, enemies.Count)];
		WayPoints = allWays[Random.Range(0, allWays.Length)];
		spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
		waveInProgress = true;
		StartCoroutine(SpawnEnemies()); // Start spawning enemies
		gm.AddEnemie();

	}

	IEnumerator SpawnEnemies()
	{
		//while (enemyCount < WaveSize)
		{
			SpawnEnemy();
			yield return new WaitForSeconds(EnemyInterval);
		}
	}

	void SpawnEnemy()
	{
		enemyCount++;
		GameObject enemy = GameObject.Instantiate(EnemyPrefab, spawnPoint.position, Quaternion.identity) as GameObject;
		enemy.GetComponent<Enemy>().waypoints = WayPoints;
	}

	IEnumerator Wait()
	{
		Debug.Log("Coroutine waiting in wait() function...");
		yield return new WaitForSeconds(10); // Wait for specified seconds

		Debug.Log("Coroutine resumed after waiting in wait() function.");
		needToWait = false; // Mark the wave as not in progress
	}

	IEnumerator WaitFor()
	{
		Debug.Log("Coroutine waiting in waitfor() function.");
		yield return new WaitForSeconds(20); // Wait for specified seconds
		Debug.Log("Coroutine resumed after waiting in waitfor() function.");
		enemyCount = 0; // Reset enemy count
		waveInProgress = false; // Mark the wave as not in progress
		UpdateWaveCount();
	}

	void UpdateWaveCount()
	{
		if (gm.newWaveCount < 11)
		{
			gm.newWaveCount++;
		}
	}
}
/*
void Update()
{
	if (enemyCount == WaveSize)
	{
		CancelInvoke("SpawnEnemy");
		StartCoroutine(WaitFor(5)); // Start the coroutine
		Debug.Log("Coroutine started!");

	}
}

void SpawnEnemy()
{
	enemyCount++;
	GameObject enemy = GameObject.Instantiate(EnemyPrefab, spawnPoint.position, Quaternion.identity) as GameObject;
	enemy.GetComponent<Enemy>().waypoints = WayPoints;
}

public void StartWave(int amountOfEnemies, GameObject enemy, Transform[] way)
{
	enemyCount = 0;
	WaveSize = amountOfEnemies;
	EnemyPrefab = enemy;
	WayPoints = way;
	InvokeRepeating("SpawnEnemy", startTime, EnemyInterval);
}

IEnumerator WaitFor(int seconds)
{
	Debug.Log("Coroutine waiting...");
	yield return new WaitForSeconds(seconds); // Wait for 2 seconds

	Debug.Log("Coroutine resumed after waiting.");
	gm.UpdateWaveCount();
}
}*/