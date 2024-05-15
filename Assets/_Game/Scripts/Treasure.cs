using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    [SerializeField] GameObject treasureClose;
    [SerializeField] GameObject treasureOpen;
    [SerializeField] ParticleSystem[] particles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            treasureClose.SetActive(false);
            treasureOpen.SetActive(true);
            Debug.Log("Open");
        }
        foreach(var i in particles)
        {
            i.Play();
        }
        
    }
}
