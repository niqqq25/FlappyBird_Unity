using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {

    [SerializeField]
    Image medalImage;
    [SerializeField]
    Sprite[] medalSprites;

    [SerializeField]
    Image newBestImage;

    [SerializeField]
    GameObject currentScore;
    [SerializeField]
    GameObject bestScore;

    [SerializeField]
    Image flashEffectImage;
    const float FlashSpeed = 2f;

    // Use this for initialization
    void Start() {
        Score score = Score.Instance;

        //set medal
        Sprite medal = GetMedal(score._Score);
        if (medal != null)
        {
            medalImage.sprite = medal;
        } else
        {
            Destroy(medalImage.gameObject);
        }

        //set current score
        foreach(Image number in NumberConverter.Convert(score._Score))
        {
            number.transform.SetParent(currentScore.transform, false);
        }

        newBestImage.gameObject.SetActive(!score.IsBestScoreGreaterThan(score._Score));

        //set best score
        foreach (Image number in NumberConverter.Convert(score.BestScore))
        {
            number.transform.SetParent(bestScore.transform, false);
        }

        ChangeAlpha(flashEffectImage, 1);
    }

    private void Update()
    {
        ChangeAlpha(flashEffectImage, Mathf.Max(0, flashEffectImage.color.a - Time.deltaTime * FlashSpeed));
    }

    public void HandleStartButtonClick()
    {
        Score.Instance._Score = 0;
        SceneManager.LoadScene("Gameplay");
    }

    void ChangeAlpha(Image image, float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }

    Sprite GetMedal(int score)
    {
        if(score >= 40)
        {
            return medalSprites[3];
        } else if (score >= 30)
        {
            return medalSprites[2];
        } else if (score >= 20)
        {
            return medalSprites[1];
        } else if ( score >= 10)
        {
            return medalSprites[0];
        }
        return null;
    }
}
