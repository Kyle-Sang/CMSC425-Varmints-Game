using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public List<GameObject> enemies; // references to enemy Game Objects in current wave
    public int enemiesToSpawn = 10;
    public float spawnRadius;
    public int numSpawnLocations;
    public int secondDelay;
    public float roundTime; // should we terminate around after t
    public float buyTime;

    private float timeLeft; // time left in current phase

  
    // Start is called before the first frame update
    void Start()
    { 
        enemies = new List<GameObject>();
        Vector3 baseSpawn = new Vector3(spawnRadius, 0, 0);
        StartCoroutine(StartBuyPhase());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Enemies Count " + enemies.Count);
    }

    IEnumerator StartRoundPhase()
    {
        while (timeLeft > 0) {
            if(enemies.Count <= 0) 
            {
                timeLeft = buyTime;
                yield return StartCoroutine(StartBuyPhase());
            }

            timeLeft -= Time.timeDelta;
        }

        yield return null;
    }

    IEnumerator StartBuyPhase()
    {

        while (timeLeft > 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                SpawnWave();
                yield return StartCoroutine(StartRoundPhase());
            }
            timeLeft -= Time.deltaTime;
        }

        SpawnWave();
        yield return null;
    }

    void SpawnWave()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Random rand = new System.Random();
            double radius = rand.nextDouble() * (outerRadius - innerRadius) + innerRadius;
            double rotation = rand.Next(0, 360) // represents degrees

            Vector3 spawn = new Vector3(radius, 0, 0);
            spawn *= new Quaternion(0, rotation, 0)

            GameObject newEnemy = Instantiate(enemyPrefab, spawn, Quaternion.identity);
            newEnemy.GetComponent<Chase>().target = GameObject.Find("Player").transform;
            enemies.Add(newEnemy); 
        }
                
        timeLeft = roundTime;
    }
}
