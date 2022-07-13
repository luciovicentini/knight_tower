using UnityEngine;

public static class UtilsClass {
    private static readonly Camera mainCamera = Camera.main;

    public static Vector3 GetTopRightWorldCameraPosition() {
        return mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.pixelWidth, mainCamera.pixelHeight, 0));
    }

    public static Vector3 GetBottomLeftWorldCameraPosition() {
        return mainCamera.ScreenToWorldPoint(Vector3.zero);
    }

    public static bool GetRandomBool() => Random.value >= .5f;
}