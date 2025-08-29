using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance; // �V���O���g��

    [System.Serializable]
    public class Wave
    {
        public GameObject enemyPrefab;
        public int enemyCount;
        public float spawnInterval = 1f;
    }

    public List<Wave> waves = new List<Wave>();
    private int currentWaveIndex = -1;
    private int aliveEnemies = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartNextWave()
    {
        if (currentWaveIndex + 1 >= waves.Count)
        {
            Debug.Log("�S�ẴE�F�[�u���I�����܂����I");
            return;
        }

        currentWaveIndex++;
        StartCoroutine(SpawnWave(waves[currentWaveIndex]));
    }

    private System.Collections.IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log("Wave " + (currentWaveIndex + 1) + " �J�n�I");
        aliveEnemies = wave.enemyCount;

        for (int i = 0; i < wave.enemyCount; i++)
        {
            Instantiate(wave.enemyPrefab, new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f)), Quaternion.identity);
            yield return new WaitForSeconds(wave.spawnInterval);
        }
    }

    public void OnEnemyKilled()
    {
        aliveEnemies--;
        if (aliveEnemies <= 0)
        {
            Debug.Log("Wave " + (currentWaveIndex + 1) + " �I���I");
            KanjiPlacementManager.Instance.StartPlacement();
        }
    }
}