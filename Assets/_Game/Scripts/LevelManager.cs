using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] Transform origin;
    public int currentLevel = 0;
    [SerializeField] List<Map> maps = new List<Map>();

    public void LoadLevel()
    {
        if(currentLevel > maps.Count)
        {
            currentLevel = 0;
        }
        currentLevel = PlayerPrefs.GetInt("Current Level");
        Instantiate(maps[currentLevel], origin, true);
        PlayerManager.Instance.SetStartPoint(maps[currentLevel].StartPoint.position);
        PlayerPrefs.SetInt("Current Level",currentLevel);
        UIManager.Instance.SetLevelUI();
        
    }
    public void LoadNextLevel()
    {
        Destroy(maps[currentLevel]);
        currentLevel++;
        if (currentLevel > maps.Count-1)
        {
            currentLevel = 0;
        }
        
        Instantiate(maps[currentLevel], origin, true);
        PlayerPrefs.SetInt("Current Level", currentLevel);
        PlayerManager.Instance.SetStartPoint(maps[currentLevel].StartPoint.position);
        UIManager.Instance.SetLevelUI();
    }

}
