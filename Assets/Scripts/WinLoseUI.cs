using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WinLoseUI : MonoBehaviour
{
    public Leaderboard leaderboard;

    public TMP_Text textTitle;

    public GameObject panelLeaderboard;

    public AudioSource audioSource;
    public AudioClip clipWin;
    public AudioClip clipLose;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        leaderboard.LoadWinLose();

        panelLeaderboard.SetActive(false);

        if (leaderboard.youWon == 1)
        {
            YouWon();
            audioSource.clip = clipWin;
            audioSource.Play();
        }
        else
        {
            YouLose();
            audioSource.clip = clipLose;
            audioSource.Play();
        }
    }

    private void YouWon()
    {
        textTitle.text = "¡HAS GANADO!";

        panelLeaderboard.SetActive(true);
    }

    public void YouLose()
    {
        textTitle.text = "HAS PERDIDO...";

        panelLeaderboard.SetActive(false);
    }

    public void ButtonPressed(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
