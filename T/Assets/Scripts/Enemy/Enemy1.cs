using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : BaseEnemy
{
    [SerializeField]
    GunController gunController;

    private void Awake()
    {
        base.Awake();
    }
    protected override void Attack()
    {
        gunController.isFiring = true;
        base.Attack();
    }

    protected override void ResetAttack()
    {
        base.ResetAttack();
        gunController.isFiring = false;
    }
}
