using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI restartText;
    public InputActionReference restartAction;
    public Button restartButton;
    private float score = 0f;
    public PlayerController player;

    private bool isGameOver = false;


    void OnEnable()
    {
        if (restartAction != null)
            restartAction.action.Enable();
    }

    void OnDisable()
    {
        if (restartAction != null)
            restartAction.action.Disable();
    }


    void Update()
    {
        // Always allow restart check, even if game is stopped
        //if (isGameOver && Input.GetKeyDown(KeyCode.Space))

        //{
        //    RestartGame();
        //}

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

        if (restartAction != null && restartAction.action.WasPressedThisFrame())
        {
            RestartGame();
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
        restartButton.gameObject.SetActive(true);
    }

    //public void RestartGame()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //}
    public void RestartGame()
    {
        SceneManager.LoadScene(
        SceneManager.GetActiveScene().buildIndex
        );
    }
}
