using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurAntenna : MonoBehaviour {
    public GameObject prefab, mappa;

    private bool isDeployAllow = true;
    private Ray ray;
    private RaycastHit hit;

    public void DisableCursor()
    {
        isDeployAllow = false;
    }

    public void EnableCursor()
    {
        isDeployAllow = true;
    }

    public void ShowCursor()
    {
        gameObject.SetActive(true);
    }

	// Update is called once per frame
	void Update ()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (isDeployAllow && Input.GetMouseButtonDown(0))
            {
                GameObject childButton = Instantiate(prefab, new Vector3(hit.point.x, hit.point.y + 1, hit.point.z), Quaternion.identity) as GameObject;
                childButton.SetActive(true);
                childButton.transform.parent = mappa.transform;
                gameObject.SetActive(false);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            gameObject.SetActive(false);
        }
    }
}
