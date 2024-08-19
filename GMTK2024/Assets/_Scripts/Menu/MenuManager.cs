using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{


    public void QuitGame()
    {
        Application.Quit();
    }

    public void RunGame()
    {
        SceneManager.LoadScene(1);
    }
}
