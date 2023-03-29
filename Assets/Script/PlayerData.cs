using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData  
{
    public int _level;
    public int _health;
    public float[] _position;

    public PlayerData(PlayerMovement player)
    {
        _level = player._level;
        _health = player._health;

        _position = new float[3];
        _position[0] = player.transform.position.x;
        _position[1] = player.transform.position.y;
        _position[2] = player.transform.position.z;
    }


}
