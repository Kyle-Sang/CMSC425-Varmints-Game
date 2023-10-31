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

    private List<Vector3> spawnLocations;
    private bool isSpawning;
    private float timeLeft; // time left in current phase

  
    // Start is called before the first frame update
    void Start()
    { 
        enemies = new List<GameObject>();

        Vector3 baseSpawn = new Vector3(spawnRadius, 0, 0);
        spawnLocations = new List<Vector3>(numSpawnLocations);
        for(int i = 0; i < numSpawnLocations; i++)
        {
            // set 10 spawn locations at 36 degrees angled around the center
            var rotation = (360 / numSpawnLocations) * i;
            Vector3 position = Quaternion.Euler(0, rotation, 0) * baseSpawn;
            spawnLocations.Add(position);
        }
        isSpawning = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Enemies Count " + enemies.Count);
        if (enemies.Count <= 0) 
        {
            StartCoroutine(StartBuyPhase());
        } 
        else
        {
            StartCoroutine(RoundPhase());
        }
        
    }

    IEnumerator RoundPhase()
    {
        yield return null;
    }

    IEnumerator StartBuyPhase()
    {

        while (timeLeft > 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                SpawnWave();
                yield return null;
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
            int spawnIndex = i % numSpawnLocations;
            Debug.Log("Adding Enemy at location " + spawnIndex);
            GameObject newEnemy = Instantiate(enemyPrefab, spawnLocations[spawnIndex], Quaternion.identity);
            newEnemy.GetComponent<Chase>().target = GameObject.Find("Player").transform;
            enemies.Add(newEnemy); 
        }
                
        timeLeft = roundTime;
        isSpawning = false;
    }
}
