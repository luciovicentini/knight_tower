using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour, IDrag
{
    [SerializeField]
    private float mouseDragSpeed;

    private Vector2 speed = Vector2.zero;
    private Vector2 mousePosition;
    private bool IsDragging = false;

    private void Awake()
    {
        mousePosition = transform.position;
    }

    private void Update()
    {
        if (IsDragging)
        {
            transform.position = mousePosition;
        }
    }

    public void OnDragEnd(Vector2 position)
    {
        Debug.Log("Knight OnDragEnd in " + position.ToString());
        if (IsDragging) MoveToFloor(position);
        IsDragging = false;
    }

    public void OnDragStart(Vector2 position)
    {
        Debug.Log("Knight OnDragStart in " + position.ToString());
        mousePosition = position;
        IsDragging = true;
    }

    public void MoveToFloor(Vector2 position)
    {

    }
}
