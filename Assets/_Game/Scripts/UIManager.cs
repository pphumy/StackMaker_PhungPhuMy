using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    // Start is called before the first frame update
    [SerializeField] GameObject PlayBtn;
    [SerializeField] GameObject BackgroundSelection;
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
        GameManager.Instance.StartGame();
    }
    public void ShowInGameUI()
    {
        PlayBtn.SetActive(false);
        int level = PlayerPrefs.GetInt("Current Level") + 1;
        text.SetText("Level " + level);
        Debug.Log(level);
        
        Debug.Log("Score");

    }
    //public void ShowWinUi()
    //{
        
        
    //}

    //public void SetLevelUI()
    //{
    //    int level = PlayerPrefs.GetInt("Current level") + 1;
    //    text.SetText("Level "+ level);
    //}

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

    public IEnumerator ShowWinUI()
    {
        yield return new WaitForSeconds(3);
        WinUI.SetActive(true);

    }
}
