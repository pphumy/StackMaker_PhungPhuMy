using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disablebrick : MonoBehaviour
{
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        if (!PlayerManager.Instance.dir.Equals(Vector3.zero))
        {
            PlayerManager.Instance.AddBrick();
            this.gameObject.SetActive(false);
        }
    }
}
