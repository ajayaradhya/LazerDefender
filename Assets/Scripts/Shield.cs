using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    void Start()
    {
        if (ShieldCountHandler.Instance.GetCurrentShieldCount() > 0)
        {
            ShieldCountHandler.Instance.UseUpOneShield();
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
