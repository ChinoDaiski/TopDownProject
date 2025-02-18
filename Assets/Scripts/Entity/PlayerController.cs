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
        // 1. [ �ε巯�� �� ��ȯ (Smooth Input) ]
        // Input.GetAxis�� 0���� �Է� �������� �ε巴�� ��ȭ�ϴ� ���� ��ȯ
        // ���� ���, Ű�� ������ ��� 1�� �ƴ϶� 0 �� 0.1 �� 0.2 �� �� �� 1ó�� õõ�� �����ϸ�
        // �ݴ�� Ű�� ���� 1 �� 0.9 �� 0.8 �� ... �� 0ó�� ������ ����
        // �� ������ ĳ���Ͱ� �ε巴�� �����̰�, ������ �ڿ�������

        // 2. [ -1 ~1 ������ �� ��ȯ ]
        // Input.GetAxis("Horizontal")�� ����(-1) ~������(1) ������ ���� ��ȯ
        // Input.GetAxis("Vertical")�� �Ʒ�(-1) ~��(1) ������ ���� ��ȯ
        // �߸�(Ű�� ������ ����)�� ���� 0

        // 3. [ Unity�� Input Manager ���� ��� ]
        // Edit �� Project Settings �� Input Manager���� ���� ���� ����
        // �⺻������ Horizontal�� A / D, LeftArrow / RightArrow Ű�� ���
        // Vertical�� W / S, UpArrow / DownArrow Ű�� ���

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // �Է°����� ���� ���͸� ����
        movementDirection = new Vector2 (horizontal, vertical).normalized;

        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPosition = m_Camera.ScreenToWorldPoint(mousePosition);     // ī�޶��� near�� ��ġ�� ��鿡 �ִ� ���콺 ��ǥ�� ���� ��ǥ�� ��ȯ
        lookDirection = worldPosition - (Vector2)transform.position;            // ī�޶��� near ��ǥ���� �÷��̾ ��ġ�� ���� �ٶ󺸴� ������ ����.
                                                                                // 2d ���ӿ��� ĳ���͵� near ��鿡 ��ġ��

        // ���� ���콺�� ĳ������ ��ġ ���̰� 1ĭ �̸��̶��
        if (lookDirection.magnitude < 0.9f)
        {
            // ���� ���� ���� �ʴ´�.
            lookDirection = Vector2.zero;
        }
        // ���콺�� ĳ������ ��ġ ���̰� 1ĭ �̻��̶��
        else
        {
            // �ش� ������ ���� ���͸� ���Ѵ�.
            lookDirection = lookDirection.normalized;
        }

        isAttacking = Input.GetMouseButton(0);
    }


}
