using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    public bool isFiring = false;
    public ParticleSystem muzzleFlash;
    public ParticleSystem hitEffect;
    public TrailRenderer tracerEffect;
    public Transform raycastOrigin;
    public Transform raycastTarget;

    Ray ray;
    RaycastHit hitinfo;
    public void StartFiring() {
        isFiring = true;
        FireBullet();
    }

    private void FireBullet() {
        muzzleFlash.Emit(1);
        ray.origin = raycastOrigin.position;
        ray.direction = raycastTarget.position - raycastOrigin.position;

        var tracer = Instantiate(tracerEffect, ray.origin, Quaternion.identity);
        tracer.AddPosition(ray.origin);

        if (Physics.Raycast(ray, out hitinfo)) {
            //Debug.DrawLine(ray.origin, hitinfo.point, Color.red, 1.0f);
            hitEffect.transform.position = hitinfo.point;
            hitEffect.transform.forward = hitinfo.normal;
            hitEffect.Emit(1);

            tracer.transform.position = hitinfo.point;
        }
    }

    public void StopFiring() { 
        isFiring= false;
    }
}
