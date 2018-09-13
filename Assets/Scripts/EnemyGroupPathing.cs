using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupPathing : MonoBehaviour {

    [SerializeField] float speedOfEnemyDescent = 2f;

    // Update is called once per frame
	void Update ()
    {
        Move();
    }

    private void Move()
    {
        var newYPos = transform.position.y - speedOfEnemyDescent * Time.deltaTime;
        transform.position = new Vector2(transform.position.x, newYPos);
    }

    public void SetSpeedOfWave(float speed)
    {
        speedOfEnemyDescent = speed;
    }
}
