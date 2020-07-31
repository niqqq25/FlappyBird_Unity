using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBasedBackground : MonoBehaviour {

    [SerializeField]
    Sprite dayBackground;

    [SerializeField]
    Sprite nightBackground;

	// Use this for initialization
	void Start () {
        int hour = System.DateTime.Now.Hour;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if(hour > 6 && hour < 21)
        {
            spriteRenderer.sprite = dayBackground;
        } else
        {
            spriteRenderer.sprite = nightBackground;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
