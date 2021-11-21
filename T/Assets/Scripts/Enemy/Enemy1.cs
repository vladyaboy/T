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
        health = maxHealth;
    }

    protected override void Attack()
    {
        gunController.isFiring = true;
    }

    protected override void ResetAttack()
    {
        base.ResetAttack();
        gunController.isFiring = false;
    }

    public override void Dying()
    {
        base.Dying();
        // add some score to the player
    }
}
