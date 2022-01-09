using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public Bullet bulletPrefab;
    public Sprite[] deathSprites;

    public string playerShootSound = "shoot";
    public string playerKilledSound = "explosion";

    public Action onHit;
    public bool alive = true;

    private AudioManager audioManager;
    private SpriteRenderer spriteRenderer;
    private Sprite defaultSprite;
    private int currentDeathSpriteIndex;
    private bool bulletSpawned = false;
    private Vector3 initialPosition;

    private Vector3 leftMapBorder;
    private Vector3 rightMapBorder;

    private void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
            Debug.LogError("No audiomanager found in scene...");

        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultSprite = spriteRenderer.sprite;
        initialPosition = transform.position;

        InvokeRepeating(nameof(HandleDestroyAnimation), 0, 0.25f);

        leftMapBorder = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightMapBorder = Camera.main.ViewportToWorldPoint(Vector3.right);
    }

    private void Update()
    {
        if (alive)
        {
            var xMovement = Input.GetAxisRaw("Horizontal");

            if (xMovement > 0)
            {
                var newPosition = transform.position + Vector3.right * speed * Time.deltaTime;

                if (newPosition.x <= (rightMapBorder.x - 1.0f))
                    transform.position = newPosition;
            }
            else if (xMovement < 0)
            {
                var newPosition = transform.position + Vector3.left * speed * Time.deltaTime;

                if (newPosition.x >= (leftMapBorder.x + 1.0f))
                    transform.position += Vector3.left * speed * Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                FireBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")
            || collision.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            audioManager.PlaySound(playerKilledSound);
            onHit?.Invoke();
        }
    }

    protected void FireBullet()
    {
        if (!bulletSpawned)
        {
            audioManager.PlaySound(playerShootSound);
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