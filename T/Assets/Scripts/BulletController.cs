using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speedf;
    public int damage;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speedf * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy1>().TakeDamage(damage);
        }

        if((!this.CompareTag("Player Bullet") && collision.gameObject.CompareTag("Player")))
        {
            GameObject.Find("Player").GetComponent<PlayerController>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
