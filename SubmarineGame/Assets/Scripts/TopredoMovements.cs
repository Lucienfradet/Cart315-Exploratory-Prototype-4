using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopredoMovements : MonoBehaviour {
    public GameObject torp;
    private static GameObject torpStatic;
    private Rigidbody rb;
    private static TorpedoAnimation torpedoAnimation;

    [Header("Speed")]
    public static float speed = 50F;
    public float targetSpeed;
    public float maxSpeed = 50F;
    public float minSpeed = 1F;
    public float speedChangeSmoothing = 20F;

    [Header("Target")]
    public string currentTarget;
    public bool haveTarget = false;

    private static bool colliderActiveFlag = true;
    private float colliderTimer = 0F;
    private float colliderTimerMax = 8F;

    private void Start() {
        torpStatic = torp;
        rb = torp.GetComponent<Rigidbody>();
        torpedoAnimation = torp.GetComponent<TorpedoAnimation>();

        targetSpeed = speed;
    }

    private void Update() {
        if (!colliderActiveFlag) {
            haveTarget = false;
            colliderTimer += Time.deltaTime;
            if (colliderTimer >= colliderTimerMax) {
                colliderTimer = 0;
                colliderActiveFlag = true;
                torp.GetComponent<Collider>().enabled = true;
            }
        }
    }

    private void FixedUpdate() {
        moveTorp();
        if (colliderActiveFlag) {
            checkColliders();
        }
        if (!haveTarget && colliderActiveFlag) {
            findTarget();
        }
    }

    private void moveTorp() {
        speed = Mathf.MoveTowards(speed, targetSpeed, speedChangeSmoothing * Time.deltaTime);
        torp.transform.Translate(Vector3.forward * Time.deltaTime * speed);

        torpedoAnimation.Spin(speed);
    }

    private void findTarget() {
        transform.Rotate(0, 0.5F, 0);
    }

    private void OnTriggerEnter(Collider other) {
    /*    if (other.CompareTag("submarine")) {
            Debug.Log("sub target");
        }
        if (other.CompareTag("decoy")) {
            Debug.Log("decoy target");
        }*/

        if (other.tag == "noisemaker") {
            Debug.Log("noiseMaker CONTACT");
        }

        
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(transform.position + new Vector3(0, 0, (10000 * (float)0.0792)), 10000 * (float)0.0792);
    }

    private void checkColliders() {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + new Vector3(0, 0, (10000 * (float)0.0792)), 10000 * (float)0.0792);
        if (hitColliders.Length > 0) {
            Collider closestCollider = null;
            float closestColliderDistance = 0F;
            foreach (Collider hitCollider in hitColliders) {
                if (hitCollider.tag != "torpedoIgnore") { //prevent noismaker contact
                    float currentMag = (transform.position - hitCollider.transform.position).magnitude;
                    //fix for submarine
                    if (hitCollider.tag == "submarine") {
                        currentMag -= 57;
                    }
                    if (closestColliderDistance == 0) {
                        closestColliderDistance = currentMag;
                        closestCollider = hitCollider;
                    }
                    if (closestColliderDistance > currentMag) {
                        closestColliderDistance = currentMag;
                        closestCollider = hitCollider;
                    }
                }
            }

            if (closestCollider != null) {
                currentTarget = closestCollider.tag;
                haveTarget = true;
                torp.transform.LookAt(closestCollider.transform);
            }
            
        }
        else {
            //find target again...
            haveTarget = false;
        }

    }

    public static void disableCollider() {
        Debug.Log("collider disabled");
        torpStatic.GetComponent<Collider>().enabled = false;
        colliderActiveFlag = false;
    }
}
