using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAntenna : MonoBehaviour
{
    public float range = 4f;        // Length of the signal
    public bool first = false;     // If the current antenna is the starting one
    public bool last = false;

    LineRenderer signalLine;        // Graphical line representing the signal
    Vector3 endPos;                 // Ending point of the line
    Quaternion rotation;

    Ray shootRay = new Ray();
    RaycastHit shootHit;

    DetectAntenna lastAntennaHit = null;

    void Awake()
    {
        signalLine = GetComponent<LineRenderer>();
        if (first)
            signalLine.enabled = true;
        else
            signalLine.enabled = false;

        signalLine.SetPosition(0, transform.position);
        signalLine.SetPosition(1, transform.position + transform.forward * range);
    }


    // Update is called once per frame
    void Update() {
        // Update line position
        signalLine.SetPosition(0, transform.position);
        signalLine.SetPosition(1, transform.position + transform.forward * range);

        if(signalLine.enabled)
            Shoot();
    }


    // Check if I hit other Antennas
    void Shoot() {

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range)) {

            Transform hitTrans = shootHit.collider.GetComponent<Transform>();

            // Check if what I hit is an Antenna
            if(hitTrans.gameObject.tag == "Antenna") {

                // Do something, like activate other antenna
                DetectAntenna detected = shootHit.collider.GetComponent<DetectAntenna>();
                detected.Activate();
                lastAntennaHit = detected;
            }
        }
        else {
            // Reset the antenna I was hitting
            Deactivate();
        }
    }

    // Antenna activates when hit
    public void Activate() {
        signalLine.enabled = true;
        // Do Something
    }

    // Disable connection unless I am the first
    public void Deactivate() {

        if (lastAntennaHit != null)
        {
            lastAntennaHit.Deactivate();
        }


        lastAntennaHit = null;

        // Disable my possibility to emit
        if (!first)
            signalLine.enabled = false;

    }
}
