using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : Singleton<Map>
{
    [SerializeField] int mapId;
    [SerializeField] public Transform StartPoint;
    [SerializeField] public Transform FinishPoint;
    [SerializeField] GameObject MapPrefab;


}
