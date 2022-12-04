using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.dataPath);
        Debug.Log(Directory.GetCurrentDirectory());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
