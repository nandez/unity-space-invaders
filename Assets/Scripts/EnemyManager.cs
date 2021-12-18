using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int rows = 5;
    public int columns = 11;
    public float spacingUnit = 2.0f;

    public Enemy[] enemyPrefabs;

    private void Awake()
    {
        GenerateEnemyLayout();
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
                var position = rPosition;

                position.x += c * spacingUnit;
                enemy.transform.localPosition = position;
            }
        }
    }
}