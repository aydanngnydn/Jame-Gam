using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    void Start()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
}