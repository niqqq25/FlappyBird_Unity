using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBase : MonoBehaviour {

    const float movementSpeed = 1f;

    float baseTextureLength;

	// Use this for initialization
	void Start () {
        baseTextureLength = GetComponent<SpriteRenderer>().bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () {
        if(GameManager.CurrentState != GameStateName.GameOver)
        {
            transform.position -= new Vector3(movementSpeed * Time.deltaTime, 0, 0);
        }
	}

    private void OnBecameInvisible()
    {
        transform.position += new Vector3(2 * baseTextureLength, 0, 0);
    }
}
