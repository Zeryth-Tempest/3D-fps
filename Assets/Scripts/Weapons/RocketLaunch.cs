using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLaunch : MonoBehaviour
{
    public float fireRate = 15f;
    public float launchForce = 15f;

    public int maxAmmo = 1;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isRealoding = false;

    public Camera fpsCam;

    private float nextTimeToFire = 0f;

    public bool readyToShoot;

    public GameObject missileprefab;
    public Transform muzzlePoint;

    public void Start()
    {
        currentAmmo = maxAmmo;
    }

    public void Update()
    {
        if (isRealoding)
        {
            return;
        }

        if(currentAmmo <= 0f)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time * 1f / fireRate;
            Shoot();
        }
    }

    IEnumerator Reload()
    {
        isRealoding = true;
        Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isRealoding = false;
    }

    public void Shoot()
    {
        currentAmmo--;

        GameObject missile = Instantiate(missileprefab, muzzlePoint.transform.position, muzzlePoint.transform.rotation);
        Rigidbody rb = missile.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * launchForce * Time.deltaTime);
    }
}