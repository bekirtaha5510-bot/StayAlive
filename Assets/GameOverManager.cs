using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public bool isGameOver = false;

    void Start()
    {
        // Oyunu normal hýzda baþlat
        Time.timeScale = 1f;

        // Game Over panelini gizle
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        // Mouse'u kilitle
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!isGameOver) return;

        // R tuþu ile oyunu yeniden baþlat
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // ESC ile mouse kilidini aç
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void ShowGameOver()
    {
        isGameOver = true;

        // Oyunu durdur
        Time.timeScale = 0f;

        // Game Over panelini göster
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        // Mouse'u serbest býrak
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
