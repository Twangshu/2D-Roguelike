using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemsTest : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            Knapsack.Instance.GetItem(Random.Range(1, 10));
    }
}
