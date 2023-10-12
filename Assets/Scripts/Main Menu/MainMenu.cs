using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Image credits;
    private bool creditMenuOpen = false;

    public void StartGame()
    {
        SceneManager.LoadScene("Runner");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleCreditsMenu()
    {
        if (creditMenuOpen)
        {
            credits.gameObject.SetActive(false);
            creditMenuOpen = false;
        }
        else
        {
            credits.gameObject.SetActive(true);
            creditMenuOpen = true;
        }
    }

    public void OpenLink(GameObject credit)
    {
        string creditName = credit.name;

        switch (creditName)
        {
            case "Font":
                Application.OpenURL("https://www.fontspace.com/medieval-sharp-font-f17170");
                break;
            case "3D Runner":
                Application.OpenURL("https://assetstore.unity.com/packages/3d/characters/creatures/gambler-cat-20897");
                break;
            case "2D Runner":
                Application.OpenURL("https://assetstore.unity.com/packages/tools/game-toolkits/platformer-game-kit-200756");
                break;
            case "3D Environment":
                Application.OpenURL("https://assetstore.unity.com/packages/3d/environments/lowpoly-environment-nature-free-medieval-fantasy-series-187052");
                break;
            case "2D Environment":
                Application.OpenURL("https://aamatniekss.itch.io/free-pixelart-tileset-cute-forest");
                break;
            case "Music":
                Application.OpenURL("www.soundimage.org");
                break;
            default:
                break;
        }
    }
}
