using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Inheritance: MainManager inherits MonoBehaviour to integrate with Unity's engine.
public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;
    public float BallSpeed = 2.0f;

    public Text ScoreText;
    public GameObject GameOverText;
    public Text BestScoreText;

    // Encapsulation: private fields with controlled access via properties
    private bool m_Started = false;
    private int m_Points;
    private bool m_GameOver = false;
    private int m_Level = 1;

    private List<Brick> m_ActiveBricks = new List<Brick>();
    private List<Brick> m_PoolBricks = new List<Brick>();

    private static int m_BestScore = 0;
    private static int m_BestLevel = 1;

    public int Points => m_Points;
    public int Level => m_Level;
    public bool GameIsOver => m_GameOver;

    void Start()
    {
        LoadBestScore();

        if (Ball != null)
        {
            Ball.GetComponent<Ball>().BallSpeed = BallSpeed;
        }

        SpawnBricks();
        UpdateScoreAndLevel();
        UpdateBestScoreText();
    }

    void Update()
    {
        HandlePlayerQuit();
        if (!m_Started && Input.GetKeyDown(KeyCode.Space))
        {
            LaunchBall();
        }
        else if (m_GameOver && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(RestartGame());
        }
    }

    private void HandlePlayerQuit()
    {
        if (Input.GetKey(KeyCode.Escape)) // Escape key to quit
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in editor
#endif
        }
    }

    // Abstraction: LaunchBall abstracts the ball launching logic
    void LaunchBall()
    {
        m_Started = true;

        Vector3 forceDir = new Vector3(0, 1, 0); // Straight up, no randomness
        forceDir.Normalize();

        Ball.transform.SetParent(null);
        Ball.AddForce(forceDir * BallSpeed, ForceMode.VelocityChange);
    }

    void SpawnBricks()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };

        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];

                // POLYMORPHISM: could use interface in future
                brick.onDestroyed.AddListener(HandleBrickDestroyed);

                m_ActiveBricks.Add(brick);
            }
        }
    }

    void HandleBrickDestroyed(Brick brick)
    {
        m_Points += brick.PointValue;
        UpdateScoreAndLevel();

        m_ActiveBricks.Remove(brick);
        m_PoolBricks.Add(brick);

        if (m_ActiveBricks.Count == 0)
        {
            m_Level++;
            StartCoroutine(RespawnBricks());
        }
    }

    void UpdateScoreAndLevel()
    {
        ScoreText.text = $"Score: {m_Points}   Level: {m_Level}";
    }

    void UpdateBestScoreText()
    {
        BestScoreText.text = $"Best Score: {m_BestScore}   Best Level: {m_BestLevel}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.GetComponent<Text>().text = $"Game Over\nFinal Score: {m_Points}\nFinal Level: {m_Level}";
        GameOverText.SetActive(true);

        if (m_Points > m_BestScore || (m_Points == m_BestScore && m_Level > m_BestLevel))
        {
            m_BestScore = m_Points;
            m_BestLevel = m_Level;
            UpdateBestScoreText();
            SaveBestScore();
        }
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator RespawnBricks()
    {
        yield return new WaitForSeconds(1.0f);

        foreach (var brick in m_PoolBricks)
        {
            brick.gameObject.SetActive(true);
            m_ActiveBricks.Add(brick);
        }

        m_PoolBricks.Clear();
        UpdateScoreAndLevel();
    }

    private void SaveBestScore()
    {
        PlayerPrefs.SetInt("BestScore", m_BestScore);
        PlayerPrefs.SetInt("BestLevel", m_BestLevel);
        PlayerPrefs.Save();
    }

    private void LoadBestScore()
    {
        m_BestScore = PlayerPrefs.GetInt("BestScore", 0);
        m_BestLevel = PlayerPrefs.GetInt("BestLevel", 1);
    }
}

public interface IScorable
{
    int GetPoints();
}