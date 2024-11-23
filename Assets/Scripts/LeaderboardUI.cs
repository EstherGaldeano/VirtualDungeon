using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardUI : MonoBehaviour
{
    public Leaderboard leaderboard;
    public TMP_Text leaderboardText;

    private void Update()
    {
        DisplayLeaderboard();
    }

    private void DisplayLeaderboard()
    {
        List<float> bestTimes = leaderboard.GetBestTimes();
        leaderboardText.text = "";

        for (int i = 0; i < bestTimes.Count; i++)
        {
            leaderboardText.text += $"{i + 1}. {bestTimes[i]:F2} segundos\n";
        }
    }
}
