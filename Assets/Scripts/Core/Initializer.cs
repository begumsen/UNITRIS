using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        EventManager.Initialize();
        GameFlowManager.Initialize();
    }
}
