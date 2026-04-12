using UnityEngine;

public class playerWin : MonoBehaviour
{
    public GameObject winScreen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        winScreen.SetActive(true);
        
    }
}
