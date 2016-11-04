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

        if (Input.GetButtonDown("Jump"))
        {
            player.GetComponent<Rigidbody>().AddForce(new Vector3(0, jump_Height, 0));
        }

        
    }

    private float getRotation(Vector2 playerPosition, Vector2 mousePosition)
    {
        return 0;
    }
}
