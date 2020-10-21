using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int honey;
    public int bees;

    // Start is called before the first frame update
    void Start()
    {
        honey = 0;
        bees = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector3.forward, 100.0f);
            if (hit)
            {
                if (hit.transform.gameObject.GetComponent<CombBehavior>())
                {
                    honey += hit.transform.gameObject.GetComponent<CombBehavior>().OnClick();
                    Debug.Log("You have " + honey + " honey.");
                }
            }
        }
    }
}
