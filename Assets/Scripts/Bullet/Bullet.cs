using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int bulletDamage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private float destroyTime;

    private void FixedUpdate()
    {
        MoveBullet();
    }

    protected void MoveBullet()
    {
        transform.Translate(new Vector2(bulletSpeed * Time.fixedDeltaTime, 0));
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject gameObject = other.gameObject;
        Vector2 contactPoint = other.GetContact(0).point;

        if (gameObject != null)
        {
            if (gameObject.TryGetComponent(out PlayerHealth player))
            {
                player.DecraseHealth(bulletDamage);

                DestroyBullet();

                //player.CreateBulletDestroyEffect(contactPoint);

            }
        }
        else
        {
            Debug.Log("There is no gameObject this" + name + "collides.");
        }
    }
}