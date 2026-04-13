using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerScript : MonoBehaviour
{
    public static void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
