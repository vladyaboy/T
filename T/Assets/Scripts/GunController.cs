using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public bool isFiring = false;

    [SerializeField]
    BulletController bulletController;

    [SerializeField]
    float bulletSpeed;

    [SerializeField]
    int bulletDamage;

    [SerializeField]
    float timeBitweenShots;

    public float nextfire;

    public Transform firePoint;

    // Update is called once per frame
    void Update()
    {
        HandleFiring();
    }

    private void HandleFiring()
    {
        if (isFiring)
        {
            nextfire -= Time.deltaTime;

            if (nextfire <= 0)
            {
                nextfire = timeBitweenShots;
                FireShot();
            }
        }
        else if (nextfire > 0)
        {
            nextfire -= Time.deltaTime;
        }
    }

    private void FireShot()
    {
        BulletController newBullet = Instantiate(bulletController, firePoint.position, transform.rotation);
        newBullet.speedf = bulletSpeed;
        newBullet.damage = bulletDamage;
    }
}
