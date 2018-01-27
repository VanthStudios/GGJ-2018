using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectAntenna : MonoBehaviour
{
    public float range = 4f;        // Length of the signal
    public bool fire = false;

    LineRenderer signalLine;        // Graphical line representing the signal
    Vector3 endPos;                 // Ending point of the line
    Quaternion rotation;

    Ray shootRay = new Ray();
    RaycastHit shootHit;

    Antenna lastAntennaHit = null;

    public void StartFiring() {
    	fire = true;
    	signalLine.enabled = true;
    }

    public void StopFiring() {

		if (lastAntennaHit != null){

        	lastAntennaHit.Deactivate();
        	lastAntennaHit = null;
        }
    	fire = false;
    	signalLine.enabled = false;
    }

    void Awake()
    {
        signalLine = GetComponent<LineRenderer>();

        if (!fire) {
        	signalLine.enabled = false;
        }
    }


    // Update is called once per frame
    void Update() {
        // Update line position
        signalLine.SetPosition(0, transform.position);
        signalLine.SetPosition(1, transform.position + transform.forward * range);

        if (fire) {
            Shoot();
        }
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
                lastAntennaHit = shootHit.collider.GetComponent<Antenna>();
                lastAntennaHit.Activate();
            }
        }
       	else if (lastAntennaHit != null){

        	lastAntennaHit.Deactivate();
        	lastAntennaHit = null;
        }

        // Deactivate
    }
}
