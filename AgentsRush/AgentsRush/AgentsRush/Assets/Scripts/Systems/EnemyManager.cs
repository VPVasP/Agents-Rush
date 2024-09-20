using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [Header("Enemy Settings")]
    public GameObject[] enemyPrefabs;
    public int enemiesToSpawn;
    public Transform[] spawnPoints;
    public Vector3 gizmosCubeSize = new Vector3(5f, 5f, 5f);
    public Vector3 gizmosPosition = new Vector3(-82, 50, -70);

    [Header("Enemy Counters")]
    public int enemiesKilled;
    public int enemiesRemaining;

    public GameObject fruitPrefab;
    public GameObject fruitEffect;
    public Transform fruitSpawnPosition;
    private void Awake()
    {
        //singleton pattern to ensure only one instance exists
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        SpawnEnemies(enemiesToSpawn);
        UpdateEnemiesRemainingText();
    }

    ///spawns a specified number of enemies at random positions
    public void SpawnEnemies(int number)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Vector3 spawnPosition = GetRandomPointInCube(Vector3.zero);
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
        enemiesRemaining += number;
        UpdateEnemiesRemainingText();
    }
    public void SpawnFruit(int number)
    {
            GameObject fruitPrefabClone = fruitPrefab;

            GameObject spawnEffect = Instantiate(fruitEffect, fruitPrefabClone.transform.position, Quaternion.identity);
            Destroy(spawnEffect, 1.5f);
            Instantiate(fruitPrefabClone, fruitSpawnPosition.position, Quaternion.identity);
     }
    

    //calculates a random point within a defined cube area
    private Vector3 GetRandomPointInCube(Vector3 offset)
    {
        Vector3 cubeCenter = gizmosPosition;
        Vector3 cubeExtents = gizmosCubeSize / 2f;

        float randomX = Random.Range(cubeCenter.x - cubeExtents.x, cubeCenter.x + cubeExtents.x);
        float randomY = Random.Range(cubeCenter.y - cubeExtents.y, cubeCenter.y + cubeExtents.y);
        float randomZ = Random.Range(cubeCenter.z - cubeExtents.z, cubeCenter.z + cubeExtents.z);

        return new Vector3(randomX, randomY, randomZ) + offset;
    }

    //called when an enemy is killed
    public void EnemyKilled()
    {
        enemiesKilled++;
        enemiesRemaining--;
        UpdateEnemiesRemainingText();

        //notify the GamePhaseManager about enemy kill
        GamePhaseManager.instance.OnEnemyKilled(enemiesKilled);
    }

    //updates the UI text for enemies remaining
    private void UpdateEnemiesRemainingText()
    {
        UiManager.instance.UpdateEnemiesRemaining(enemiesRemaining);
    }

    ///visualizes the spawn area in the editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(gizmosPosition, gizmosCubeSize);
    }
}