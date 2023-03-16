using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoHit : MonoBehaviour
{
    public GameObject missile;

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "submarine") {
            UpdateUI.gameLostFlag = true;
        }
    }
}
