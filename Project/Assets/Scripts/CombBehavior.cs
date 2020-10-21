using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombBehavior : MonoBehaviour
{
    enum Capacity { EMPTY, LOW, MEDIUM, HIGH }

    public Sprite[] sprites;

    Capacity myCapacity;
    PlayerController playerController;

    float timer;
    float interval;

    // Start is called before the first frame update
    void Start()
    {
        myCapacity = Capacity.HIGH;
        RefreshSprite();
        playerController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > (1/Mathf.Max(playerController.bees, 1))*5)
        {
            timer = Random.Range(0,3);
            if ((int)myCapacity < 3)
                myCapacity = myCapacity+1;
            RefreshSprite();
        }
    }

    public int OnClick()
    {
        int honeyToReturn = 1;
        switch (myCapacity)
        {
            case Capacity.EMPTY:
                Debug.Log("Clicked on Empty Honeycomb!");
                honeyToReturn = 0;
                break;

            case Capacity.LOW:
                myCapacity = Capacity.EMPTY;
                break;

            case Capacity.MEDIUM:
                myCapacity = Capacity.LOW;
                break;

            case Capacity.HIGH:
                myCapacity = Capacity.MEDIUM;
                break;
        }
        RefreshSprite();
        return honeyToReturn;
    }

    void RefreshSprite()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = sprites[(int)myCapacity];
    }
}
