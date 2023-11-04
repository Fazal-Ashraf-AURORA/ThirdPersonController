using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterAiming : MonoBehaviour {
    public float turnSpeed = 15;
    public float aimDuration = 0.3f;
    public float firerate = 0.0f;
    public int maxAmmo = 30;
    private int currentAmmo;
    private bool isReloading = false;

    Camera mainCamera;
    public Rig aimLayer;
    RaycastWeapon weapon;

    private bool isFiring = false; // Flag for auto-firing

    // Start is called before the first frame update
    void Start() {
        mainCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        weapon = GetComponentInChildren<RaycastWeapon>();
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void FixedUpdate() {
        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
    }

    private void LateUpdate() {
        if (isReloading)
            return;

        if (Input.GetMouseButton(1)) {
            aimLayer.weight += Time.deltaTime / aimDuration;
        } else {
            aimLayer.weight -= Time.deltaTime / aimDuration;
        }

        if (Input.GetButtonDown("Fire1") && aimLayer.weight ==1) {
            if (currentAmmo > 0) {
                isFiring = true;
                    StartCoroutine(AutoFire());
            } else {
                isFiring = false;
                StartCoroutine(Reload());
            }
        }

        if (Input.GetButtonUp("Fire1")) {
            isFiring = false;
            weapon.StopFiring();
        }
    }

    // Coroutine for auto-firing
    IEnumerator AutoFire() {
        while (isFiring) {
            if (currentAmmo > 0) {
                weapon.StartFiring();
                currentAmmo--;
            } else {
                isFiring = false; // Stop auto-firing when the magazine is empty
                StartCoroutine(Reload());
            }
            yield return new WaitForSeconds(firerate);
        }
    }

    IEnumerator Reload() {
        if (isReloading || currentAmmo == maxAmmo)
            yield break;
        
        isReloading = true;
        yield return new WaitForSeconds(2.0f);
        currentAmmo = maxAmmo;
        isReloading = false;
    }
}
