using UnityEngine;

public class Bunker : MonoBehaviour
{
    private int totalBricks;

    private void Start()
    {
        foreach (Transform brick in transform)
        {
            totalBricks++;
            brick.GetComponent<BunkerBrick>().onDestroy += OnBrickDestroy;
        }
    }

    private void OnBrickDestroy()
    {
        totalBricks--;

        if (totalBricks == 0)
            gameObject.SetActive(false);
    }
}