using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    protected Rigidbody2D m_rigidbody2D;

    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Transform weaponPivot;

    // 이동 방향
    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    // 바라보는 방향
    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    // 넉백 관련
    private Vector2 knockBack = Vector2.zero;
    private float knockbackDuration = 0f;

    // 애니메이션 관련
    protected AnimationHandler m_animationHandler;
    protected StatHandler m_statHandler;





    protected virtual void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_animationHandler = GetComponent<AnimationHandler>();
        m_statHandler = GetComponent<StatHandler>();
    }

    protected abstract void Start();

    protected virtual void Update()
    {
        // 입력, 이동에 관한 데이터 처리
        HandleAction();

        // 바라보는 방향에 따른 회전 처리.
        Rotate(lookDirection);
    }

    protected virtual void FixedUpdate()
    {
        // 움직임
        Movement(movementDirection);

        // 넉백 효과가 있다면, 적용 시간을 감소
        if(knockbackDuration > 0f)
        {
            knockbackDuration -= Time.deltaTime;
        }
    }

    protected abstract void HandleAction();

    private void Movement(Vector2 direction)
    {
        // 스탯의 speed를 적용
        direction = direction * m_statHandler.Speed;

        // 만약 넉백이 적용되어야한다면
        if(knockbackDuration > 0f)
        {
            // 기존 이동방향의 힘을 줄이고
            direction *= 0.2f;

            // 넉백을 적용시킨다.
            direction += knockBack;
        }

        // direction을 강체의 속도에 적용한다.
        m_rigidbody2D.velocity = direction;

        // 움직임에 따른 애니메이션 적용
        m_animationHandler.Move(direction);
    }

    // 컨트롤 되는 모든 객체를 특정 방향으로 회전시키는 함수
    // 방향에 따라 좌/우를 판별하고, 객체를 뒤집는다.
    // 만약 객체에 무기가 있다면, 해당 무기 방향을 인자로 받은 방향에 맞는 각도로 대입한다.
    private void Rotate(Vector2 direction)
    {
        // 인자로 받은 방향의 각도를 구한다. 각도는 radian으로 되어 있으므로 degree로 바꿔주기 위해 Rad2Deg를 곱해 각으로 바꿔준다.
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Atan2로 구한 각도는 1,4 사분면에서 -90 ~ 90의 값을 가진다.
        // 2,3 사분면에선 절댓값으로 따지면 90 ~ 180의 값을 가지므로 90f 이상이라면 왼쪽이라고 볼 수 있다.
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        // 기본 이미지로 사용하는 sprite는 오른쪽을 바라보고 있으므로, flipX에 true값을 넣으면 x축 기준으로 좌우로 뒤집는다.
        // y축을 기준으로 180도 회전하는 것과 같다. 
        characterRenderer.flipX = isLeft;

        // 무기가 존재한다면
        if(weaponPivot)
        {
            weaponPivot.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }
    }

    // 넉백 적용 함수
    // 인자로 받은 위치에서 해당 객체에게 넉백을 가한 것.
    // 방향은 상대로부터 나를 향한 것이고, 나를 기준으론 내가 가고자 하는 방향의 반대로 밀려나는 힘이다.
    public void ApplyKnockBack(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockBack = (transform.position - other.position).normalized * power;
    }
}
