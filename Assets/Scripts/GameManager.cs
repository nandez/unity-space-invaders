using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public EnemyManager enemyManager;
    public HomeLine homeLine;

    public Text livesTxt;
    public Text scoreTxt;

    public Action onGameOver;
    public Action onStageCleared;

    public int PlayerLives { get; private set; }

    private void Start()
    {
        player.onHit += PlayerOnHitEventHandler;
        enemyManager.onEnemiesCleared += StageClearedEventHandler;
        homeLine.onEnemyReached += HomeLineOnEnemyReachedEventHandler;
        PlayerLives = 3;
    }

    private void Update()
    {
        scoreTxt.text = $"SCORE {enemyManager.Score}";
    }

    protected void PlayerOnHitEventHandler()
    {
        enemyManager.active = false;
        enemyManager.ship.movementEnabled = false;
        player.alive = false;

        PlayerLives--;

        if (PlayerLives == 0)
        {
            livesTxt.text = $"LIVES 0";
            GameOver();
        }
        else
        {
            livesTxt.text = $"LIVES {PlayerLives}";
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    protected void HomeLineOnEnemyReachedEventHandler()
    {
        enemyManager.active = false;
        enemyManager.ship.movementEnabled = false;
        player.alive = false;
        PlayerLives = 0;

        GameOver();
    }

    protected void GameOver()
    {
        onGameOver?.Invoke();
    }

    protected void StageClearedEventHandler()
    {
        onStageCleared?.Invoke();
    }

    protected void NewRound()
    {
        player.ResetState();
        enemyManager.active = true;
        enemyManager.ship.ResetShip();
    }
}