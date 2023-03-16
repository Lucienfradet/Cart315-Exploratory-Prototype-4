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

    private static bool noisemakerSent = false;
    private static float noisemakerCounter = 0F;
    private static float noisemakerSentDelay = 30F;

    private static bool decoySent = false;
    private static float decoyCounter = 0F;
    private static float decoySentDelay = 30F;

    [Header("Noisemaker")]
    public GameObject noisemakerPrefab;
    private static GameObject noisemakerPrefabStatic;
    public GameObject noisemakerSpawn;
    private static GameObject noisemakerSpawnStatic;

    [Header("Decoy")]
    public GameObject decoyPrefab;
    public float decoySpeed = 46F;
    private static float decoySpeedStatic;
    private static GameObject decoyPrefabStatic;
    public GameObject decoySpawn;
    private static GameObject decoySpawnStatic;

    [Header("Contact")]
    public GameObject torpedo;
    public static float bearing;
    public static float range;



    void Start()
    {
        //set initial target speed
        targetSpeed = speed;
        targetHeading = heading;

        //set static variables
        noisemakerPrefabStatic = noisemakerPrefab;
        noisemakerSpawnStatic = noisemakerSpawn;
        decoyPrefabStatic = decoyPrefab;
        decoySpawnStatic = decoySpawn;
        decoySpeedStatic = decoySpeed;

        rb = sub.GetComponent<Rigidbody>();
        subAnimation = sub.GetComponent<SubAnimation>();
    }

    private void Update() {
        //prevent Spam
        if (noisemakerSent) {
            noisemakerCounter += Time.deltaTime;
            if (noisemakerCounter >= noisemakerSentDelay) {
                noisemakerCounter = 0F;
                noisemakerSent = false;
            }
        }
        if (decoySent) {
            decoyCounter += Time.deltaTime;
            if (decoyCounter >= decoySentDelay) {
                decoyCounter = 0F;
                decoySent = false;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveSub(sub);
        updateContact();
    }

    private void moveSub(GameObject sub) {
        speed = Mathf.MoveTowards(speed, targetSpeed, speedChangeSmoothing * Time.deltaTime);
        sub.transform.Translate(Vector3.forward * Time.deltaTime * speed);


        heading = Mathf.MoveTowards(heading, targetHeading, headingChangeSmoothing * Time.deltaTime * speed);
        transform.localEulerAngles = new Vector3(0, heading, 0);
        //topCamera.transform.eulerAngles = new Vector3(0, 0, 0);



        /*float currentTurn = Mathf.MoveTowards(lastTurbineTurn, turbineSpeed / speedBuffer * -1, turbineSmoothing * Time.deltaTime);
        turbine.Rotate(new Vector3(0, 0, currentTurn));
        lastTurbineTurn = currentTurn;*/

        subAnimation.Spin(speed);
    }

    private void updateContact() {
        Vector3 dir = transform.position - torpedo.transform.position;
        range = dir.magnitude;
        dir = torpedo.transform.InverseTransformDirection(dir);
        bearing = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }

    public static void speedChangeRequest(float value) {
        targetSpeed = value;
    }

    public static void headingChangeRequest(float value) {
        targetHeading = value;
    }

    public static void sendNoisemaker() {
        if (!noisemakerSent) {
            noisemakerSent = true;
            GameObject noiseMaker = Instantiate(noisemakerPrefabStatic) as GameObject;
            Vector3 vectorPos = noisemakerSpawnStatic.transform.position;
            noiseMaker.transform.position = vectorPos;
        }
    }

    public static void sendDecoy() {
        if (!decoySent) {
            decoySent = true;
            GameObject decoy = Instantiate(decoyPrefabStatic) as GameObject;
            Vector3 vectorPos = decoySpawnStatic.transform.position;
            decoy.transform.position = vectorPos;

            Rigidbody rb = decoy.GetComponent<Rigidbody>();
            rb.AddForce(decoy.transform.forward * decoySpeedStatic);
        }
    }

    public static float getSpeed() {
        return speed;
    }
}
