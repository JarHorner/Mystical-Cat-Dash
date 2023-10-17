using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Image credits;
    [SerializeField] private Image settings;
    [SerializeField] private Image linkWarning;
    private bool creditMenuOpen = false;
    private bool settingsMenuOpen = false;
    private bool linkWarningMenuOpen = false;
    private string linkName;

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

    public void ToggleSettingsMenu()
    {
        if (settingsMenuOpen)
        {
            settings.gameObject.SetActive(false);
            settingsMenuOpen = false;
        }
        else
        {
            settings.gameObject.SetActive(true);
            settingsMenuOpen = true;
        }
    }

    // opens a webpage of the url chosen
    public void OpenLink()
    {

        linkWarning.gameObject.SetActive(false);

        switch (linkName)
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

    // opens the link warning panel
    public void ToggleLinkWarning(string credit = "")
    {
        if (linkWarningMenuOpen)
        {
            linkWarning.gameObject.SetActive(false);
            linkWarningMenuOpen = false;
        }
        else
        {
            linkWarning.gameObject.SetActive(true);
            linkWarningMenuOpen = true;
        }

        linkName = credit;
    }
}
