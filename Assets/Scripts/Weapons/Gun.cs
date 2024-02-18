using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.UI;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;

    public int maxAmmo = 32;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isRealoding = false;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;

    private float nextTimeToFire = 0f;

    public void Start()
    {
        currentAmmo = maxAmmo;
    }

    void OnEnable()
    {
        isRealoding = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRealoding)
        {
            return;
        }

        if (currentAmmo <= 0f)
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
        muzzleFlash.Play();

        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }
    }
}
