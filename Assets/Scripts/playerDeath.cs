using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerDeath : MonoBehaviour
{

    public GameManager Manager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Manager.ResetOnPlayerDeath();
    }
   
}
