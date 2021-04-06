using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleTeleport : MonoBehaviour
{
    private Vector2 teleportPosition;

    public void setTeleportPosition(Vector2 teleportPosition)
    {
        this.teleportPosition = teleportPosition;
    }

    public Vector2 getTeleportPosition()
    {
        return teleportPosition;
    }
}
