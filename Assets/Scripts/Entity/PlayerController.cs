using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private Camera m_Camera;
    protected override void Start()
    {
        m_Camera = Camera.main;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void HandleAction()
    {
        // 1. [ 부드러운 값 반환 (Smooth Input) ]
        // Input.GetAxis는 0에서 입력 방향으로 부드럽게 변화하는 값을 반환
        // 예를 들어, 키를 누르면 즉시 1이 아니라 0 → 0.1 → 0.2 → … → 1처럼 천천히 증가하며
        // 반대로 키를 떼면 1 → 0.9 → 0.8 → ... → 0처럼 서서히 감소
        // 이 때문에 캐릭터가 부드럽게 움직이고, 조작이 자연스러움

        // 2. [ -1 ~1 사이의 값 반환 ]
        // Input.GetAxis("Horizontal")는 왼쪽(-1) ~오른쪽(1) 범위의 값을 반환
        // Input.GetAxis("Vertical")은 아래(-1) ~위(1) 범위의 값을 반환
        // 중립(키를 누르지 않음)일 때는 0

        // 3. [ Unity의 Input Manager 설정 사용 ]
        // Edit → Project Settings → Input Manager에서 직접 축을 설정
        // 기본적으로 Horizontal은 A / D, LeftArrow / RightArrow 키를 사용
        // Vertical은 W / S, UpArrow / DownArrow 키를 사용

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 입력값으로 방향 벡터를 구함
        movementDirection = new Vector2 (horizontal, vertical).normalized;

        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPosition = m_Camera.ScreenToWorldPoint(mousePosition);     // 카메라의 near에 위치한 평면에 있는 마우스 좌표를 월드 좌표로 변환
        lookDirection = worldPosition - (Vector2)transform.position;            // 카메라의 near 좌표에서 플레이어가 위치한 곳을 바라보는 방향을 구함.
                                                                                // 2d 게임에선 캐릭터도 near 평면에 위치함

        // 만약 마우스와 캐릭터의 위치 차이가 1칸 미만이라면
        if (lookDirection.magnitude < 0.9f)
        {
            // 방향 값을 주지 않는다.
            lookDirection = Vector2.zero;
        }
        // 마우스와 캐릭터의 위치 차이가 1칸 이상이라면
        else
        {
            // 해당 방향의 방향 벡터를 구한다.
            lookDirection = lookDirection.normalized;
        }

        isAttacking = Input.GetMouseButton(0);
    }


}
