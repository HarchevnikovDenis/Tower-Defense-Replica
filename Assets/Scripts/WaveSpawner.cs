using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    public Wave[] waves;
    private Transform spawnPoint;               //точка спауна

    public float timebetweenWaves = 5.4f;       //время между волнами
    private float countDown = 2.0f;             //переменная для отсчета времени

    private int waveIndex = 0;                  //индекс текущей волны

    [SerializeField] 
    private Text waveCountDownText;             //текст отсчета времени 

    public GameManager gameManager;

    private void Start()
    {
        EnemiesAlive = 0;
        spawnPoint = transform;
    }

    private void Update()
    {
        if (EnemiesAlive > 0)
            return;

        if(countDown <= 0.0f)
        {
            StartCoroutine(SpawnWave());
            countDown = timebetweenWaves;
            return;
        }

        countDown -= Time.deltaTime;
        countDown = Mathf.Clamp(countDown, 0.0f, Mathf.Infinity);
        waveCountDownText.text = string.Format("{0:00.00}", countDown);
    }

    private IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];

        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1 / wave.rate);
        }
        waveIndex++;

        if(waveIndex == waves.Length)
        {
            while(EnemiesAlive != 0)
            {
                yield return null;
            }
            gameManager.WinLevel();
            this.enabled = false;
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
