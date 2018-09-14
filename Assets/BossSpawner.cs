using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour {

    [SerializeField] GameObject bossEnemyPrefab;
    [SerializeField] float positionYToSpawn = 10f;
    [SerializeField] GameObject bossPathPrefab;
    [SerializeField] float movementSpeed = 10f;

    private GameObject bossSpawned;
    private List<Transform> bossPathTransforms = new List<Transform>();
    private int waypointIndex = 0;

    // Use this for initialization
    void Start () {

        var positionToSpawn = new Vector3(0, positionYToSpawn, 0);
        bossSpawned = Instantiate(bossEnemyPrefab, positionToSpawn, transform.rotation);
        bossSpawned.transform.SetParent(gameObject.transform, false);

        foreach (Transform waypoint in bossPathPrefab.transform)
        {
            bossPathTransforms.Add(waypoint);
        }
    }
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    private void Move()
    {
        if (bossPathTransforms == null) { return; }
        if (waypointIndex <= bossPathTransforms.Count - 1)
        {
            var targetPos = bossPathTransforms[waypointIndex].transform.position;
            targetPos = new Vector3(targetPos.x, targetPos.y - positionYToSpawn, 0);
            var moveThisFrame = Random.Range(0, movementSpeed) * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveThisFrame);

            if (transform.position == targetPos)
            {
                waypointIndex++;
            }
        }
        else
        {
            waypointIndex = 0;
        }
    }
}
