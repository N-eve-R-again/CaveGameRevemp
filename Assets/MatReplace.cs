using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatReplace : MonoBehaviour
{
    public MeshRenderer render;
    public Material MatRuntime;
    // Start is called before the first frame update
    void Start()
    {
        render.material = MatRuntime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
