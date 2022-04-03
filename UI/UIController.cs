using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private DiamondTaker diamondTaker;
    [SerializeField] private GameObject endGamePanelPrefab;
    [SerializeField] private Canvas canvas;
    private void Start()
    {
        player.Health.OnDestroy += ShowLooseGameUI;
        diamondTaker.allDiamondsCollected += ShowWinGameUI;
    }
    private void ShowLooseGameUI()
    {
        GameObject prefab = Instantiate(endGamePanelPrefab, canvas.transform);
        //prefab.GetComponent<Panel>().GetRestartButton().onClick.AddListener(RestartGame);
        //prefab.GetComponent<Panel>().GetQuitButton().onClick.AddListener(ExitGame);
        Debug.Log("Show ui");
        Time.timeScale = 0;
        prefab.GetComponent<Panel>().GetMainText().text = "GAME OVER";
        prefab.SetActive(true);
    }
    private void ShowWinGameUI()
    {
        Time.timeScale = 0;
        endGamePanel.GetComponent<Panel>().GetMainText().text = "YOU WIN";
        endGamePanel.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !endGamePanel.activeSelf)
        {
            Time.timeScale = 0;
            endGamePanel.GetComponent<Panel>().GetMainText().text = "PAUSE MENU";
            endGamePanel.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && endGamePanel.activeSelf)
        {
            Time.timeScale = 1;
            endGamePanel.SetActive(false);
        }
    }
}
