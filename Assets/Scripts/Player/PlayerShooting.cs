using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    private float fireCooldown = 0f;
    private bool isReloading = false;

    void Update()
    {
        var weapon = PlayerWeaponManager.Instance.currentWeaponSO;
        if (weapon == null) return;

        fireCooldown -= Time.deltaTime;

        // 장전 중이면 아무 것도 못함
        if (isReloading) return;

        // 발사
        if (Input.GetMouseButton(0) && fireCooldown <= 0f && weapon.currentAmmo > 0)
        {
            Shoot(weapon);
            fireCooldown = 1f / weapon.fireRate;
        }

        GameManager.Instance?.UpdateAmmoUI(weapon);

        // 장전 키 입력 (R)
        if (Input.GetKeyDown(KeyCode.R) && weapon.currentAmmo < weapon.maxAmmo)
        {
            StartCoroutine(Reload(weapon));
        }
    }

    void Shoot(WeaponSO weapon)
    {
        weapon.currentAmmo--;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
            bulletScript.damage = weapon.damage;

        Debug.Log($"{weapon.weaponName} 발사! 남은 탄약: {weapon.currentAmmo}");
    }

    System.Collections.IEnumerator Reload(WeaponSO weapon)
    {
        isReloading = true;
        GameManager.Instance?.SetReloadingUI(true);

        Debug.Log($" {weapon.weaponName} 장전 중...");

        yield return new WaitForSeconds(weapon.reloadTime);

        weapon.currentAmmo = weapon.maxAmmo;
        isReloading = false;

        Debug.Log($"{weapon.weaponName} 장전 완료! 탄약: {weapon.currentAmmo}");

        GameManager.Instance?.SetReloadingUI(false);

        GameManager.Instance?.UpdateAmmoUI(weapon);
    }
}
