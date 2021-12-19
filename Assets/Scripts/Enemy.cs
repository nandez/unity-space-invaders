using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public Sprite[] sprites;
    public float animationSpeed = 0.75f;
    public Action onDestroy;

    public int ScorePoints = 0;

    private SpriteRenderer spriteRenderer;
    private int currentSpriteIndex = 0;


  
    private void Awake()
    {
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