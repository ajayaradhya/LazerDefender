using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerSpinner : MonoBehaviour {

    [SerializeField] float speedOfSpin = 360f;

	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 0, speedOfSpin * Time.deltaTime));
	}
}
