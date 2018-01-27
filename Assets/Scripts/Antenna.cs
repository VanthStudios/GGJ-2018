using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Antenna : MonoBehaviour {
	public UnityEvent OnActivate; 
	public UnityEvent OnDeactivate;

	public void Activate() {
		OnActivate.Invoke();
	}

	public void Deactivate(){
		OnDeactivate.Invoke();
	}
}
