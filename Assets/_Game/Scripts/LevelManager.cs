using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] Transform origin;
    [SerializeField] List<Map> maps = new List<Map>();

    public void LoadLevel()
    {

        Instantiate(maps[0], origin,true);
        PlayerManager.Instance.SetStartPoint(maps[0].StartPoint.position);
    }

}
