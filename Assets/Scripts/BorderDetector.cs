﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderDetector : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Cannon Ball")
        {
            Destroy(collision.gameObject);
        }
    }
}