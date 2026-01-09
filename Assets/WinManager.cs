using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    public GameObject winPanel;
    public bool isWin = false;

    void Start()
    {
        if (winPanel != null) winPanel.SetActive(false);
    }

    void Update()
    {
        if (!isWin) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void ShowWin()
    {
        isWin = true;
        Time.timeScale = 0f;

        if (winPanel != null) winPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
    }
}
