using UnityEngine;
using System.Collections;

public class PlayerAmmo : MonoBehaviour
{

    private int pistolAmmo,
               shotgunAmmo,
               grenadeAmmo,
               machinegunAmmo;

    [SerializeField]
    private int startPistolAmmo,
                startShotgunAmmo,
                startGrenadeAmmo,
                startMachinegunAmmo;

    public string currentWeaponTag;

    // Use this for initialization
    void Start()
    {
        pistolAmmo = startPistolAmmo;
        shotgunAmmo = startShotgunAmmo;
        grenadeAmmo = startGrenadeAmmo;
        machinegunAmmo = startMachinegunAmmo;
    }
}
	



