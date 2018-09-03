using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour {

    [SerializeField] float lazerSpeed = 2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        var newPosition = transform.position.y + lazerSpeed;
        transform.position = new Vector2(transform.position.x, newPosition);
	}
}
