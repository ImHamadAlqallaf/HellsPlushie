using UnityEngine;
using System.Collections;

public class TrapRoomSpawner : MonoBehaviour
{
    public GameObject teddyPrefab;
    public Transform[] spawnPoints;
    public GameObject frontDoor;
    public GameObject backWall;

    private int aliveEnemies = 0;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            frontDoor.SetActive(true);
            backWall.SetActive(true);
            StartCoroutine(SpawnWave());
            Destroy(gameObject);
        }
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(teddyPrefab, spawnPoints[i].position, Quaternion.identity);
            aliveEnemies++;
            yield return new WaitForSeconds(0.4f);
        }
    }

    public void OnEnemyKilled()
    {
        aliveEnemies--;
        if (aliveEnemies <= 0)
        {
            frontDoor.SetActive(false);
            backWall.SetActive(false);
        }
    }
}
