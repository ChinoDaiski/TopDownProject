using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraFollow : MonoBehaviour
{
    public Transform player;       // 따라다닐 플레이어
    public Tilemap tilemap;        // 경계를 가져올 타일맵
    public Camera cam;             // 따라다니는 카메라

    private Vector3 minBounds;     // 타일맵 최소 좌표
    private Vector3 maxBounds;     // 타일맵 최대 좌표
    private float halfHeight;
    private float halfWidth;

    void Start()
    {
        if (!player || !tilemap || !cam)
        {
            Debug.LogError("플레이어, 타일맵, 카메라를 연결하세요.");
            return;
        }

        BoundsInt bounds = tilemap.cellBounds;
        minBounds = tilemap.CellToWorld(bounds.min);
        maxBounds = tilemap.CellToWorld(bounds.max);

        // 카메라 크기 계산 (Orthographic 모드일 때)
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;
    }

    void LateUpdate()
    {
        Vector3 targetPosition = player.position;

        // 카메라가 타일맵 경계를 벗어나지 않도록 제한
        float clampedX = Mathf.Clamp(targetPosition.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        float clampedY = Mathf.Clamp(targetPosition.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
