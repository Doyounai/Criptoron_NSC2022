using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testShowCSV : MonoBehaviour
{
    public CSVReader myData;

    public void Start()
    {
        Debug.Log(myData.DATA[0].Date);
    }
}
