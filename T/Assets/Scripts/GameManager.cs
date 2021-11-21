using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool gameOver = false;

    [SerializeField]
    TextMeshProUGUI gameOverText;

    [SerializeField]
    LayerMask isLevel;

    //Можно было бы и сделать Лист из префабов противников, но пока что тип противников только 1
    public GameObject enemyPrefab;

    int waveNumber = 1;
    int enemyCounter = 0;
    bool spawnPointSet;
    Vector3 spawnPoint;

    float xBoundaries = 29f;
    float zBoundaries = 14f;
    float yPos = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnNewWave(waveNumber);
    }


    // Update is called once per frame
    void Update()
    {
        enemyCounter = FindObjectsOfType<BaseEnemy>().Length;
        if(enemyCounter == 0 && !gameOver)
        {
            SpawnNewWave(++waveNumber);
        }

        if (gameOver)
        {
            DestroyAllEnemies();
        }
    }

    private void DestroyAllEnemies()
    {
        BaseEnemy[] enemies = FindObjectsOfType<BaseEnemy>();
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].Dying();
        }
    }

    private void SpawnNewWave(int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            if (!spawnPointSet) GenerateSpawnPosition();
            if (spawnPointSet)
            {
                Instantiate(enemyPrefab, spawnPoint, enemyPrefab.transform.rotation);
                spawnPointSet = false;
            }
            
        }
    }

    private void GenerateSpawnPosition()
    {
        float x = Random.Range(-xBoundaries, xBoundaries);
        float z = Random.Range(-zBoundaries, zBoundaries);
        spawnPoint = new Vector3(x, yPos, z);

        if(!Physics.CheckSphere(spawnPoint, 1f, isLevel))
        {
            spawnPointSet = true;
            Debug.Log(spawnPointSet.ToString());
            Debug.Log(enemyCounter);
        }
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverText.gameObject.SetActive(true);
    }
}
