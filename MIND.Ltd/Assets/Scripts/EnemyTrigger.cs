using UnityEngine;
using System.Collections;

public class EnemyTrigger : MonoBehaviour {

    public GameObject player;
    public BoxCollider territory;
    private bool playerInTerritory;
    public int speed;
    public bool facingLeft;
    public int attackDistance;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInTerritory = false;
        facingLeft = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (playerInTerritory) {
            MoveToPlayer();
        } 
	}

    void MoveToPlayer() {
        float playerX = player.transform.position.x;
        if (playerX <= transform.position.x && !facingLeft) {
            facingLeft = !facingLeft;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        } else if (playerX > transform.position.x && facingLeft) {
            facingLeft = !facingLeft;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        if (Vector3.Distance(transform.position, player.transform.position) > attackDistance) {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject == player) {
            playerInTerritory = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject == player) {
            playerInTerritory = false;
        }
    }
}
