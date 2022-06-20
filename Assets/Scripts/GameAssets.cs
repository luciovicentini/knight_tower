using UnityEngine;

public class GameAssets : MonoBehaviour {
    private static GameAssets instance;

    public Transform pfEnemyTower;
    public Transform pfEnemyFloor;

    public static GameAssets Instance {
        get {
            if (instance == null) instance = Resources.Load<GameAssets>("GameAssets");
            return instance;
        }
    }
}