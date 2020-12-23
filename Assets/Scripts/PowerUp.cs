using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    [SerializeField] GameObject powerUpPrefab;
    [SerializeField] float minDelay;
    [SerializeField] float maxDelay;
    [SerializeField] float minX = -1f;
    [SerializeField] float maxX = 1f;
    [SerializeField] float delay;
    [SerializeField] float padding = 3f;
    [SerializeField] float speed = 2f;

    float xPos;
    float yPos;
    Vector3 spawnPosition;
    Camera gameCamera;
    

	// Use this for initialization
	void Start () {

        delay = Random.Range(minDelay,maxDelay);
        gameCamera = Camera.main;
        yPos =  gameCamera.ViewportToWorldPoint(Vector2.up).y + powerUpPrefab.GetComponent<Renderer>().bounds.size.y / 2;
        xPos = Random.Range(gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding, gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding);
        StartCoroutine(Spawn());
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    IEnumerator Spawn()
    {
        while (true)
        {
            spawnPosition = new Vector3(xPos, yPos);
            yield return new WaitForSeconds(delay);
            GameObject newPowerUp = Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity) as GameObject;
            newPowerUp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
            xPos = Random.Range(gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding, gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding);
            delay = Random.Range(minDelay, maxDelay);
        }
        
    }

    
}
