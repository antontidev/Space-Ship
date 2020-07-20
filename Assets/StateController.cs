using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public GameObject lose;
    public GameObject win;
    void Start()
    {
        var obj = GameObject.FindGameObjectsWithTag("Ready")[0];

        var comp = obj.GetComponent<IsReady>();

        if (comp.isReady == true)
        {
            win.SetActive(true);
        }
        else
        {
            lose.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
