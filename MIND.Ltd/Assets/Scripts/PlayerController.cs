using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour {

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
    GameObject bulletPrefab;

    private int jumpCount = 0;
	// Use this for initialization
	void Start () {
        player = transform.gameObject; //Get reference of the object using this controller
        player_Torso = player.transform.GetChild(0).gameObject;
        player_Legs = player.transform.GetChild(0).gameObject;
	}

    // Update is called once per frame
    void Update()
    {
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

        if (Input.GetMouseButtonDown(0))
        {

            Vector3 gunHeight = player.transform.position;
            gunHeight.x += 0.7f;
            gunHeight.y += 0.45f;

            var bulletVector = cloneVector;
            var bullet1Vector = cloneVector;
            var bullet2Vector = cloneVector;
            var bullet3Vector = cloneVector;
            var bullet4Vector = cloneVector;

            GameObject bullet = Instantiate(bulletPrefab, gunHeight, Quaternion.Euler(rotationVector)) as GameObject;
            GameObject bullet1 = Instantiate(bulletPrefab, gunHeight, Quaternion.Euler(rotationVector)) as GameObject;
            GameObject bullet2 = Instantiate(bulletPrefab, gunHeight, Quaternion.Euler(rotationVector)) as GameObject;
            GameObject bullet3 = Instantiate(bulletPrefab, gunHeight, Quaternion.Euler(rotationVector)) as GameObject;
            GameObject bullet4 = Instantiate(bulletPrefab, gunHeight, Quaternion.Euler(rotationVector)) as GameObject;

            BulletBehaviour b = bullet.GetComponent<BulletBehaviour>();
            BulletBehaviour b1 = bullet1.GetComponent<BulletBehaviour>();
            BulletBehaviour b2 = bullet2.GetComponent<BulletBehaviour>();
            BulletBehaviour b3 = bullet3.GetComponent<BulletBehaviour>();
            BulletBehaviour b4 = bullet4.GetComponent<BulletBehaviour>();

            bulletVector.z =  getRotation(camera.WorldToScreenPoint(player_Torso.transform.position), Input.mousePosition);
            bullet1Vector.z = getRotation(camera.WorldToScreenPoint(player_Torso.transform.position), Input.mousePosition) + 5;
            bullet2Vector.z = getRotation(camera.WorldToScreenPoint(player_Torso.transform.position), Input.mousePosition) + 10;
            bullet3Vector.z = getRotation(camera.WorldToScreenPoint(player_Torso.transform.position), Input.mousePosition) - 5;
            bullet4Vector.z = getRotation(camera.WorldToScreenPoint(player_Torso.transform.position), Input.mousePosition) - 10;

            b.rotation =  Quaternion.Euler(bulletVector);
            b1.rotation = Quaternion.Euler(bullet1Vector);
            b2.rotation = Quaternion.Euler(bullet2Vector);
            b3.rotation = Quaternion.Euler(bullet3Vector);
            b4.rotation = Quaternion.Euler(bullet4Vector);
        }
        
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Floor")
        {
            jumpCount = 0;
        }
    }

    private float getRotation(Vector2 playerPosition, Vector2 mousePosition)
    {
        Vector2 playerToMouse = mousePosition - playerPosition;
        float angleRads = Mathf.Atan2(playerToMouse.y, playerToMouse.x);
        float angleDeg = angleRads * (180 / Mathf.PI);

        if (angleDeg >= 60)
            return 60;
        if (angleDeg <= -40)
            return -40;
        return angleDeg;
    }
}
