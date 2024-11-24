using UnityEngine;
using TMPro;
using UnityEditor.Timeline;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public float totalTime;
    public float time;
    public bool countingTime;
    public float currentTime;

    public TMP_Text timeText;

    public Leaderboard leaderboard;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        totalTime = 180f;
        time = totalTime;
        countingTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (countingTime)
        {
            time -= Time.deltaTime;

            currentTime += Time.deltaTime;

            if (time <= 0)
            {
                countingTime = false;

                TimeOut();                
            }
        }
        timeText.text = TimeTextFormat(time);

        if(Input.GetKeyDown(KeyCode.T)) 
        {
            YouWin();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            time = 2.0f;
        }
    }

    public void TimeOut()
    {
        leaderboard.SetWinLose(0);

        Invoke("GoToWinLose", 2.0f);
    }

    public void YouWin()
    {
        countingTime = false;

        leaderboard.AddTime(currentTime);

        leaderboard.SetWinLose(1);

        Invoke("GoToWinLose", 2.0f);
    }
    

    string TimeTextFormat(float t)
    {
        int min;
        int sec;

        if (t >= 0)
        {
            min = Mathf.FloorToInt(t / 60);
            sec = Mathf.FloorToInt(t % 60);
        }
        else
        {
            min = 0;
            sec = 0;
        }

        return string.Format("{0:00}:{1:00}", min, sec);
    }

    private void GoToWinLose()
    {
        SceneManager.LoadScene("WinLose");
    }
}
