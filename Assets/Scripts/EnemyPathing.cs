using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

    WaveConfig waveConfig;

    private int waypointIndex = 0;
    private List<Transform> waypoints;

    // Use this for initialization
    void Start ()
    {
        waypoints = waveConfig.GetAllWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig config)
    {
        this.waveConfig = config;
    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPos = waypoints[waypointIndex].transform.position;
            var moveThisFrame = waveConfig.GetMovementSpeed() * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveThisFrame);

            if (transform.position == targetPos)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
