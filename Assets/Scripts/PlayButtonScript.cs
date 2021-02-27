using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButtonScript : MonoBehaviour
{

    public Button startButton;
    public void Start()
    {
        startButton.onClick.AddListener(TaskOnClick);
        startButton.onClick.AddListener(TaskOnClick);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void TaskOnClick()
    {

    }

    public void exitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
