using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject Tower;
    public DistanceMap distanceMapScript;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name + " Was Clicked.");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("On End Drag");
        GameObject NewTower = Instantiate(Tower);
        Vector3Int cell = distanceMapScript.tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        NewTower.transform.position = distanceMapScript.tilemap.CellToWorld(cell);
    }
}
