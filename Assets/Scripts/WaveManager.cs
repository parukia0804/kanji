using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public int enemiesPerWave = 5;
    public float spawnInterval = 1f;
    public int totalWaves = 3;

    private int currentWave = 0;

    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        while (currentWave < totalWaves)
        {
            currentWave++;
            Debug.Log("Wave " + currentWave + " 開始！");

            for (int i = 0; i < enemiesPerWave; i++)
            {
                Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(enemyPrefab, sp.position, Quaternion.identity);
                yield return new WaitForSeconds(spawnInterval);
            }

            // ウェーブ終了 → プレイヤーに漢字選択権を与える
            Debug.Log("Wave " + currentWave + " 終了！");

            yield return new WaitForSeconds(5f); // 次ウェーブまで休憩
        }

        Debug.Log("クリア！");
    }
}
