using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponDisplay : MonoBehaviour
{
    public WeaponData weapondata;

    public MeshRenderer weaponRenderer;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI discriptionText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupWeapon(WeaponData data)
    {
        weapondata = data;
        if (nameText != null) nameText.text = data.weaponName;
        if (levelText != null) levelText.text = data.level.ToString();
        if(attackText != null) attackText.text = data.damage.ToString();
        if (ammoText != null) ammoText.text = data.currentAmmo.ToString();
        if (discriptionText != null) discriptionText.text = data.description;

        if (weaponRenderer != null && data.icon != null)
        {
            Material weaponMaterial = weaponRenderer.material;
            weaponMaterial.mainTexture = data.icon.texture;
        }
    }
}
