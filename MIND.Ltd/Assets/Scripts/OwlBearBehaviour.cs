using UnityEngine;
using System.Collections;

public class OwlBearBehaviour : MonoBehaviour {

    enum BearState
    {
        Tracking,
        Charging,
        Waiting,
        Cooldown
    }


    [SerializeField]
    GameObject player;
    [SerializeField]
    private float trackingTime,
        cooldownTime,
        chargeDelay,
        chargeDistance,
        moveSpeed;

    private float timer,
        distanceCharged;
    private bool bearIsOnRight = true;
    private BearState bearState;

	// Use this for initialization
	void Start () {
        bearState = BearState.Tracking;
        timer = trackingTime;
	}

    // Update is called once per frame
    void Update()
    {

        switch (bearState)
        {
            case BearState.Tracking:
                float z = player.transform.position.z - transform.position.z > 0 ? moveSpeed : -moveSpeed;
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + z);
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    bearState = BearState.Waiting;
                    timer = chargeDelay;
                }
                break;
            case BearState.Waiting:
                timer -= Time.deltaTime;
                if (timer <= 0)
                    bearState = BearState.Charging;
                break;
            case BearState.Charging:
                float x = bearIsOnRight ? -moveSpeed * 3: moveSpeed * 3;

                transform.position = new Vector3(transform.position.x + x, transform.position.y, transform.position.z);
                distanceCharged += moveSpeed * 3;
                if(distanceCharged >= chargeDistance)
                {
                    bearState = BearState.Cooldown;
                    bearIsOnRight = !bearIsOnRight;
                    timer = cooldownTime;
                    distanceCharged = 0;
                }
                break;
            case BearState.Cooldown:
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    bearState = BearState.Tracking;
                    timer = trackingTime;
                }                   
                break;
        }
    }

}
