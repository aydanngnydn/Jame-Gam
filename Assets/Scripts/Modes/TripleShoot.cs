using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShoot : MonoBehaviour
{
    [SerializeField] private GameObject[] bullets;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private string selectPlayer;
    [SerializeField] private float nextFire = 0.2F;
    [SerializeField] private BulletSpawner bulletSpawner;
    private float myTime = 0.0F;
    private bool isShootAvailable, timePassed = false;
    private float fireDelta = 0.2F;
    [SerializeField] UpgradeManager upgradeManager;

  
    private void OnEnable()
    {
        upgradeManager.OnTripleShootUpgrade += ChangeTripleMode;
    }

    private void OnDisable()
    {
        upgradeManager.OnTripleShootUpgrade -= ChangeTripleMode;
    }

    

    private void Update()
    {
        if (!timePassed)
        {
            bulletSpawner.enabled = false;
            SpawnTripleBullet();
        }
        else if (timePassed)
        {
            bulletSpawner.enabled = true;
            this.enabled = false;
        }
    }

    void SpawnTripleBullet()
    {
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

    void ChangeTripleMode()
    {
        StartCoroutine(TimeCount());
    }

    private IEnumerator TimeCount()
    {
        while (true)
        {
            timePassed = !timePassed;
            yield return new WaitForSeconds(2f);
            timePassed = !timePassed;
        }
    }
}
