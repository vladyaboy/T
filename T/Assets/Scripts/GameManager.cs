using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool gameOver = false;

    int playerScore = 0;

    [SerializeField]
    TextMeshProUGUI gameOverText;

    [SerializeField]
    TextMeshProUGUI scoreText;

    [SerializeField]
    TextMeshProUGUI waveText;

    [SerializeField]
    Button restartButton;

    string scoreTextTemplate = "The score:";
    string waveTextTemplate = "Wave:";

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
        HandleWaveSpawning();

        //не уверен что это стоит делать в апдейте
        HandleGameOver();

        HandleUiText();
    }

    private void HandleWaveSpawning()
    {
        enemyCounter = FindObjectsOfType<BaseEnemy>().Length;
        if (enemyCounter == 0 && !gameOver)
        {
            SpawnNewWave(++waveNumber);
        }
    }

    private void HandleUiText()
    {
        scoreText.text = ($"{scoreTextTemplate} {playerScore}");
        waveText.text = ($"{waveTextTemplate} {waveNumber}");
    }

    private void HandleGameOver()
    {
        if (gameOver)
        {
            DestroyAllEnemies();
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
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
            while(!spawnPointSet)
            { 
                GenerateSpawnPosition(); 
            }

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
        }
    }

    public void GameOver()
    {
        gameOver = true;
    }

    public void AddScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
