using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeLine : MonoBehaviour
{
    public Action onEnemyReached;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            onEnemyReached?.Invoke();
        }
    }
}
