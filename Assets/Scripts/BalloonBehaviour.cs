using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PU
{
    INFLATE, DEFLATE, ROCKET, SHIELD, AK47, RAY_GUN, NO_PU
}

public class BalloonBehaviour : MonoBehaviour
{
    public Transform yDownLimit;
    public float windSpeed = 2f;
    public float size = 0.6f;
    public float maxSize = 0.9f, minSize = 0.3f;
    Vector2 originalScale;

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

    bool puActive = false;
    bool hasPU = false;
    public PU localPu = PU.NO_PU;

    bool isShielded = false;
    public GameObject shield;

    bool isStarred = false;

    public int akAmmo = 5;
    public GameObject bulletPrefab;
    public Transform shootingPos;
    int orAmmo;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        transform.localScale = new Vector3(size, size);
        originalScale = transform.localScale;

        orAmmo = akAmmo;
    }

    float puTimer = 5f;
    float t = 0;
    void Update()
    {
        #region INPUT
        if (!isStarred)
        {

#if UNITY_STANDALONE || UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y > yDownLimit.position.y)
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
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                PowerUpButtonUsing();
            }
#endif
#if UNITY_IOS || UNITY_EDITOR || UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (Camera.main.ScreenToWorldPoint(touch.position).y > yDownLimit.position.y)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
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
                }
            }
#endif
        }
        #endregion

        if (puActive)
        {
            t += Time.deltaTime;
            if (t >= puTimer)
            {
                ClearPU();
            }
        }
    }

    public void PowerUpButtonUsing()
    {
        puActive = true;
        UsePU(true);
    }

    private void ChangeSize(bool up)
    {
        if (up)
        {
            if (size < 0.9f)
            {
                size += 0.15f;
            }
        }
        else
        {
            if (size > 0.3f)
            {
                size -= 0.15f;
            }
        }
        transform.localScale = new Vector3(size, size);
    }
    private void UsePU(bool up)
    {
        switch (localPu)
        {
            case PU.ROCKET:
                RocketPU(up);
                break;
            case PU.AK47:
                Ak47PU(up);
                break;
            case PU.RAY_GUN:
                if (up)
                {
                    RayGunPU();
                }
                break;
            case PU.NO_PU:
                break;
        }
    }
    private void ClearPU()
    {
        puActive = false;
        UsePU(false);
        localPu = PU.NO_PU;
        t = 0;
    }

    #region POWER UPS
    private void RocketPU(bool up)
    {
        isStarred = up;
        if (up)
        {
            GameManager.Instance.RocketActive();
            rb.velocity = Vector2.zero;
        }
        else
        {
            GameManager.Instance.RocketDeactive();
        }
    }
    private void ShieldPU(bool up)
    {

        isShielded = up;
        shield.SetActive(up);
        //if (up)
        //{
        //    //ACTIVATE SHIELD VISUAL EFFECT
        //    shield.SetActive(true);
        //}
        //else
        //{
        //    //DEACTIVATE SHIELD VISUAL EFFECT

        //}
    }
    private void Ak47PU(bool up)
    {
        if (up)
        {
            puTimer = 999999;
            //SHOOT PROJECTILE
            ShootProjectile();
            akAmmo--;
            if (akAmmo == 0)
            {
                ClearPU();
            }
        }
        else
        {
            //DEACTIVATE AK VISUAL EFFECT
            akAmmo = orAmmo;
        }
    }
    private void ShootProjectile()
    {
        GameObject go = Instantiate(bulletPrefab, shootingPos.position, shootingPos.rotation);
    }
    private void RayGunPU()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].transform.localScale /= 2;
        }

        ClearPU();
    }
    #endregion
    #region WINDS
    private void RightToLeftWind()
    {
        rb.AddForce(new Vector2(-windSpeed, 0), ForceMode2D.Force);
    }
    private void LeftToRightWind()
    {
        rb.AddForce(new Vector2(windSpeed, 0), ForceMode2D.Force);
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Powerup pu;
        if (collision.tag == "Obstacle")
        {
            Death(collision);
        }
        else if (pu = collision.GetComponent<Powerup>())
        {
            if (pu.size <= size)
            {


                switch (pu.powerUp)
                {
                    case PU.INFLATE:
                        ChangeSize(true);
                        break;
                    case PU.DEFLATE:
                        ChangeSize(false);
                        break;
                    case PU.SHIELD:
                        ShieldPU(true);
                        break;
                    case PU.ROCKET:
                    case PU.AK47:
                    case PU.RAY_GUN:
                        if (!hasPU)
                        {
                            hasPU = true;
                            localPu = pu.powerUp;
                            puTimer = pu.timer;
                            akAmmo = pu.akAmmo;
                        }
                        break;
                    default:
                        break;
                }

                Destroy(collision.gameObject);
            }
            else
            {
                Death(collision);
            }
        }
    }

    private void Death(Collider2D collision)
    {
        if (isStarred)
        {
            Debug.Log("Has star");
            Destroy(collision.gameObject);
        }
        else if (isShielded)
        {
            Debug.Log("Had shield");
            Destroy(collision.gameObject);
            ShieldPU(false);
        }
        else
        {
            Debug.Log("Dead");
        }
    }
}