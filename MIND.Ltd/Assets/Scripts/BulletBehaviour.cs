using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {

    [SerializeField]
    private float velocity,
        distanceTravelled = 0,
        maxDistance;
    public Quaternion rotation;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().velocity = (rotation * (Vector3.right * velocity));
        transform.rotation = Camera.main.transform.rotation;

    }

	// Update is called once per frame
	void Update () {
        distanceTravelled += velocity;
        if (distanceTravelled > maxDistance)
            Destroy(gameObject);
	}
}
