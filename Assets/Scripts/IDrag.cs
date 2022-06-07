using UnityEngine;

public interface IDrag
{
    public void OnDragStart(Vector2 position);
    public void OnDragEnd(Vector2 position);
}