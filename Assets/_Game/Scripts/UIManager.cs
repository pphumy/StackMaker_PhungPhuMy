using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    // Start is called before the first frame update
    [SerializeField] GameObject PlayBtn;
    [SerializeField] GameObject BackgroundSelection;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject PauseBtn;
    [SerializeField] TextMeshProUGUI text;
    
    public TextMeshProUGUI scoreUi;
    [SerializeField] GameObject WinUI;

    private void OnInit()
    {
        scoreUi.SetText(" ");
        PlayerManager.Instance.score = 0;
    }
    public void ShowMainUI()
    {
        
        PlayBtn.SetActive(true);
    }

    public void OnclickPlay()
    {
        //PlayerManager.Instance.setHeight();
        PlayerManager.Instance.setHeight();
        PlayerManager.Instance.ClearBrick();
        text.gameObject.SetActive(true);
        GameManager.Instance.StartGame();
    }
    
    public void ShowInGameUI()
    {
        PlayBtn.SetActive(false);
        PauseBtn.SetActive(true);
        int level = PlayerPrefs.GetInt("Current Level") + 1;
        text.SetText("Level " + level);
        Debug.Log(level);
        
        Debug.Log("Score");

    }

    public void OnClickNextLevelBtn()
    {
        LevelManager.Instance.LoadNextLevel();
        WinUI.SetActive(false);
        GameManager.Instance.StartGame();
        OnInit();
    }
    public void OnClickRePlayBtn()
    {
        LevelManager.Instance.LoadLevel();
        WinUI.SetActive(false);
        GameManager.Instance.StartGame();
        OnInit();
    }
    public void OnClickChangeBackGroundBtn()
    {
        BackgroundSelection.SetActive(true);
        
    }
    public void OnClickResume()
    {
        PauseMenu.SetActive(false);
        PauseBtn.SetActive(true);
        Time.timeScale = 1f;
        GameManager.Instance.ChangeState(GameManager.GameState.Replay);
    }
    
    public void OnClickHome()
    {
        PauseMenu.SetActive(false);
        PauseBtn.SetActive(false);
        Time.timeScale = 1f;
        
        //PlayerManager.Instance.setHeightEnd();
        PlayerManager.Instance.ClearBrick();
        WinUI.SetActive(false);
        PlayerManager.Instance.OnInit();
        LevelManager.Instance.ClearCurrentLevel();
        
        OnInit();
        text.gameObject.SetActive(false);
        
        
        ShowMainUI();
        GameManager.Instance.ChangeState(GameManager.GameState.MainMenu);
    }
    public void OnClickPause()
    {
        GameManager.Instance.ChangeState(GameManager.GameState.Pause);
        PauseMenu.SetActive(true);
        PauseBtn.SetActive(false);
        Time.timeScale = 0f;
    }

    public IEnumerator ShowWinUI()
    {
        yield return new WaitForSeconds(3);
        WinUI.SetActive(true);

    }
}
