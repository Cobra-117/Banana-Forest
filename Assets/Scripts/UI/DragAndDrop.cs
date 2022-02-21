using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject Tower;
    public int towerPrice;
    public DistanceMap distanceMapScript;

    private GLOBAL global;

    void Start()
    {
        global = GameObject.FindGameObjectWithTag("Global").GetComponent<GLOBAL>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name + " Was Clicked.");
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Drag");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (hasEnoughMoney(towerPrice) == false)
            return;
        Debug.Log("On End Drag");
        GameObject NewTower = Instantiate(Tower);
        Vector3Int cell = distanceMapScript.tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        //Mettre une vérification pour savoir si la tile est libre
        //vérifier si on a assez d'argent
        NewTower.transform.position = new Vector3 (distanceMapScript.tilemap.CellToWorld(cell).x + 0.32f,
            distanceMapScript.tilemap.CellToWorld(cell).y + 0.32f, 0);
    }

    public bool hasEnoughMoney(int price)
    {
        if (price > global.money)
            return false;
        return true;
    }
}
