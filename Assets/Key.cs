using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public void FollowPlayer(Vector3 target)
    {
        transform.position = Vector3.Lerp(transform.position, Vector3.MoveTowards(transform.position, target, 1), 0.1f);
        print("follow");
    }
}
