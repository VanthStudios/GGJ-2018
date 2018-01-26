using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneraCilindri : MonoBehaviour {

    Ray ray;
    RaycastHit hit;
    public GameObject prefab;
    public GameObject mappa;
    
    // Use this for initialization
    void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {

            if (Input.GetMouseButtonDown(0))
            {
                GameObject childButton = Instantiate(prefab, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity) as GameObject;
                childButton.SetActive(true);
                childButton.transform.parent = mappa.transform;
            }

        }
        if(Input.GetKey(KeyCode.Space))
        {
            
            GetComponent<Animator>().SetBool("movimentoCamera", true);
        }
    }
}
