using System;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float shipAppearInterval = 5f;
    public float shipSpeed = 7.5f;
    public Action onDestroy;

    public bool movementEnabled = false;

    public string shipSound = "ufo";

    private AudioManager audioManager;
    private Vector3 direction = Vector3.right;
    private Vector3 initialPosition;

    private void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
            Debug.LogError("No AudioManager found in scene...");

        initialPosition = transform.position;

        Invoke(nameof(EnableShipMovement), shipAppearInterval);
    }

    private void Update()
    {
        if (movementEnabled)
        {
            transform.position += direction * shipSpeed * Time.deltaTime;

            var leftMapBorder = Camera.main.ViewportToWorldPoint(Vector3.zero);
            var rightMapBorder = Camera.main.ViewportToWorldPoint(Vector3.right);

            if ((direction == Vector3.right && transform.position.x >= (rightMapBorder.x + 5.0f))
                || (direction == Vector3.left && transform.position.x <= (leftMapBorder.x - 5.0f)))
            {
                // En este punto, la nave llegó al otro punto del mapa sin daños.
                // Damos vuelta la dirección y deshabilitamos el movimiento hasta el próximo intérvalo.
                direction.x *= -1.0f;
                movementEnabled = false;
                audioManager.StopSound(shipSound);
                Invoke(nameof(EnableShipMovement), shipAppearInterval);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            onDestroy?.Invoke();
            gameObject.SetActive(false);
            
            ResetShip();
        }
    }

    protected void EnableShipMovement()
    {
        movementEnabled = true;
        audioManager.PlaySound(shipSound);
    }

    public void ResetShip()
    {
        gameObject.SetActive(true);
        movementEnabled = false;
        transform.position = initialPosition;
        direction = Vector3.right;

        Invoke(nameof(EnableShipMovement), shipAppearInterval);
    }
}