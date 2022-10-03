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
    [SerializeField] private DefaultShoot bulletSpawner;

    [SerializeField] private AudioClip shotClip;

    private float myTime = 0.0F;
    private float fireDelta = 0.2F;

    public event Action OnTripleFire;

    private void Update()
    {
        SpawnTripleBullet();
    }

    private void SpawnTripleBullet()
    {
        myTime += Time.deltaTime;

        if ((Input.GetKeyDown(KeyCode.T) && selectPlayer == "Player1" ||
             Input.GetKeyDown(KeyCode.Keypad1) && selectPlayer == "Player2") && myTime > nextFire)
        {
            nextFire = myTime + fireDelta;
            float rot = GetComponentInParent<Transform>().transform.rotation.y == 0 ? -15 : 165;

            bullets = new GameObject[3];


            for (int i = 0; i < bullets.Length; i++)
            {
                Instantiate(bulletPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, rot)));
                rot += 10;
            }

            nextFire = nextFire - myTime;

            myTime = 0f;

            HandleShootEvents();
        }
    }

    private void HandleShootEvents()
    {
        OnTripleFire?.Invoke();
        SoundManager.Instance.PlaySound(shotClip);
    }
}
