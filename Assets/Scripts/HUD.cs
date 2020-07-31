using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    [SerializeField]
    GameObject scoreField;

    [SerializeField]
    Image instructionImage;

	// Use this for initialization
	void Start () {
        EventManager.AddListener(EventName.ScoreChanged, UpdateScoreField);
        EventManager.AddListener(EventName.GameStarted, RemoveInstructionImage);
        EventManager.AddListener(EventName.GameStarted, UpdateScoreField);
        EventManager.AddListener(EventName.GameEnded, RemoveScoreField);
    }

    void RemoveInstructionImage(float unused)
    {
        Destroy(instructionImage.gameObject);
    }

    void RemoveScoreField(float unused)
    {
        Destroy(scoreField.gameObject);
    }

    void UpdateScoreField(float score = 0)
    {
        //clear score
        foreach (Transform child in scoreField.transform)
        {
            Destroy(child.gameObject);
        }

        //add new score
        List<Image> numbers = NumberConverter.Convert((int)score);

        foreach (Image number in numbers)
        {
            number.transform.SetParent(scoreField.transform, false);
        }
    }
}
