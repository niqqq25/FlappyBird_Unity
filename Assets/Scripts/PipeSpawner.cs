using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour {

    const int MaxPipes = 4;
    const float MovementSpeed = 1;
    const float UpperPipeOffset = 1f;
    const float LowerPipeOffset = 1.8f;

    [SerializeField]
    GameObject pipePrefab;
    Vector3 spawningPosition = Vector3.zero;
    public float firstPipeShowUpDelay;
    public float spawnDelay;
    Timer spawnTimer;
    Queue<GameObject> pipesQueue = new Queue<GameObject>();

	// Use this for initialization
	void Start () {
        //init spawn timer
        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = spawnDelay;
        spawnTimer.AddListener(Spawn);

        //initialize pipes
        for(int i = 0; i < MaxPipes; i++)
        {
            GameObject pipe = Instantiate(pipePrefab);
            pipe.SetActive(false);
            pipesQueue.Enqueue(pipe);
        }

        //calc spawning position
        spawningPosition.x = ScreenUtils.ScreenRight + MovementSpeed * (firstPipeShowUpDelay - spawnDelay);
    }
	
	// Update is called once per frame
	void Update () {
        if(GameManager.CurrentState == GameStateName.Gameplay)
        {
            transform.position -= new Vector3(MovementSpeed * Time.deltaTime, 0, 0);
            if (!spawnTimer.IsRunning) spawnTimer.Run();
        }
	}

    void Spawn()
    {
        GameObject pipe = pipesQueue.Dequeue();
        pipesQueue.Enqueue(pipe);
        pipe.transform.parent = gameObject.transform;
        pipe.SetActive(true);

        float maxSpawningPositionY = ScreenUtils.ScreenTop - UpperPipeOffset;
        float minSpawningPositionY = ScreenUtils.ScreenBottom + LowerPipeOffset;
        spawningPosition.y = Random.Range(minSpawningPositionY, maxSpawningPositionY);

        pipe.transform.position = spawningPosition;
    }
}
