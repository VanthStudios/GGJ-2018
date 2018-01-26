using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class rotazione : MonoBehaviour
{
    public Button destra, sinistra;
    public float velocita;
    public float movimento;
    // Use this for initialization
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (movimento==1)
            transform.Rotate(0, velocita * Time.deltaTime, 0, Space.World);
        if (movimento == 2)
            transform.Rotate(0, -velocita * Time.deltaTime, 0, Space.World);
    }
    
    public void Fermo()
    {
        movimento = 0;
    }
    public void MuoviSinistra()
    {
        movimento = 2;
    }
    
    public void MuoviDestra()
    {
        movimento = 1;
    }
}
