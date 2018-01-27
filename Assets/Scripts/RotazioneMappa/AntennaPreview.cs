using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntennaPreview : MonoBehaviour {
    private Ray ray;
    private RaycastHit hit;

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) {
            //this.transform.parent = mappa.transform;
            //Vector3 temp = new Vector3(hit.point.x, hit.point.y, hit.point.z)+mappa.transform.position;
            gameObject.transform.position = new Vector3(hit.point.x, hit.point.y + 1, hit.point.z);
        } else {
            gameObject.transform.position = ray.GetPoint(10);
        }
    }
}
