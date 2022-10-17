using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SwipeDetection : MonoBehaviour
{
    private Vector2 startPos;
    public Vector2 direction;
    private bool directionChosen = false;

    public static bool swipeRight;
    public static bool  swipeLeft;
    public static bool swipeUp;
    public static bool swipeDown;

    // Start is called before the first frame update
    void Start()
    {
        swipeRight = false;
        swipeLeft = false;
        swipeUp = false;
        swipeDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        swipeRight = false;
        swipeLeft = false;
        swipeUp = false;
        swipeDown = false;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = touch.position;
                    directionChosen = false;
                    break;
                case TouchPhase.Moved:
                    direction = touch.position - startPos;
                    //touchText.SetActive(true);
                    directionChosen = false;
                    break;
                case TouchPhase.Ended:
                    directionChosen = true;
                    break;        

            }
        }
        if (directionChosen)
        {
            directionChosen = false;
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x > 0)
                {
                    swipeRight = true;
                }
                else
                {
                    swipeLeft = true;
                }

            }
            else
            {
                if (direction.y > 0)
                {
                    swipeUp = true;

                }
                else
                {
                    swipeDown = true;
                }
            }


        }

    }
}
