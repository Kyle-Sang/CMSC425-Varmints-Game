using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public List<GameObject> enemies; // references to enemy Game Objects in current wave
    public int enemiesToSpawn = 10;
    public float innerRadius;
    public float outerRadius;
    public float roundTime; // should we terminate around after t
    public float buyTime;
    public TextMeshProUGUI text;

    private float timeLeft; // time left in current phase

  
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
        StartCoroutine(StartBuyPhase());
    }

    IEnumerator StartBuyPhase()
    {
        Debug.Log("Buy Phase Starting");
        timeLeft = buyTime;
        yield return new WaitWhile(() => timeLeft > 0 && !Input.GetKey(KeyCode.C));
        SpawnWave();
        StartCoroutine(StartRoundPhase());
    }

    void SpawnWave()
    {
        Debug.Log("Wave Spawning");
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            System.Random rand = new System.Random();
            float radius = (float) rand.NextDouble() * (outerRadius - innerRadius) + innerRadius;
            float rotation = rand.Next(0, 360); // represents degrees

            Vector3 spawn = new Vector3(radius, 0, 0);
            Quaternion q_rotation = Quaternion.Euler(0, rotation, 0);

            spawn = q_rotation * spawn;

            GameObject newEnemy = Instantiate(enemyPrefab, spawn, Quaternion.identity);
            newEnemy.GetComponent<Chase>().target = GameObject.Find("Player").transform;
            enemies.Add(newEnemy); 
        }
                
        timeLeft = roundTime;
    }
}
