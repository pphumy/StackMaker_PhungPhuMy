using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : Singleton<Map>
{
    [SerializeField] int mapId;
    [SerializeField] public Transform StartPoint;
    [SerializeField] public Transform FinishPoint;
    [SerializeField] GameObject MapPrefab;
    private void Update()
    {
        
        float dist = Vector3.Distance(PlayerManager.Instance.transform.position, FinishPoint.transform.position);
        if (dist < 1f)
        {
            Debug.Log("Win");
            UIManager.Instance.ShowWinUi();
            PlayerManager.Instance.ChangeAnim("Win");
        }
    }


}
