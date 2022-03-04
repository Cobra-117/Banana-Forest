using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject Tower;
    public int towerPrice;
    public DistanceMap distanceMapScript;
    public GameObject mask;

    private GLOBAL global;

    void Start()
    {
        global = GameObject.FindGameObjectWithTag("Global").GetComponent<GLOBAL>();
    }

    void Update()
    {
        if (global.money < towerPrice)
            mask.SetActive(true);
        else
            mask.SetActive(false);
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
        Vector3Int cell = distanceMapScript.tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (distanceMapScript.tilemap.GetTile<TileBase>(cell).name != "grass_03-export")
            return;
        GameObject NewTower = Instantiate(Tower);
        //Mettre une vérification pour savoir si la tile est libre
        //vérifier si on a assez d'argent
        NewTower.transform.position = new Vector3 (distanceMapScript.tilemap.CellToWorld(cell).x + 0.32f,
            distanceMapScript.tilemap.CellToWorld(cell).y + 0.32f, 0);
        global.money -= towerPrice;
    }

    public bool hasEnoughMoney(int price)
    {
        if (price > global.money)
            return false;
        return true;
    }
}
