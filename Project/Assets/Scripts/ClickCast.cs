using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ClickCast : MonoBehaviour
{
    public Grid grid; //  You can also use the Tilemap object
    // Start is called before the first frame update
    void Start()
    {
        Gizmos.color = Color.red;
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int coordinate = grid.WorldToCell(mouseWorldPos);
            grid.GetComponentInChildren<Tilemap>().SetTile(coordinate, null);
            Debug.Log(coordinate);
        }
    }
}
