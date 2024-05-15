using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : Singleton<Map>
{
    [SerializeField] public Transform StartPoint;
    [SerializeField] GameObject MapPrefab;
}
