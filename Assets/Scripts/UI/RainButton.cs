using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RainButton : MonoBehaviour, IPointerDownHandler
{
    public acidRain _acidRain;
    public float cooldown = 30;
    public GameObject mask;

    void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown < 0)
            cooldown = 0;
        mask.transform.localScale = new Vector3(1f * (cooldown / 30), mask.transform.localScale.y, mask.transform.localScale.z);
        mask.transform.localPosition  = new Vector3((63.5f * mask.transform.localScale.x) - 63.5f, mask.transform.localPosition.y, mask.transform.localPosition.x);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (cooldown > 0)
            return;
        Debug.Log("clicked");
        _acidRain.ActivateRain();
        cooldown = 30;
    }
}
