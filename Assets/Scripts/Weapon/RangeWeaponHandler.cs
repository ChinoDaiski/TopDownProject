using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeaponHandler : WeaponHandler
{
    [Header("Ranged Attack Data")]
    [SerializeField] private Transform projectTileSpawnPosition;

    [SerializeField] private int bulletIndex;
    public int BulletIndex { get { return bulletIndex; } }

    [SerializeField] private float bulletSize = 1f;
    public float BulletSize{ get { return bulletSize; } }

    [SerializeField] private float duration;
    public float Duration { get { return duration; } }

    [SerializeField] private float spread;  // 탄 퍼짐의 정도
    public float Spread { get { return spread; } }

    [SerializeField] private int numberOfProjectilePerShot; // 발사하는 것 당 몇발을 쏘는지 수를 결정
    public int NumberOfProjectilePerShot { get { return numberOfProjectilePerShot; } }

    [SerializeField] private float multipleProjectileAngle; // 발사체들의 분산 각도
    public float MultipleProjectileAngle { get { return multipleProjectileAngle; } }

    [SerializeField] private Color projectileColor; // 발사체의 색상
    public Color ProjectileColor { get { return projectileColor; } }

    public override void Attack()
    {
        base.Attack();

        float _projectileAngleSpace = multipleProjectileAngle;
        int _numberOfProjectilePerShot = numberOfProjectilePerShot;

        // 다중 발사체 발사시, 발사해야하는 최소 각도 계산
        float minAngle = -(_numberOfProjectilePerShot / 2f) * _projectileAngleSpace;

        for (int i = 0; i < numberOfProjectilePerShot; i++)
        {
            float angle = minAngle + _projectileAngleSpace * i;
            float randomSpread = Random.Range(-spread, spread);
            angle += randomSpread;
            CreateProjectile(m_controller.LookDirection, angle);
        }
    }

    private void CreateProjectile(Vector2 _lookDirection, float angle)
    {

    }

    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }
}
