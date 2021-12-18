using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Sprite[] sprites;
    public float animationSpeed = 0.75f;

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

    private void Update()
    {
        
    }

    protected void HandleSpriteAnimation()
    {
        currentSpriteIndex++;

        if (currentSpriteIndex >= sprites.Length)
            currentSpriteIndex = 0;

        spriteRenderer.sprite = sprites[currentSpriteIndex];
    }
}