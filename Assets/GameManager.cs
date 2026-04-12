using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int PlayerTurn;
    
    public int gamePhase; // 0 = move item, 1 = move player

    public TMP_Text PlayerTracker;
    public TMP_Text GamePhaseTracker;
    public PlayerManager PlayerManager;
    public UnityEngine.UI.Button button;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerTurn = 0;
        gamePhase = 0;
        
    }

    public void GamePhaseChange()
    {
        PlayerManager.toggleMove(); // movement locked
        if (gamePhase == 0)
        {
            gamePhase = 1;
            button.enabled = false;
            GamePhaseTracker.text = "Move yourself around!";
        }
        else
        {
            gamePhase = 0;
            button.enabled = true;
            GamePhaseTracker.text = "Move items around!";
        }

    }

    public void PlayerTurnChange()
    {
        PlayerTurn = (PlayerTurn + 1) % 2; // change player turn
        PlayerTracker.text = "Player" + (PlayerTurn + 1) + "'s Turn"; // update canvas

        PlayerManager.resetPosition(); // back to start
        GamePhaseChange();
        
    }

}
