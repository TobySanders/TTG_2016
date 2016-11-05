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
            if (player.transform.position.y <= 0)
                jumpCount = 0;
            if (jumpCount < 2)
            {
                player.GetComponent<Rigidbody>().AddForce(new Vector3(0, jump_Height, 0));
                jumpCount++;
            }

        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(bulletPrefab, player.transform.position, Quaternion.Euler(rotationVector)) as GameObject;
            BulletBehaviour b = bullet.GetComponent<BulletBehaviour>();
            cloneVector.z =  getRotation(camera.WorldToScreenPoint(player_Torso.transform.position), Input.mousePosition);
            b.rotation =  Quaternion.Euler(cloneVector);
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
