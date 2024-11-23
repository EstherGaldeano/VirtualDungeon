using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    private const int MaxScores = 10; // Máximo de puntuaciones a guardar
    private List<float> bestTimes = new List<float>();

    public int youWon;

    private void Start()
    {
        LoadTimes();
    }

    public void AddTime(float newTime)
    {
        // Agregar el tiempo a la lista
        bestTimes.Add(newTime);

        // Ordenar los tiempos (de menor a mayor)
        bestTimes.Sort();

        // Limitar la lista a los 10 mejores
        if (bestTimes.Count > MaxScores)
        {
            bestTimes.RemoveAt(bestTimes.Count - 1);
        }

        SaveTimes();
    }

    private void SaveTimes()
    {
        for (int i = 0; i < bestTimes.Count; i++)
        {
            PlayerPrefs.SetFloat("Time" + i, bestTimes[i]);
        }

        PlayerPrefs.SetInt("TimesCount", bestTimes.Count);
        PlayerPrefs.Save();
    }

    private void LoadTimes()
    {
        bestTimes.Clear();
        int count = PlayerPrefs.GetInt("TimesCount", 0);

        for (int i = 0; i < count; i++)
        {
            float time = PlayerPrefs.GetFloat("Time" + i, float.MaxValue);
            bestTimes.Add(time);
        }
    }

    public List<float> GetBestTimes()
    {
        return new List<float>(bestTimes);
    }

    public void SetWinLose(int number)
    {       
        PlayerPrefs.SetInt("youWon", number);
        PlayerPrefs.Save();
    }

    public void LoadWinLose()
    {
        youWon = PlayerPrefs.GetInt("youWon");
    }
}
