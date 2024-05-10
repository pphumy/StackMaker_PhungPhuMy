using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public EDirection direction;
    private static InputManager instance;   
    public static InputManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<InputManager>();
            }
            return instance;
        }

    }

    
    private void Awake()
    {
        instance = this;
        OnInit();
    }
    
    

    void Update()
    {
        Swipe();
    }
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    private void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //normalize the 2d vector
            currentSwipe.Normalize();

            //swipe upwards
            if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.6f)
            {
                direction = EDirection.Forward;
            }
            //swipe down
            else if (currentSwipe.y < 0 && (currentSwipe.x > -0.5f && currentSwipe.x < 0.6f))
            {
                Debug.Log("down swipe");
                direction = EDirection.Backward;
            }
            //swipe left
            else if (currentSwipe.x < 0 && (currentSwipe.y > -0.5f && currentSwipe.y < 0.6f))
            {
                Debug.Log("left swipe");
                direction = EDirection.Left;
            }
            //swipe right
            else if (currentSwipe.x > 0 && (currentSwipe.y > -0.5f && currentSwipe.y < 0.6f))
            {
                Debug.Log("right swipe");
                direction = EDirection.Right;
            }
        }
    }
    void OnInit()
    {
        direction = EDirection.None;
    }

}
