using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public Bullet bulletPrefab;
    public Sprite[] deathSprites;

    public Action onHit;
    public bool alive = true;

    private SpriteRenderer spriteRenderer;
    private Sprite defaultSprite;
    private int currentDeathSpriteIndex;
    private bool bulletSpawned = false;
    private Vector3 initialPosition;
    

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultSprite = spriteRenderer.sprite;
        initialPosition = transform.position;

        InvokeRepeating(nameof(HandleDestroyAnimation), 0, 0.25f);
    }

    private void Update()
    {
        var xMovement = Input.GetAxisRaw("Horizontal");

        if (xMovement > 0)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else if (xMovement < 0)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            FireBullet();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")
            || collision.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            onHit?.Invoke();
        }
    }

    protected void FireBullet()
    {
        if (!bulletSpawned)
        {
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.onDestroy = () => { bulletSpawned = false; };
            bulletSpawned = true;
        }
    }

    protected void HandleDestroyAnimation()
    {
        if (!alive)
        {
            currentDeathSpriteIndex++;

            if (currentDeathSpriteIndex >= deathSprites.Length)
                currentDeathSpriteIndex = 0;

            spriteRenderer.sprite = deathSprites[currentDeathSpriteIndex];
        }
    }

    public void ResetState()
    {
        spriteRenderer.sprite = defaultSprite;
        transform.position = initialPosition;
        alive = true;
    }
}