using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoyMovements : MonoBehaviour {
    public GameObject decoy;
    private Rigidbody rb;
    private static TorpedoAnimation torpedoAnimation;

    [Header("Speed")]
    public static float speed = 50F;
    public float maxSpeed = 50F;
    public float minSpeed = 1F;
    public float speedChangeSmoothing = 20F;

    private float targetSpeed;

    private void Start() {
        rb = decoy.GetComponent<Rigidbody>();
        torpedoAnimation = decoy.GetComponent<TorpedoAnimation>();

        targetSpeed = speed;
    }

    private void FixedUpdate() {
        moveTorp();
    }

    private void moveTorp() {
        speed = Mathf.MoveTowards(speed, targetSpeed, speedChangeSmoothing * Time.deltaTime);
        decoy.transform.Translate(Vector3.forward * Time.deltaTime * speed);

        torpedoAnimation.Spin(speed);
    }
}
