using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    private void Start()
    {
        transform.localScale *= GameManager.Instance.explosiveRadius;
    }
    void AutoDestroy()
    {
        Destroy(gameObject);
    }
}
