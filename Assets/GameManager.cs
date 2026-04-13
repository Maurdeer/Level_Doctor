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
    [SerializeField] public GameObject goalObject; 
    [SerializeField] public GameObject levelCameraObject;
    [SerializeField] public GameObject playerCameraObject;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        PlayerTurn = 0;
        gamePhase = 0;
        gameManager = this;
        levelCameraObject.transform.position = playerCameraObject.transform.position;
    }

    public void GamePhaseChange()
    {
        PlayerManager.toggleMove(); // movement locked
        ToggleCamera();
        if (gamePhase == 0)
        {
            gamePhase = 1;
            SetButtonsActive(false);
            GamePhaseTracker.text = "Move yourself around!";
        }
        else
        {
            gamePhase = 0;
            SetButtonsActive(true);
            GamePhaseTracker.text = "Move items around!";
        }
        

    }

    public void SetButtonsActive(bool value)
    {
        button.gameObject.SetActive(value);
        button.enabled = value;
    }
    // Currently toggles between level and player camera.
    private void ToggleCamera()
    {
        playerCameraObject.SetActive(!playerCameraObject.active);
        levelCameraObject.SetActive(!levelCameraObject.active);
        if (levelCameraObject.active) levelCameraObject.transform.position = playerCameraObject.transform.position;
        
    }

    public void SetIntroUIElementsActive(bool value)
    {
        PlayerTracker.gameObject.SetActive(value);
        button.gameObject.SetActive(value);
        button.enabled = value;
        GamePhaseTracker.gameObject.SetActive(value);
        ObjectiveText.SetActive(!value);
    }

    public void PlayerTurnChange()
    {
        PlayerTurn = (PlayerTurn + 1) % 2; // change player turn
        PlayerTracker.text = "Player" + (PlayerTurn + 1) + "'s Turn"; // update canvas

        PlayerManager.resetPosition(); // back to start
        GamePhaseChange();
        
    }

}
