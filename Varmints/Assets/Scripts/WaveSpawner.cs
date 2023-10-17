using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public List<GameObject> enemies; // references to enemy Game Objects in current wave
    public Vector3 spawnLocation;
    public int enemiesToSpawn;

  
    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();
        enemiesToSpawn = 10; // TODO: Make zero later
        spawnLocation = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Enemies Count " + enemies.Count);
        if (enemies.Count <= 0) 
        {
            // advance to next wave
            SpawnWave();
        }
        else
        {
            // wave is still happening
        }
    }

    void SpawnWave()
    {
        // Assume enemies is empty 
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, spawnLocation, Quaternion.identity);
            newEnemy.GetComponent<Chase>().target = GameObject.Find("Player").transform;
            enemies.Add(newEnemy); 
        }
    }
}
