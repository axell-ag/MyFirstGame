using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool IsOpen = false;
    public Transform TeleportDoor;
    public Sprite Mid, Top;

    public void Unlock()
    {
        IsOpen = true;
        GetComponent<SpriteRenderer>().sprite = Mid;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Top;
    }

    public void Teleport(GameObject player)
    {
        player.transform.position = new Vector3(TeleportDoor.position.x, TeleportDoor.position.y, player.transform.position.z);
    }
}
