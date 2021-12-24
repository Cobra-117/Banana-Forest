using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{
    public Tilemap tilemap;
    private Vector3 pos;
    private int[,] distanceMap;

    // Start is called before the first frame update
    void Start()
    {
        distanceMap = new int[tilemap.size.y, tilemap.size.x];
        for (int j = 0; j < tilemap.size.y; j++) {
            for (int i = 0; i < tilemap.size.x; i++) {
                distanceMap[j, i] = 99;
            }
       }   
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cellpos = tilemap.WorldToCell(transform.position);
        //Debug.Log("x: " + cellpos.x + "y: " + cellpos.y);   
    }
}
