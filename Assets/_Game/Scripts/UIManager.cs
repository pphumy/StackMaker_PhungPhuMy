using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    // Start is called before the first frame update
    [SerializeField] GameObject PlayBtn;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject WinUI;


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
    }
    public void OnClickRePlayBtn()
    {
        LevelManager.Instance.LoadLevel();
        WinUI.SetActive(false);
        GameManager.Instance.StartGame();
    }

    public IEnumerator ShowWinUI()
    {
        yield return new WaitForSeconds(2);
        WinUI.SetActive(true);

    }
}
