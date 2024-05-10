using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] GameObject yellow;
    public bool havePassed = false;
    public void ChangeColor()
    {
        var cubeRenderer = yellow.GetComponent<Renderer>();
        cubeRenderer.material.color = Color.yellow;
    }

}
