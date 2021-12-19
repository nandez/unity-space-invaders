using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;

    public Text gameMessage;

    private int playerLives = 3;

    public void Awake()
    {
        player = GetComponent<Player>();
        player.onPlayerDestroyed += PlayerDestroyed;
    }

    public void PlayerDestroyed()
    {
        playerLives--;
        
        if(playerLives <= 0)
        {
            // Show game over.
            Debug.Log("GAME OVER!");
        }
        else
        {
            // UPdate GUI
            Debug.Log("LOST LIVE");
        }
    }

}