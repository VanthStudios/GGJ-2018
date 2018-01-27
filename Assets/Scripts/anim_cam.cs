using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_cam : MonoBehaviour {

    public Animator ControlloCam = new Animator();

	// Use this for initialization
	void Start () {
        ControlloCam = GameObject.Find("Main Camera").GetComponent<Animator>();
        ControlloCam.SetBool("spostato", true);
        //ControlloCam.SetBool("adj_lvl2", true);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
