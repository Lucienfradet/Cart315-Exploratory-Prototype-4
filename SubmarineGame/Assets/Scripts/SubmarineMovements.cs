using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineMovements : MonoBehaviour
{
    public GameObject sub;
    private Rigidbody rb;
    private static SubAnimation subAnimation;

    [Header("Speed")]
    public static float speed = 13F;
    public float maxSpeed = 40F;
    public float minSpeed = 1F;
    public float speedChangeSmoothing = 20F;

    private static float targetSpeed;

    [Header("Heading")]
    public float heading = 0F;
    public float headingChangeSmoothing = 6F;
    public Camera topCamera;
    private static float targetHeading;

    

    void Start()
    {
        //set initial target speed
        targetSpeed = speed;
        targetHeading = heading;

        rb = sub.GetComponent<Rigidbody>();
        subAnimation = sub.GetComponent<SubAnimation>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveSub(sub);
    }

    private void moveSub(GameObject sub) {
        speed = Mathf.MoveTowards(speed, targetSpeed, speedChangeSmoothing * Time.deltaTime);
        sub.transform.Translate(Vector3.forward * Time.deltaTime * speed);


        heading = Mathf.MoveTowards(heading, targetHeading, headingChangeSmoothing * Time.deltaTime * speed);
        Debug.Log(heading);
        transform.localEulerAngles = new Vector3(0, heading, 0);
        //topCamera.transform.eulerAngles = new Vector3(0, 0, 0);



        /*float currentTurn = Mathf.MoveTowards(lastTurbineTurn, turbineSpeed / speedBuffer * -1, turbineSmoothing * Time.deltaTime);
        turbine.Rotate(new Vector3(0, 0, currentTurn));
        lastTurbineTurn = currentTurn;*/

        subAnimation.Spin(speed);
    }

    public static void speedChangeRequest(float value) {
        Debug.Log("targetSpeed = " + value);
        targetSpeed = value;
    }

    public static void headingChangeRequest(float value) {
        targetHeading = value;
    }

    public static float getSpeed() {
        return speed;
    }
}
