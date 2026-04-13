using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManager;

    public static GameManager Instance
    {
        get => gameManager;
        set
        {
            if (gameManager == null) gameManager = value;
        }
    }
    int PlayerTurn;
    
    public int gamePhase; // 0 = move item, 1 = move player

    public TMP_Text PlayerTracker;
    public TMP_Text GamePhaseTracker;
    public PlayerManager PlayerManager;
    public UnityEngine.UI.Button button;
    public GameObject ObjectiveText;

    // I love boilerplate
    [SerializeField] private GameObject buttonGameObject;
    [SerializeField] public GameObject goalObject; 
    [SerializeField] public GameObject levelCameraObject;
    [SerializeField] public GameObject playerCameraObject;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        PlayerTurn = 0;
        gamePhase = 0;
        gameManager = this;
        // buttonGameObject = button.GameObject;
        levelCameraObject.transform.position = playerCameraObject.transform.position;
    }

    public void GamePhaseChange()
    {
        PlayerManager.toggleMove(); // movement locked
        if (gamePhase == 0)
        {
            gamePhase = 1;
            button.enabled = false;
            buttonGameObject.SetActive(false);
            GamePhaseTracker.text = "Move yourself around!";
            playerCameraObject.SetActive(true);
            levelCameraObject.SetActive(false);
        }
        else
        {
            gamePhase = 0;
            button.enabled = true;
            buttonGameObject.SetActive(true);
            GamePhaseTracker.text = "Move items around!";
            playerCameraObject.SetActive(false);
            levelCameraObject.transform.position = playerCameraObject.transform.position;
            levelCameraObject.SetActive(true);
            
        }

    }

    public void SetUIElementsActive(bool value)
    {
        PlayerTracker.gameObject.SetActive(value);
        button.gameObject.SetActive(value);
        GamePhaseTracker.gameObject.SetActive(value);
    }

    public void PlayerTurnChange()
    {
        PlayerTurn = (PlayerTurn + 1) % 2; // change player turn
        PlayerTracker.text = "Player" + (PlayerTurn + 1) + "'s Turn"; // update canvas

        PlayerManager.resetPosition(); // back to start
        GamePhaseChange();
        
    }

}
