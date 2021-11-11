using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class fingerScanBehavior : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public fingerprintLineRenderer scanLine;

    bool is_scanUp = true;
    bool is_Press = false;

    public float holdTime = 2f;
    float currentHold = 0f;

    public void OnPointerDown(PointerEventData eventData)
    {
        is_Press = true;
        scanLine.is_Active = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        is_Press = false;
        scanLine.YPosition = 0;
        scanLine.is_Active = false;
        currentHold = 0;
    }

    public void Update()
    {
        if(is_Press)
        {
            currentHold += Time.deltaTime;

            //up
            if(is_scanUp)
            {
                if(scanLine.YPosition < scanLine.getHeight - scanLine.size)
                    scanLine.YPosition = scanLine.YPosition + scanLine.speed;
                else
                    is_scanUp = false;
            }
            else
            {
                if (scanLine.YPosition > 0)
                    scanLine.YPosition = scanLine.YPosition - scanLine.speed;
                else
                    is_scanUp = true;
            }
        }

        if(currentHold >= holdTime && is_Press)
        {
            GameObject.FindObjectOfType<safeManager>().result();
            is_Press = false;
            scanLine.YPosition = 0;
            scanLine.is_Active = false;
            currentHold = 0;
        }
    }
}
