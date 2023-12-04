using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Projectile
{
    public virtual void Init(Vector3 aSpawnPosition, Vector3 aAimPosition)
    {
        base.Init(aSpawnPosition, aAimPosition);
    }

    public override void Update()
    {
        transform.position += AimDirection.normalized * MovementSpeed * Time.deltaTime;
        base.Update();
    }
}
