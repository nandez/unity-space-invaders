using System;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Enemy Grid Settings")]
    public int rows = 5;

    public int columns = 11;
    public float spacingUnit = 2.0f;

    [Header("Enemy Settings")]
    public Enemy[] enemyPrefabs;

    public AnimationCurve enemySpeed;
    public float missileAttackRate = 1.0f;
    public Bullet missilePrefab;

    public ShipMovement ship;

    public bool active = true;

    public Action onEnemiesCleared;
    public int Score { get; private set; }

    private int enemiesKilled;
    private int totalEnemies => rows * columns;
    private float enemiesKilledPercentage => enemiesKilled / (float)totalEnemies;
    private int remainingEnemies => totalEnemies - enemiesKilled;

    private Vector3 direction = Vector3.right;

    private void Awake()
    {
        GenerateEnemyLayout();
    }

    private void Start()
    {
        InvokeRepeating(nameof(FireMissiles), missileAttackRate, missileAttackRate);

        ship.onDestroy += HandleShipOnDestroyEvent;
    }

    private void Update()
    {
        if (active)
        {
            transform.position += direction * enemySpeed.Evaluate(enemiesKilledPercentage) * Time.deltaTime;

            var leftMapBorder = Camera.main.ViewportToWorldPoint(Vector3.zero);
            var rightMapBorder = Camera.main.ViewportToWorldPoint(Vector3.right);

            foreach (Transform enemy in transform)
            {
                if (!enemy.gameObject.activeInHierarchy)
                    continue;

                if ((direction == Vector3.right && enemy.position.x >= (rightMapBorder.x - 1.0f))
                    || (direction == Vector3.left && enemy.position.x <= (leftMapBorder.x + 1.0f)))
                {
                    MoveEnemiesDown();
                }
            }
        }
    }

    protected void GenerateEnemyLayout()
    {
        var gridWith = spacingUnit * (columns - 1);
        var gridHeight = spacingUnit * (rows - 1);

        Vector2 centerOffset = new Vector2(-gridWith / 2, -gridHeight / 2);

        for (int r = 0; r < rows; r++)
        {
            Vector3 rPosition = new Vector3(centerOffset.x, centerOffset.y + (r * spacingUnit), 0);

            for (int c = 0; c < columns; c++)
            {
                var enemy = Instantiate(enemyPrefabs[r], transform);
                enemy.onDestroy += () =>
                {
                    enemiesKilled++;
                    Score += enemy.ScorePoints;

                    if (remainingEnemies == 0)
                    {
                        active = false;
                        onEnemiesCleared?.Invoke();
                    }
                };

                var position = rPosition;

                position.x += c * spacingUnit;
                enemy.transform.localPosition = position;
            }
        }
    }

    protected void MoveEnemiesDown()
    {
        direction.x *= -1.0f;

        var pos = transform.position;
        pos.y -= 1.0f;
        transform.position = pos;
    }

    protected void FireMissiles()
    {
        if (active)
        {
            foreach (Transform enemy in transform)
            {
                if (!enemy.gameObject.activeInHierarchy)
                    continue;

                if (UnityEngine.Random.value < (1.0f / remainingEnemies))
                {
                    Instantiate(missilePrefab, enemy.position, Quaternion.identity);
                    break;
                }
            }
        }
    }

    protected void HandleShipOnDestroyEvent()
    {
        var possibleScores = new int[3] {50, 100, 150};
        Score += possibleScores[UnityEngine.Random.Range(0, possibleScores.Length - 1)];
    }
}