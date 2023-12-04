using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public List<GameObject> enemies; // references to enemy Game Objects in current wave
    public List<GameObject> enemyTypes; // List of prefabs that can be spawned

    public int maxRounds = 10;
    public float innerRadius = 20;
    public float outerRadius = 30;
    public float roundTime; // should we terminate a round after t
    public float buyTime;
    public TextMeshProUGUI text;

    private float timeLeft; // time left in current phase
    private int roundNumber = 1;
    private int roundValue = 10;

  
    // Start is called before the first frame update
    void Start()
    { 
        StartCoroutine(StartBuyPhase());
        text.SetText(timeLeft.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        text.SetText(timeLeft.ToString());
    }

    IEnumerator StartRoundPhase()
    {
        Debug.Log("Round Starting");
        timeLeft = roundTime;
        yield return new WaitWhile(() => timeLeft > 0 && enemies.Count > 0);
        DestroyAll();
        StartCoroutine(StartBuyPhase());
    }

    IEnumerator StartBuyPhase()
    {
        Debug.Log("Buy Phase Starting");
        timeLeft = buyTime;
        yield return new WaitWhile(() => timeLeft > 0 && !Input.GetKey(KeyCode.H));
        SpawnWave();
        StartCoroutine(StartRoundPhase());
    }

    void SpawnWave()
    {
        Debug.Log("Wave Spawning");
        int totalCost = roundNumber * roundValue;
        System.Random rand = new System.Random();

        // Loop while we have not reached the total cost of a round
        while (totalCost > 0) {

            // generate an index into the enemyTypes array
            int enemy = rand.Next(0, enemyTypes.Count);
            int enemyCost = enemyTypes[enemy].GetComponent<EnemyHealth>().cost;

            // If the cost of the generated enemy is too steep, go to the next largest enemy and check cost
            while (enemy > 0 && enemyCost > totalCost) {
                enemy -= 1;
                enemyCost = enemyTypes[enemy].GetComponent<EnemyHealth>().cost;
            }
            totalCost -= enemyCost;
            // Note: at the end of this loop totalCost should always equal zero since the cost of the basic 
            // enemy is 1

            // choose spawn Location
            float radius = (float) rand.NextDouble() * (outerRadius - innerRadius) + innerRadius;
            float rotation = rand.Next(0, 360); // represents degrees

            Vector3 spawn = new Vector3(radius, 0, 0);
            Quaternion q_rotation = Quaternion.Euler(0, rotation, 0);

            spawn = q_rotation * spawn;
            SpawnEnemy(enemy, spawn);
        }

        var checkCost = 0;
        foreach (GameObject enemy in enemies)
        {
            checkCost += enemy.GetComponent<EnemyHealth>().cost;
        }
        Debug.Log("Wave Cost: " + checkCost);

        timeLeft = roundTime;
        roundNumber += 1;
    }

    void SpawnEnemy(int index, Vector3 spawn)
    {
        GameObject newEnemy = Instantiate(enemyTypes[index], spawn, Quaternion.identity);
        newEnemy.GetComponent<Chase>().target = GameObject.Find("Player").transform;
        enemies.Add(newEnemy);
    }

    void DestroyAll()
    {
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        enemies.Clear();
    }
}
