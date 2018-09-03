﻿using UnityEngine;

public class Shredder : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Lazer")
        {
            Destroy(col.gameObject);
        }
    }

}
