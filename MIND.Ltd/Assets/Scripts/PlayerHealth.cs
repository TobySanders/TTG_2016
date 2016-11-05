using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    public int startHealth = 100;
    public int currentHealth;

    // Use this for initialization
    void Start() {
        currentHealth = startHealth;
    }

    // Update is called once per frame
    void Update() {

    }

    public void TakeDamage(int amount) {
        currentHealth -= amount;
    }

    private void Die() {
        this.gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "health") {
            currentHealth += 10;
            col.gameObject.SetActive(false);
        }
    }
}
