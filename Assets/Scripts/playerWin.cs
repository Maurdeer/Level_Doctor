using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class playerWin : MonoBehaviour
{
    public GameObject winScreen;
    public string scenename;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        winScreen.SetActive(true);
        GameManager.Instance.PlayerManager.canMove = false;
        // GameManager.Instance.PlayerManager.SetPlayerColliderActive(false);
        GameManager.Instance.PlayerManager.ZeroXVelocity();
        StartCoroutine(GoToNextScene());
    }

    private IEnumerator GoToNextScene()
    {
        yield return new WaitForSeconds(2f);
        SceneChangerScript.ChangeScene(scenename);
    }
}
