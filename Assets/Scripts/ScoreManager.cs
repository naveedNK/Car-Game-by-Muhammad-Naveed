using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI restartText;
    private float score = 0f;
    public PlayerController player;

    private bool isGameOver = false;

    void Update()
    {
        // Always allow restart check, even if game is stopped
        if (isGameOver && Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }

        // Stop updating score if game over
        if (isGameOver) return;

        // Update score
        if (!player.gameOver)
        {
            score += Time.deltaTime * 10;
            scoreText.text = "Score: " + Mathf.FloorToInt(score);
        }
        else
        {
            GameOver();
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + Mathf.FloorToInt(score);
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
