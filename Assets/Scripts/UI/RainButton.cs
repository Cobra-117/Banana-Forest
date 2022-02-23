using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RainButton : MonoBehaviour, IPointerDownHandler
{
    public acidRain _acidRain;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("clicked");
        _acidRain.ActivateRain();
    }
}
