using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }

    public void Replay(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
