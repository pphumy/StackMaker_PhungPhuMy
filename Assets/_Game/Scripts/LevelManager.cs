using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] Transform origin;
    Map currentMap;
    public int currentLevel = 0;
    
    [SerializeField] List<Map> maps = new List<Map>();

    public void LoadLevel()
    {
        if (currentMap != null)
        {
            Destroy(currentMap.gameObject);
        }
        currentLevel = currentLevel = PlayerPrefs.GetInt("Current Level");
        currentMap = Instantiate(maps[currentLevel], origin, true);
        PlayerManager.Instance.SetStartPoint(maps[currentLevel].StartPoint.position);
        
        PlayerManager.Instance.OnInit();

        Debug.Log("lOAD " + maps[currentLevel].StartPoint.transform.position);
        
    }

    public void LoadNextLevel()
    {
        currentLevel+=1;
        
        if (currentLevel > maps.Count-1)
        {
            currentLevel = 0;
            PlayerPrefs.SetInt("Current Level", currentLevel);
            LoadLevel();
            Debug.Log("Replayy");
        }
        else
        {
            PlayerPrefs.SetInt("Current Level", currentLevel);
            LoadLevel();
        }

    }

}
