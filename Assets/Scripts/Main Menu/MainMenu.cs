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
    [SerializeField] private RectTransform fader;

    void Update()
    {
        if (Tween.Instance.backToMainMenu)
        {
            Tween.Instance.backToMainMenu = false;
            Tween.Instance.TweenEnd();
        }

    }

    public void StartGame()
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.gameObject.SetActive(true);
            //GameManager.Instance.loaded = false;
        }
        if (GameUI.Instance)
        {
            GameUI.Instance.score.transform.parent.gameObject.SetActive(true);
        }

        Tween.Instance.TweenStartGame(fader);
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
            case "GUI Panels":
                Application.OpenURL("https://kanekizlf.itch.io/fantasy-wooden-gui-free");
                break;
            case "GUI Buttons":
                Application.OpenURL("https://assetstore.unity.com/packages/2d/gui/icons/game-gui-buttons-96277");
                break;
            case "3D Player":
                Application.OpenURL("https://assetstore.unity.com/packages/3d/characters/creatures/gambler-cat-20897");
                break;
            case "2D Player":
                Application.OpenURL("https://assetstore.unity.com/packages/tools/game-toolkits/platformer-game-kit-200756");
                break;
            case "3D Environment":
                Application.OpenURL("https://assetstore.unity.com/packages/3d/environments/lowpoly-environment-nature-free-medieval-fantasy-series-187052");
                break;
            case "2D Environment":
                Application.OpenURL("https://aamatniekss.itch.io/free-pixelart-tileset-cute-forest");
                break;
            case "Coin":
                Application.OpenURL("https://skfb.ly/oqGSQ");
                break;
            case "Running Cat":
                Application.OpenURL("https://www.clipartmax.com/middle/m2K9A0A0K9K9m2b1_clipart-cat-running-cats-free-stock-photo-illustration-cat-running-clip-art/");
                break;
            case "Shield":
                Application.OpenURL("https://pngtree.com/so/shield");
                break;
            case "Magnet":
                Application.OpenURL("https://www.flaticon.com/free-icon/magnet-hand-drawn-tool-outline_35593");
                break;
            case "X":
                Application.OpenURL("https://favpng.com/png_view/symbol-multiplication-sign-symbol-clip-art-number-png/Z0Z0VSwu");
                break;
            case "2":
                Application.OpenURL("https://www.freebiefindingmom.com/free-printable-number-bubble-letters-bubble-number-2/");
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
