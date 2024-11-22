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
                Debug.Log("Te has quedado sin tiempo");
                countingTime = false;
            }
        }
        timeText.text = TimeTextFormat(time);

        if(Input.GetKeyDown(KeyCode.T)) 
        { 
            StopTimer();

            SceneManager.LoadScene("WinLose");
        }
    }

    public void StopTimer()
    {
        countingTime = false;

        leaderboard.AddTime(currentTime);
    }

    string TimeTextFormat(float t)
    {
        int min = Mathf.FloorToInt(t / 60);
        int sec = Mathf.FloorToInt(t % 60);
        return string.Format("{0:00}:{1:00}", min, sec);
    }
}
