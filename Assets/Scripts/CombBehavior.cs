using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombBehavior : MonoBehaviour
{
    enum Capacity { EMPTY, LOW, MEDIUM, HIGH, REGULAR, MAGIC, QUEEN }

    public Sprite[] sprites;

    Capacity myCapacity;
    PlayerController playerController;

    float timer;
    float interval;

    bool clump;

    // Start is called before the first frame update
    void Start()
    {
        myCapacity = Capacity.HIGH;
        RefreshSprite();
        playerController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerController>();
        timer = Random.Range(0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!clump)
        {
            timer += Time.deltaTime;

            if (IsBeeTime(timer))
            {
                timer = Random.Range(0f, 1f);
                if ((int)myCapacity < 3)
                    myCapacity = myCapacity + 1;
                RefreshSprite();
            }
        }
    }

    public int OnClick()
    {
        int honeyToReturn = 0;
        if (!clump)
        {

            honeyToReturn = 1;
            switch (myCapacity)
            {
                case Capacity.EMPTY:
                    Debug.Log("Clicked on Empty Honeycomb!");
                    honeyToReturn = 0;
                    break;

                case Capacity.LOW:
                    myCapacity = Capacity.EMPTY;
                    EvaluateClump();
                    break;

                case Capacity.MEDIUM:
                    myCapacity = Capacity.LOW;
                    break;

                case Capacity.HIGH:
                    myCapacity = Capacity.MEDIUM;
                    break;
            }
            RefreshSprite();
        }
        else
        {
            clump = false;
            switch ((int)myCapacity)
            {
                case 4:
                    honeyToReturn = 5;
                    break;
                case 5:
                    honeyToReturn = 10;
                    break;
                case 6:
                    honeyToReturn = 25;
                    break;
                default:
                    break;
            }
            myCapacity = Capacity.EMPTY;
            RefreshSprite();
        }
        return honeyToReturn;
    }

    void RefreshSprite()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = sprites[(int)myCapacity];
    }

    bool IsBeeTime(float time)
    {
        float curveValue = 1 / Mathf.Pow(1.014f, playerController.bees - 170f);
        return time > curveValue - Random.Range(0f, curveValue/2f);
    }
    
    void EvaluateClump()
    {
        float eval = Random.Range(0f, 1f);
        // 70% normal, 20% magic, 10% queen
        if (eval > 0.95f)
        {
            clump = true;
            myCapacity = Capacity.QUEEN;
            RefreshSprite();
        }
        else if (eval > 0.85f)
        {
            clump = true;
            myCapacity = Capacity.MAGIC;
            RefreshSprite();
        }
        else if (eval > 0.7f)
        {
            clump = true;
            myCapacity = Capacity.REGULAR;
            RefreshSprite();
        }
    }
}
