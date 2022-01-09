using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public Sprite[] sprites;
    public float animationSpeed = 0.75f;
    public Action onDestroy;

    public string enemyKilledSound = "enemy_killed";

    public int ScorePoints = 0;

    private AudioManager audioManager;
    private SpriteRenderer spriteRenderer;
    private int currentSpriteIndex = 0;


  
    private void Awake()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
            Debug.LogError("No AudioManager was found in scene...");

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(HandleSpriteAnimation), animationSpeed, animationSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            audioManager.PlaySound(enemyKilledSound);

            onDestroy?.Invoke();
            gameObject.SetActive(false);
        }
    }

    protected void HandleSpriteAnimation()
    {
        currentSpriteIndex++;

        if (currentSpriteIndex >= sprites.Length)
            currentSpriteIndex = 0;

        spriteRenderer.sprite = sprites[currentSpriteIndex];
    }
}