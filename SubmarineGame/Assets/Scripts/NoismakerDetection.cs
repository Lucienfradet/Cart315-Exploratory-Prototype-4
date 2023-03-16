using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoismakerDetection : MonoBehaviour
{
    public GameObject noisemaker;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "torpedo") {
            TopredoMovements.disableCollider();
        }
    }
}
