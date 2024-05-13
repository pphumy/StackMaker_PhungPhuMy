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

    public void Play()
    {
        PlayBtn.SetActive(false);
        LevelManager.Instance.LoadLevel();
        
    }
    //public void ShowWinUi()
    //{
        
        
    //}

    public void SetLevelUI()
    {
        int level = PlayerPrefs.GetInt("Current level") + 1;
        text.SetText("Level "+ level);
    }

    public void NextLevel()
    {
        LevelManager.Instance.LoadNextLevel();
        WinUI.SetActive(false);
    }

    public IEnumerator ShowWinUI()
    {
        yield return new WaitForSeconds(2);
        WinUI.SetActive(true);
    }
}
