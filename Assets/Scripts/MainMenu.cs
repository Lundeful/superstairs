using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject canvas;
    public GameObject background;
    public GameObject playButton;
    public GameObject exitButton;
    private Image image;
    private Image image2;
    private Image image3;
    byte r = 255;
    byte g = 255;
    byte b = 255;

    IEnumerator waiter()
    {
        while (r > 0)
        {
            image.color = new Color32(r, g, b, 255);
            image2.color = new Color32(r, g, b, 255);
            image3.color = new Color32(r, g, b, 255);
            r--;
            g--;
            b--;
            //Wait for 4 seconds
            yield return new WaitForSeconds(0.01F);
        }
        setScene();
    }

    public void startGame()
    {
        image = background.GetComponentInChildren<Image>();
        image2 = playButton.GetComponentInChildren<Image>();
        image3 = exitButton.GetComponentInChildren<Image>();
        r = 255;
        g = 255;
        b = 255;
        StartCoroutine(waiter());
    }

    private void setScene()
    {
        SceneManager.LoadScene("Prototype_Lund");
    }

    public void exitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
