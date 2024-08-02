using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuffle : MonoBehaviour
{
    public int[] numbers = { 1, 2, 3 };
    void Start()
    {
        
        

        // Output the random order
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shuf(numbers);
            Debug.Log($"Random order: {numbers[0]}, {numbers[1]}, {numbers[2]}");
        }
    }
    void Shuf(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int randomIndex = Random.Range(0, array.Length);
            int temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}
