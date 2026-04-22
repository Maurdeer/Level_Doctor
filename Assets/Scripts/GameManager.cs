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
    [SerializeField] public GameObject movementDirectionsObject;
    [SerializeField] public GameObject levelEditorDirectionsObject;
    [SerializeField] public TMP_Text movementDirectionsPlayerText;
    [SerializeField] public TMP_Text levelDirectionsPlayerText;
    [SerializeField] private CameraControlScript cameraScript;
    public bool IS_TRANSITION_PLAYING;

    private GameObject activeDescriptionTextObject;
    // private TMP_Text CurrentTaskDescriptiveText;


    
    void Start()
    {
        PlayerTurn = 1;
        gamePhase = 0;
        gameManager = this;
        levelCameraObject.transform.position = playerCameraObject.transform.position;
        PlayerManager.SetPlayerColliderActive(false);
    }

    public void GamePhaseChange()
    {
        ToggleCamera();
        
        SetIntroUIElementsActive(false);
        if (gamePhase == 0)
        {
            activeDescriptionTextObject = movementDirectionsObject;
            SetButtonsActive(false);
        }
        else
        {
            PlayerManager.toggleMove(); // movement locked
            activeDescriptionTextObject = levelEditorDirectionsObject;
        }
        IS_TRANSITION_PLAYING = true;
        SetDescriptiveObjectActive(true);
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
        GamePhaseTracker.gameObject.SetActive(value);

    }


    public void ResetOnPlayerDeath()
    {
        PlayerManager.resetPosition();
        GamePhaseChange();
    }
    private void PlayerTurnChange()
    {
        PlayerTurn = (PlayerTurn + 1) % 2; // change player turn
        PlayerTracker.text = "Player " + (PlayerTurn + 1) + "'s Turn"; // update canvas
        levelDirectionsPlayerText.text = "Player " + (PlayerTurn + 1)   + "'s Go!";
        movementDirectionsPlayerText.text = "Player " + (2 - PlayerTurn) + "'s Go!";
    }

    public void SetDescriptiveObjectActive(bool value)
    {
        activeDescriptionTextObject.SetActive(value);
    }

    public void ConfirmDirectionsRead()
    {
        SetDescriptiveObjectActive(false);
        SetIntroUIElementsActive(true);
        IS_TRANSITION_PLAYING = false;
        

        if (gamePhase == 0)
        {
            PlayerManager.toggleMove(); // unlock movement
            gamePhase = 1;
            PlayerManager.SetPlayerColliderActive(true);
            GamePhaseTracker.text = "Reach the goal!";
            PlayerTurnChange();
        } else if (gamePhase == 1)
        {
            gamePhase = 0;
            cameraScript.IS_RESETTING_CAMERA = false;
            PlayerManager.SetPlayerColliderActive(false);
            GamePhaseTracker.text = "Move items around!";
            SetButtonsActive(true);
            
            
        }
    }
}
