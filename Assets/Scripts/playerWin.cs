using UnityEngine;
using System.Collections;

public class playerWin : MonoBehaviour
{
    public GameObject winScreen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        winScreen.SetActive(true);
        GameManager.Instance.PlayerManager.canMove = false;
        GameManager.Instance.PlayerManager.SetPlayerColliderActive(false);
        StartCoroutine(GoToMainMenu());
    }

    private IEnumerator GoToMainMenu()
    {
        yield return new WaitForSeconds(2f);
        SceneChangerScript.ChangeScene("Title Screen");
    }
}
