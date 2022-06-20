using UnityEngine;

public interface IDrag {
    public void OnDragStart(Vector2 position);
    public void OnDragging(Vector2 position);
    public void OnDragEnd();
}