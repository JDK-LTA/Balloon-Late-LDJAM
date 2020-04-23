using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonBehaviour : MonoBehaviour
{
    public Transform yPosition;
    public float windSpeed = 2f;
    public float size;

    public float maxTime;
    public float minSwipeDistance;

    float swStartTime;
    float swEndTime;
    Vector3 swStartPos;
    Vector3 swEndPos;
    float swDistance;
    float swTime;

    public delegate void OnInput();
    public event OnInput InputLeft;
    public event OnInput InputRight;
    public event OnInput InputDoubleTap;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.x > Screen.width / 2)
            {
                InputRight?.Invoke();
                RightToLeftWind();
            }
            else
            {
                InputLeft?.Invoke();
                LeftToRightWind();
            }
            //Debug.Log(rb.velocity.x);
        }
#endif
#if UNITY_IOS || UNITY_EDITOR
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x > Screen.width / 2)
            {
                InputRight?.Invoke();
                RightToLeftWind();
            }
            else
            {
                InputLeft?.Invoke();
                LeftToRightWind();
            }
        }
#endif
    }

    private void RightToLeftWind()
    {
        rb.AddForce(new Vector2(-windSpeed, 0), ForceMode2D.Force);
    }
    private void LeftToRightWind()
    {
        rb.AddForce(new Vector2(windSpeed, 0), ForceMode2D.Force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle")
        {
            Debug.Log("Dead");
        }
    }
}
