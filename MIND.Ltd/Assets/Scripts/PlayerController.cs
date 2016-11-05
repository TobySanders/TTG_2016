using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    enum Weapon
    {
        Pistol,
        Shotgun,
        Machinegun,
    }

    private Weapon currentWeapon;

    [SerializeField]
    GameObject torsoQuad;
    [SerializeField]
    Material pistolMaterial,
        shotgunMaterial,
        machinegunMaterial;

    [SerializeField]
    private Text healthText,
        selectedWeaponText,
        AmmoText;

    [SerializeField]
    private int startHealth,
        startShotgunAmmo,
        startMachinegunAmmo,
        startGrenadeAmmo,
        machinegunBurst;

    private int health,
        shotgunAmmo,
        machinegunAmmo,
        grenadeAmmo;

    
    [SerializeField]
    private float
        horizontal_Move_Speed,
        vertical_Move_Speed,
        jump_Height;
    GameObject player,
        player_Torso,
        player_Legs;
    [SerializeField]
    Camera camera;
    [SerializeField]
    GameObject bulletPrefab,
        shotgunPrefab,
        machinegunPrefab;

    private int jumpCount = 0;
	// Use this for initialization
	void Start () {
        player = transform.gameObject; //Get reference of the object using this controller
        player_Torso = player.transform.GetChild(0).gameObject;
        player_Legs = player.transform.GetChild(0).gameObject;

        health = startHealth;
        shotgunAmmo = startShotgunAmmo;
        machinegunAmmo = startMachinegunAmmo;
        grenadeAmmo = startGrenadeAmmo;
	}

    void NextWeapon(bool forward) //true if cycling forward
    {
        switch (currentWeapon)
        {
            case Weapon.Pistol:
                currentWeapon = forward ? Weapon.Shotgun : Weapon.Machinegun;
                break;
            case Weapon.Shotgun:
                currentWeapon = forward ? Weapon.Machinegun : Weapon.Pistol;
                break;
            case Weapon.Machinegun:
                currentWeapon = forward ? Weapon.Pistol : Weapon.Shotgun;
                break;
        }
        selectedWeaponText.text = currentWeapon.ToString();
        SetAmmo();
    }
    void SetAmmo()
    {
        switch (currentWeapon)
        {
            case Weapon.Pistol:
                AmmoText.text = "∞";
                torsoQuad.GetComponent<Renderer>().material = pistolMaterial;
                break;
            case Weapon.Shotgun:
                AmmoText.text = shotgunAmmo.ToString();
                torsoQuad.GetComponent<Renderer>().material = shotgunMaterial;
                break;
            case Weapon.Machinegun:
                AmmoText.text = machinegunAmmo.ToString();
                torsoQuad.GetComponent<Renderer>().material = machinegunMaterial;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            //TODO:Gameover
        }


        player.transform.position = new Vector3(player.transform.position.x + horizontal_Move_Speed * Input.GetAxis("Horizontal"),
            player.transform.position.y, player.transform.position.z);

        if(player.transform.position.z + vertical_Move_Speed*Input.GetAxis("Vertical") < 1.09 && player.transform.position.z + vertical_Move_Speed * Input.GetAxis("Vertical") > -5.89)
        player.transform.position = new Vector3(player.transform.position.x,
            player.transform.position.y, player.transform.position.z + vertical_Move_Speed*Input.GetAxis("Vertical"));

        //print(getRotation(player.transform.position, new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"))));
        player_Torso.transform.rotation = new Quaternion(player_Torso.transform.rotation.x,
            player_Torso.transform.rotation.y,
            player_Torso.transform.rotation.z + getRotation(player.transform.position, new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"))),
            player_Torso.transform.rotation.w);

        print(Input.mousePosition);

        var rotationVector = player_Torso.transform.rotation.eulerAngles;
        var cloneVector = rotationVector;
        rotationVector.z = getRotation(camera.WorldToScreenPoint(player_Torso.transform.position), Input.mousePosition);
        player_Torso.transform.rotation = Quaternion.Euler(rotationVector);

        if (Input.GetButtonDown("Jump"))
        {
            if (jumpCount < 2)
            {
                player.GetComponent<Rigidbody>().AddForce(new Vector3(0, jump_Height, 0));
                jumpCount++;
            }

        }

        if(Input.GetButtonDown("Change Weapon"))
        {
            NextWeapon(Input.GetAxis("Change Weapon") > 0);
        }

        if (Input.GetMouseButtonDown(0))
        {

            Vector3 gunHeight = player.transform.position;
            gunHeight.x += 0.7f;
            gunHeight.y += 0.45f;


            switch (currentWeapon)
            {
                case Weapon.Pistol:
                    var pistolVector = cloneVector;
                    GameObject pistolBullet = Instantiate(bulletPrefab, gunHeight, Quaternion.Euler(rotationVector)) as GameObject;
                    BulletBehaviour pb = pistolBullet.GetComponent<BulletBehaviour>();
                    pistolVector.z = getRotation(camera.WorldToScreenPoint(player_Torso.transform.position), Input.mousePosition);
                    pb.rotation = Quaternion.Euler(pistolVector);
                    break;

                case Weapon.Shotgun:
                    if (shotgunAmmo <= 0)
                        return;
                    var bulletVector = cloneVector;
                    var bullet1Vector = cloneVector;
                    var bullet2Vector = cloneVector;
                    var bullet3Vector = cloneVector;
                    var bullet4Vector = cloneVector;

                    GameObject bullet = Instantiate(shotgunPrefab, gunHeight, Quaternion.Euler(rotationVector)) as GameObject;
                    GameObject bullet1 = Instantiate(shotgunPrefab, gunHeight, Quaternion.Euler(rotationVector)) as GameObject;
                    GameObject bullet2 = Instantiate(shotgunPrefab, gunHeight, Quaternion.Euler(rotationVector)) as GameObject;
                    GameObject bullet3 = Instantiate(shotgunPrefab, gunHeight, Quaternion.Euler(rotationVector)) as GameObject;
                    GameObject bullet4 = Instantiate(shotgunPrefab, gunHeight, Quaternion.Euler(rotationVector)) as GameObject;

                    BulletBehaviour b = bullet.GetComponent<BulletBehaviour>();
                    BulletBehaviour b1 = bullet1.GetComponent<BulletBehaviour>();
                    BulletBehaviour b2 = bullet2.GetComponent<BulletBehaviour>();
                    BulletBehaviour b3 = bullet3.GetComponent<BulletBehaviour>();
                    BulletBehaviour b4 = bullet4.GetComponent<BulletBehaviour>();

                    bulletVector.z = getRotation(camera.WorldToScreenPoint(player_Torso.transform.position), Input.mousePosition);
                    bullet1Vector.z = getRotation(camera.WorldToScreenPoint(player_Torso.transform.position), Input.mousePosition) + 5;
                    bullet2Vector.z = getRotation(camera.WorldToScreenPoint(player_Torso.transform.position), Input.mousePosition) + 10;
                    bullet3Vector.z = getRotation(camera.WorldToScreenPoint(player_Torso.transform.position), Input.mousePosition) - 5;
                    bullet4Vector.z = getRotation(camera.WorldToScreenPoint(player_Torso.transform.position), Input.mousePosition) - 10;

                    b.rotation = Quaternion.Euler(bulletVector);
                    b1.rotation = Quaternion.Euler(bullet1Vector);
                    b2.rotation = Quaternion.Euler(bullet2Vector);
                    b3.rotation = Quaternion.Euler(bullet3Vector);
                    b4.rotation = Quaternion.Euler(bullet4Vector);
                    shotgunAmmo--;
                    AmmoText.text = shotgunAmmo.ToString();
                    break;

                case Weapon.Machinegun:
                    StartCoroutine(FireBullet());
                    break;
            }
           
        }
        
    }
    IEnumerator FireBullet()
    {
        for (int i = 0; i < machinegunBurst; i++)
        {
            if (machinegunAmmo == 0)
                break;
            var pistolVector = player_Torso.transform.rotation.eulerAngles;
            Vector3 gunHeight = player.transform.position;
            gunHeight.x += 0.7f;
            gunHeight.y += 0.45f;
            GameObject pistolBullet = Instantiate(machinegunPrefab, gunHeight, Quaternion.Euler(pistolVector)) as GameObject;
            BulletBehaviour pb = pistolBullet.GetComponent<BulletBehaviour>();
            pistolVector.z = getRotation(camera.WorldToScreenPoint(player_Torso.transform.position), Input.mousePosition);
            pb.rotation = Quaternion.Euler(pistolVector);
            yield return new WaitForSeconds(0.1f);
            machinegunAmmo--;
            AmmoText.text = machinegunAmmo.ToString();
        }
    }
    void OnCollisionEnter(Collision col)
    {
        System.Random random = new System.Random();

        switch (col.gameObject.tag)
        {
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
            case "Floor":
                jumpCount = 0;
                break;
            case "Enemy":
                health--;
                healthText.text = health.ToString();
                break;
            case "Boss":

            default:
                return;
        }
    }

    private float getRotation(Vector2 playerPosition, Vector2 mousePosition)
    {
        Vector2 playerToMouse = mousePosition - playerPosition;
        float angleRads = Mathf.Atan2(playerToMouse.y, playerToMouse.x);
        float angleDeg = angleRads * (180 / Mathf.PI);

        if (angleDeg >= 40)
            return 40;
        if (angleDeg <= -40)
            return -40;
        return angleDeg;
    }
}
