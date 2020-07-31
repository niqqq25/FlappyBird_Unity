using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : FloatEventInvoker {

    const string BestScoreKey = "bestScore";
    int score = 0;
    int bestScore = 0;
    public static Score Instance { get; set; }

    // Use this for initialization
    void Start () {
        if(Instance != null)
        {
            Destroy(gameObject);
        } else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            unityEvents.Add(EventName.ScoreChanged, new ScoreChanged());
            LoadBestScore();
        }

        EventManager.AddListener(EventName.ScoreIncreased, Instance.IncreaseScore);
        EventManager.AddInvoker(EventName.ScoreChanged, Instance);
    }

    void IncreaseScore(float unused)
    {
        score++;
        AudioManager.Play(AudioClipName.ScoreIncrease);
        unityEvents[EventName.ScoreChanged].Invoke(score);
    }

    void LoadBestScore() {
        bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
    }

    void SaveBestScore() {
        PlayerPrefs.SetInt(BestScoreKey, bestScore);
    }

    public bool IsBestScoreGreaterThan(int score)
    {
        return bestScore > score;
    }

    public int _Score
    {
        get { return score; }
        set { score = value; }
    }

    public int BestScore
    {
        get {
            if(bestScore < score)
            {
                bestScore = score;
                SaveBestScore();
            }
            return bestScore;
        }
    }
}
