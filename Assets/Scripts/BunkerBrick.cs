using System;
using UnityEngine;

public class BunkerBrick : MonoBehaviour
{
    public Action onDestroy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Missile")
            || collision.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            onDestroy?.Invoke();
            gameObject.SetActive(false);
        }
    }
}