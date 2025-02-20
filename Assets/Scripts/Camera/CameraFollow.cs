using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraFollow : MonoBehaviour
{
    public Transform player;       // ����ٴ� �÷��̾�
    public Tilemap tilemap;        // ��踦 ������ Ÿ�ϸ�
    public Camera cam;             // ����ٴϴ� ī�޶�

    private Vector3 minBounds;     // Ÿ�ϸ� �ּ� ��ǥ
    private Vector3 maxBounds;     // Ÿ�ϸ� �ִ� ��ǥ
    private float halfHeight;
    private float halfWidth;

    void Start()
    {
        if (!player || !tilemap || !cam)
        {
            Debug.LogError("�÷��̾�, Ÿ�ϸ�, ī�޶� �����ϼ���.");
            return;
        }

        BoundsInt bounds = tilemap.cellBounds;
        minBounds = tilemap.CellToWorld(bounds.min);
        maxBounds = tilemap.CellToWorld(bounds.max);

        // ī�޶� ũ�� ��� (Orthographic ����� ��)
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;
    }

    void LateUpdate()
    {
        Vector3 targetPosition = player.position;

        // ī�޶� Ÿ�ϸ� ��踦 ����� �ʵ��� ����
        float clampedX = Mathf.Clamp(targetPosition.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        float clampedY = Mathf.Clamp(targetPosition.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
