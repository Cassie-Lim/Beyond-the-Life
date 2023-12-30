using UnityEngine;

public static class GameSaver
{
    public static void SaveLevel(int levelIndex)
    {
        PlayerPrefs.SetInt("CurrentLevel", levelIndex);
        PlayerPrefs.Save();
    }

    public static void SaveCharacterPositionAndDirection(Character character, string characterKey)
    {
        PlayerPrefs.SetFloat(characterKey + "_PosX", character.position.x);
        PlayerPrefs.SetFloat(characterKey + "_PosY", character.position.y);
        PlayerPrefs.SetFloat(characterKey + "_PosZ", character.position.z);
        
        PlayerPrefs.SetFloat(characterKey + "_DirX", character.direction.x);
        PlayerPrefs.SetFloat(characterKey + "_DirY", character.direction.y);
        PlayerPrefs.SetFloat(characterKey + "_DirZ", character.direction.z);

        PlayerPrefs.Save();
    }

    public static int LoadLevel()
    {
        return PlayerPrefs.GetInt("CurrentLevel", 1); // Default to level 1 if no save exists
    }

    public static Character LoadCharacterPositionAndDirection(string characterKey)
    {
        Vector3 position = new Vector3(
            PlayerPrefs.GetFloat(characterKey + "_PosX", 0),
            PlayerPrefs.GetFloat(characterKey + "_PosY", 0),
            PlayerPrefs.GetFloat(characterKey + "_PosZ", 0)
        );

        Vector3 direction = new Vector3(
            PlayerPrefs.GetFloat(characterKey + "_DirX", 0),
            PlayerPrefs.GetFloat(characterKey + "_DirY", 0),
            PlayerPrefs.GetFloat(characterKey + "_DirZ", 1) // Default direction is forward
        );

        return new Character(position, direction);
    }
}
