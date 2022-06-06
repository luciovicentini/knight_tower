using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour, IDrag
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDragEnd()
    {
        Debug.Log("Knight OnDragEnd");
    }

    public void OnDragStart()
    {
        Debug.Log("Knight OnDragStart");
    }
}
