using UnityEngine;

[System.Serializable]
public class Character
{
    public Vector3 position;
    public Vector3 direction;

    // Constructor to set the position and direction
    public Character(Vector3 pos, Vector3 dir)
    {
        position = pos;
        direction = dir;
    }
}
