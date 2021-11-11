using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tabletController : MonoBehaviour
{
    public GameObject tablet;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            tablet.SetActive(!tablet.active);
        }
    }
}
