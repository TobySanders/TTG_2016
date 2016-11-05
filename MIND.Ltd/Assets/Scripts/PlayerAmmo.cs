using UnityEngine;
using System.Collections;

public class PlayerAmmo : MonoBehaviour {

    public int pistolAmmo,
               shotgunAmmo,
               grenadeAmmo,
               machinegunAmmo;

    public int startPistolAmmo,
                startShotgunAmmo,
                startGrenadeAmmo,
                startMachinegunAmmo;

    public string currentWeaponTag;

    // Use this for initialization
    void Start () {
        pistolAmmo = startPistolAmmo;
        shotgunAmmo = startShotgunAmmo;
        grenadeAmmo = startGrenadeAmmo;
        machinegunAmmo = startMachinegunAmmo;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col) {
        System.Random random = new System.Random();
        switch (col.gameObject.tag) {
            case "pistolAmmo":
                pistolAmmo += random.Next(5, 20);
                col.gameObject.SetActive(false);
                break;
            case "shotgunAmmo":
                shotgunAmmo += random.Next(5, 15);
                col.gameObject.SetActive(false);
                break;
            case "grenadeAmmo":
                grenadeAmmo += random.Next(1, 5);
                col.gameObject.SetActive(false);
                break;
            case "machinegunAmmo":
                machinegunAmmo += random.Next(5, 15);
                col.gameObject.SetActive(false);
                break;
            default:
                return;
        }
    }
}
