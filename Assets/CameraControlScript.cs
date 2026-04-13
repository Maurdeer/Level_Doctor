using UnityEngine;
using System.Collections;

public class CameraControlScript : MonoBehaviour
{
    private GameObject levelCamera;
    private Vector2 previousCoordinates;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float MAX_ORTHOGRAPHIC_SIZE = 5f;
    [SerializeField] private float ZOOMED_IN_ORTHOGRAPHIC_SIZE = 2f;
    [SerializeField] private float GOAL_PAUSE_TIME = 2f;
    [SerializeField] private float GOAL_TRANSITION_TIME = 1f;
    private Vector3 mousePositionOffset;
    
    private bool IS_TRANSITION_PLAYING = false;
    private Vector3 Z_OFFSET = new Vector3(0, 0, -10);

    void Start()
    {
        levelCamera = GameManager.Instance.levelCameraObject;
        StartCoroutine(goalIntro());
    }

    private IEnumerator goalIntro()
    {
        InitializeIntro();
        GameObject playerCamera = GameManager.Instance.playerCameraObject;
        Camera cameraScript = levelCamera.GetComponent<Camera>();
        cameraScript.orthographicSize = 2f;
        
        yield return new WaitForSeconds(GOAL_PAUSE_TIME);

        // Initialize variables necessary for the timer
        float goalTransitionTimer = GOAL_TRANSITION_TIME;
        float currentProgress;
        Vector3 originalLevelCameraPosition = levelCamera.transform.position;

        while (goalTransitionTimer >= 0)
        {
            // Timer functionality
            float timeWaited = Time.deltaTime;
            goalTransitionTimer -= timeWaited;
            yield return new WaitForSeconds(timeWaited);
            currentProgress = (GOAL_TRANSITION_TIME - goalTransitionTimer) / GOAL_TRANSITION_TIME;

            // Interpolate between camera position and zoom.
            levelCamera.transform.position = Vector3.Lerp(originalLevelCameraPosition, playerCamera.transform.position + Z_OFFSET, currentProgress);
            cameraScript.orthographicSize = Mathf.Lerp(ZOOMED_IN_ORTHOGRAPHIC_SIZE, MAX_ORTHOGRAPHIC_SIZE, currentProgress);
        }

        CleanupAfterIntro();
        // Debug.Log("Finished camera transition");
    }

    private void InitializeIntro()
    {
        IS_TRANSITION_PLAYING = true;
        levelCamera.transform.position = GameManager.Instance.goalObject.transform.position + Z_OFFSET;
        GameManager.Instance.ObjectiveText.SetActive(true);
        GameManager.Instance.SetUIElementsActive(false);
    }

    private void CleanupAfterIntro()
    {
        IS_TRANSITION_PLAYING = false;
        GameManager.Instance.SetUIElementsActive(true);
        GameManager.Instance.ObjectiveText.SetActive(false);
    }


    void OnResetCameraPosition()
    {
        Debug.Log("Receiving message to reset camera position");
        levelCamera.transform.position = GameManager.Instance.playerCameraObject.transform.position;
    }

    void Update()
    {
        if (GameManager.Instance != null && !IS_TRANSITION_PLAYING && GameManager.Instance.gamePhase == 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                OnResetCameraPosition();
            }
        }
    }
    void FixedUpdate()
    {
        if (GameManager.Instance != null && !IS_TRANSITION_PLAYING && GameManager.Instance.gamePhase == 0)
        {
            
            Vector3 cameraOffset = new Vector3();
            cameraOffset.x = Input.GetAxis("Horizontal") * cameraSpeed;
            cameraOffset.y = Input.GetAxis("Vertical") * cameraSpeed;
            levelCamera.transform.position += new Vector3(cameraOffset.x, cameraOffset.y, 0);
        }        
    }
}
