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
            Debug.Log("Wave " + currentWave + " �J�n�I");

            for (int i = 0; i < enemiesPerWave; i++)
            {
                Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(enemyPrefab, sp.position, Quaternion.identity);
                yield return new WaitForSeconds(spawnInterval);
            }

            // �E�F�[�u�I�� �� �v���C���[�Ɋ����I������^����
            Debug.Log("Wave " + currentWave + " �I���I");

            yield return new WaitForSeconds(5f); // ���E�F�[�u�܂ŋx�e
        }

        Debug.Log("�N���A�I");
    }
}
