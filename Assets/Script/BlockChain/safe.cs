using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class safe : MonoBehaviour
{
    public bool is_correctSafe = false;
    public bool is_currentUse = false;
    public GameObject canvas;
    public Transform botLandingPoint;
    public Vector3 botLandinPointOffset = Vector3.zero;

    bool is_interac = false;

    private void OnValidate()
    {
        botLandingPoint.position = transform.position + botLandinPointOffset;
    }

    public void canvasActive()
    {
        canvas.SetActive(true);
        is_interac = true;
    }

    public void canvaseInactice()
    {
        canvas.SetActive(false);
        is_interac = false;
    }

    private void Update()
    {
        if (is_interac && !is_currentUse && Input.GetKeyDown(KeyCode.E))
            GameObject.FindObjectOfType<safeManager>().activePanel(is_correctSafe, this.GetComponent<safe>());
    }
}
