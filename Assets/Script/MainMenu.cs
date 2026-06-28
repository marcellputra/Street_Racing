using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Panel")]
    public GameObject mainMenuPanel;
    public GameObject tutorialPanel;
    public GameObject levelPanel;
    public GameObject settingPanel;

    private void Start()
    {
        tutorialPanel.SetActive(false);
        levelPanel.SetActive(false);

        if (settingPanel != null)
            settingPanel.SetActive(false);
    }

    // Tombol PLAY
    public void OpenTutorial()
    {
        tutorialPanel.SetActive(true);
    }

    // Tombol Pilih Level
    public void OpenLevelPanel()
    {
        tutorialPanel.SetActive(false);
        levelPanel.SetActive(true);
    }

    // Tombol Kembali dari Pilih Level
    public void BackToTutorial()
    {
        levelPanel.SetActive(false);
        tutorialPanel.SetActive(true);
    }

    // Tombol Setting
    public void OpenSetting()
    {
        if (settingPanel != null)
            settingPanel.SetActive(true);
    }

    public void CloseSetting()
    {
        if (settingPanel != null)
            settingPanel.SetActive(false);
    }

    // Load Level
    public void LoadLevel1()
    {
        SceneManager.LoadScene("track2");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("track3");
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("track4");
    }

    // Exit Game
    public void ExitGame()
    {
        Application.Quit();

        Debug.Log("Keluar dari game");
    }
}