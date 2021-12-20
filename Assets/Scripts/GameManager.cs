using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public EnemyManager enemyManager;

    public Text livesTxt;
    public Text scoreTxt;

    private int playerLives = 3;

    private void Start()
    {
        player.onHit += PlayerOnHitEventHandler;
        enemyManager.onEnemiesCleared += StageClearedEventHandler;
    }

    private void Update()
    {
        scoreTxt.text = $"SCORE {enemyManager.Score}";
    }

    protected void PlayerOnHitEventHandler()
    {
        enemyManager.active = false;
        player.alive = false;

        playerLives--;

        if (playerLives == 0)
        {
            livesTxt.text = $"LIVES 0";
            Debug.Log("Show GAMEVOER");
        }
        else
        {
            livesTxt.text = $"LIVES {playerLives}";
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    protected void StageClearedEventHandler()
    {

    }

    protected void NewRound()
    {
        player.ResetState();
        enemyManager.active = true;
    }

}