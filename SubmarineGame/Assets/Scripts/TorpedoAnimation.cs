using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoAnimation : MonoBehaviour
{
    public float turbineSmoothing = 25F;
    public float lastTurbineTurn = 13F;

    public float speedBuffer = 2F;
    public Transform turbine;

    private void Start() {

    }

    public void Spin(float turbineSpeed) {
        float currentTurn = Mathf.MoveTowards(lastTurbineTurn, turbineSpeed / speedBuffer * -1, turbineSmoothing * Time.deltaTime);
        turbine.Rotate(new Vector3(0, 0, currentTurn));
        lastTurbineTurn = currentTurn;
    }

}
