using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CloudsManager : MonoBehaviour {
    [SerializeField] private Sprite[] cloudsSprites;
    [SerializeField] private Transform terrainBackground;
    
    private float offScreenMargin = 4f;

    private float spawnTime;
    private float maxTime = 6f;
    private float minTime = 3f;
    private float elapsedTime;
    private Sprite sprite;

    private float minCloudSpeed = 1f;
    private float maxCloudSpeed = 4f;

    private void Start() {
        CreateCloud();
    }

    private void Update() {
        elapsedTime += Time.deltaTime;
        
        if (elapsedTime >= spawnTime) {
            elapsedTime = 0;
            CreateCloud();
        }
    }

    private void CreateCloud() {
        SetTimer();
        GameObject cloud = SetCloud();
        Direction direction = GetRandomDirection();
        cloud.transform.position = new Vector3 (GetStartingPositionX(direction), GetRandomYPosition(), 0f);
        cloud.SetActive(true);
        if (direction == Direction.LeftToRight) {
            StartCoroutine(MoveCloudLeft2Right(cloud.transform, GetRandomSpeed(), direction));    
        }
        else {
            StartCoroutine(MoveCloudRight2Left(cloud.transform, GetRandomSpeed(), direction));
        }
    }

    private GameObject SetCloud() {
        GameObject cloud = new GameObject("Cloud");
        cloud.SetActive(false);
        sprite = cloudsSprites[Random.Range(0, cloudsSprites.Length - 1)];
        cloud.AddComponent<SpriteRenderer>().sprite = this.sprite;
        cloud.GetComponent<SpriteRenderer>().sortingLayerName = "Background";
        cloud.transform.SetParent(gameObject.transform);
        return cloud;
    }

    private void SetTimer() {
        spawnTime = Random.Range(minTime, maxTime);
        elapsedTime = 0f;
    }

    private IEnumerator MoveCloudLeft2Right(Transform cloud, float moveSpeed, Direction direction) {
        while (cloud.position.x < GetFinalPositionX(direction)) {
            cloud.position += (Vector3) GetMovingDirection(direction) * (moveSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        Destroy(cloud.gameObject);
    }

    private IEnumerator MoveCloudRight2Left(Transform cloud, float moveSpeed, Direction direction) {
        while (cloud.position.x > GetFinalPositionX(direction)) {
            cloud.position += (Vector3) GetMovingDirection(direction) * (moveSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        Destroy(cloud.gameObject);
    }
    
    private float GetRandomYPosition() {
        float minRange = GetMinYPosition();
        float maxRange = UtilsClass.GetTopRightWorldCameraPosition().y;
        float margin = sprite.bounds.size.y;
        return Random.Range(minRange + margin, maxRange - margin);
    }

    private float GetMinYPosition() {
        float backgroundTopY = terrainBackground.position.y + (terrainBackground.localScale.y / 2);
        float bottomCameraPosition = UtilsClass.GetBottomLeftWorldCameraPosition().y;
        if (bottomCameraPosition > backgroundTopY) {
            return bottomCameraPosition;
        }
        return backgroundTopY;
    }

    private float GetRandomSpeed() => Random.Range(minCloudSpeed, maxCloudSpeed);
    
    private Direction GetRandomDirection() => UtilsClass.GetRandomBool() ? Direction.LeftToRight : Direction.RightToLeft;

    private float GetStartingPositionX(Direction direction) =>
        direction == Direction.LeftToRight ? UtilsClass.GetBottomLeftWorldCameraPosition() . x - offScreenMargin : 
            UtilsClass.GetTopRightWorldCameraPosition(). x + offScreenMargin;

    private float GetFinalPositionX(Direction direction) =>
        direction == Direction.LeftToRight ? UtilsClass.GetTopRightWorldCameraPosition(). x + offScreenMargin : UtilsClass.GetBottomLeftWorldCameraPosition() . x - offScreenMargin;

    private Vector2 GetMovingDirection(Direction direction) =>
        direction == Direction.LeftToRight ? Vector2.right : Vector2.left;
    
    private enum Direction {
        RightToLeft,
        LeftToRight,
    }
}
