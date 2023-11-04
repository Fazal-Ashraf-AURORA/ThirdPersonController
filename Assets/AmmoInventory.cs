using UnityEngine;

public class AmmoInventory : MonoBehaviour {
    public int maxAmmo = 200;
    private int magazineAmmo = 0;
    private int totalAmmo = 0;

    public int MagazineAmmo => magazineAmmo;
    public int TotalAmmo => totalAmmo;

    public void AddAmmo(int amount) {
        totalAmmo = Mathf.Min(totalAmmo + amount, maxAmmo);
    }

    public bool ConsumeMagazineAmmo(int amount) {
        if (magazineAmmo >= amount) {
            magazineAmmo -= amount;
            return true;
        }
        return false;
    }

    public bool ConsumeInventoryAmmo(int amount) {
        if (totalAmmo >= amount) {
            totalAmmo -= amount;
            return true;
        }
        return false;
    }

    public void FillMagazine() {
        int ammoNeeded = maxAmmo - magazineAmmo;
        if (ammoNeeded > 0) {
            if (ConsumeInventoryAmmo(ammoNeeded)) {
                AddMagazineAmmo(ammoNeeded); // Call a new method to add ammo to the magazine
            }
        }
    }

    public void AddMagazineAmmo(int amount) {
        magazineAmmo += amount;
    }
}
