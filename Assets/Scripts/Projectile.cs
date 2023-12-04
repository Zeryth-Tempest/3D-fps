using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject ProjectileObject = null;
    public GameObject DetonateObject = null;

    public float DetonationLifetime = 1.0f;
    float DetonationTime = -1.0f;

    protected Vector3 SpawnPosition = new Vector3();
    protected Vector3 AimPosition = new Vector3();
    protected Vector3 AimDirection = new Vector3();

    public float MovementSpeed = 10.0f;

    public virtual void Init(Vector3 aSpawnPosition, Vector3 aAimPosition)
    {
        SpawnPosition = aSpawnPosition;
        AimPosition = aAimPosition;
        AimDirection = AimPosition - SpawnPosition;
        gameObject.transform.position = SpawnPosition;
        gameObject.transform.rotation.SetLookRotation(AimDirection, transform.up);
    }

    public virtual void Start()
    {
        ProjectileObject.SetActive(true);
        DetonateObject.SetActive(false);
    }

    public virtual void Update()
    {
        if(DetonationTime > 0)
        {
            DetonationTime -= Time.deltaTime;
            if(DetonationTime < 0 )
            {
                GameObject.Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            ProjectileObject.SetActive(false);
            DetonateObject.SetActive(true);
            DetonationTime = DetonationLifetime;
        }
    }
}
