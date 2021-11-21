using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speedf;
    public int damage;
    float xBoundaries = 30f;
    float zBoundaries = 15f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speedf * Time.deltaTime);

        if (transform.position.x > xBoundaries || transform.position.x < -xBoundaries) Destroy(gameObject);
        if (transform.position.z > zBoundaries || transform.position.z < -zBoundaries) Destroy(gameObject);
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
