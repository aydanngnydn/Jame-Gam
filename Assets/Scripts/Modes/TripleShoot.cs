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
    private bool isShootAvailable;
    private float fireDelta = 0.2F;
    
    private void Update()
    {
        isShootAvailable = true;
        SpawnTripleBullet();
    }

    void SpawnTripleBullet()
    {
        myTime += Time.deltaTime;

        if ((Input.GetKeyDown(KeyCode.T) && selectPlayer == "Player1" || Input.GetKeyDown(KeyCode.Keypad1) && selectPlayer == "Player2") && myTime > nextFire)
        {
            Debug.Log("triple");
            nextFire = myTime + fireDelta;
            int x = -15;
            bullets = new GameObject[3];
           
            for (int i = 0; i < bullets.Length; i++)
            {
                Instantiate(bulletPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, x)));
                x += 15;
            }
            
            nextFire = nextFire - myTime;

            myTime = 0f;
        }
    }
}
