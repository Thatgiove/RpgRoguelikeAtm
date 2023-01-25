using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform player;
    void Start()
    {

    }

    void LateUpdate()
    {
        transform.position = new Vector3(
            player.position.x,
            player.position.y,
            transform.position.z
            );
    }
}
