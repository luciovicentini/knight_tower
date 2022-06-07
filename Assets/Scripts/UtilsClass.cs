using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsClass
{
    private static Camera mainCamera = Camera.main;

    public static Vector3 GetScreenWorldPosition()
    {
        return mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.pixelWidth, mainCamera.pixelHeight, 0));
    }
}
