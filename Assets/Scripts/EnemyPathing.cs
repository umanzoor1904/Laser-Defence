using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

    WaveConfig waveConfig;
    [SerializeField] List<Transform> waypoint;

    int waypointIndex = 0;

	// Use this for initialization
	void Start () {
        waypoint = waveConfig.GetWayPoint();
        transform.position = waypoint[waypointIndex].transform.position;
		
	}

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (waypointIndex <= waypoint.Count - 1)
        {
            var targetPosition = waypoint[waypointIndex].transform.position;
            var movement = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movement);

            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            /*
            var firstPosition = waypoint[0].transform.position;
            var movement = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, firstPosition, movement);
            waypointIndex = 0;
            */
            Destroy(gameObject);
        }
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
        

        
		
	
}
