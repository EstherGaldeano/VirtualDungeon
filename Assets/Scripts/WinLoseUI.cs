using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WinLoseUI : MonoBehaviour
{
    public Leaderboard leaderboard;

    public TMP_Text textTitle;

    public GameObject panelLeaderboard;

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
        }
        else
        {
            YouLose();
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
