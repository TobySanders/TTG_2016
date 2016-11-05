using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;

    private const float speed = 3.0f;

    // Use this for initialization
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        float interpolation = Time.deltaTime * speed;

        Vector3 position = this.transform.position;
        //position.y = Mathf.Lerp(this.transform.position.y, player.transform.position.y, interpolation);
        position.x = Mathf.Lerp(this.transform.position.x, player.transform.position.x, interpolation);

        this.transform.position = position;
    }
}
