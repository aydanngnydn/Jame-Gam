using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShoot : MonoBehaviour
{
    [SerializeField] private GameObject[] bullets;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private string selectPlayer;
    [SerializeField] private float nextFire = 0.2F;
    private float myTime = 0.0F;
    private bool isShootAvailable, timePassed = false;
    private float fireDelta = 0.2F;
    [SerializeField] private BulletSpawner bulletSpawner;
    
    private void Update()
    {
        isShootAvailable = true;
        SpawnTripleBullet();
        if (timePassed)
        {
            bulletSpawner.enabled = true;
            this.enabled = false;
            sta
        }
    }

    void SpawnTripleBullet()
    {
        StartCoroutine(ChangeBool());
        
       if(!timePassed){
            myTime += Time.deltaTime;

            if ((Input.GetKeyDown(KeyCode.T) && selectPlayer == "Player1" ||
                 Input.GetKeyDown(KeyCode.Keypad1) && selectPlayer == "Player2") && myTime > nextFire)
            {
                nextFire = myTime + fireDelta;
                float rot = GetComponentInParent<Transform>().transform.rotation.y == 0 ? 0 : 150;

                bullets = new GameObject[3];


                for (int i = 0; i < bullets.Length; i++)
                {
                    Instantiate(bulletPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, rot)));
                    rot += 10;
                }

                nextFire = nextFire - myTime;

                myTime = 0f;
            }
        }
    }

    private IEnumerator ChangeBool()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            timePassed = true;
        }
    }
}
