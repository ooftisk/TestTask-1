using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CharacterHealth playerHealth;
    public Transform playerTransform;
    public InventoryManager inventory;

    private void Start()
    {
        //LoadGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void SaveGame()
    {
        PlayerData data = new PlayerData
        {
            playerPosition = playerTransform.position,
            health = playerHealth.GetCurrentHP(),
        };

        SaveSystem.Save(data, "player");
        inventory.SaveInventory();
    }

    public void LoadGame()
    {
        PlayerData data = SaveSystem.Load<PlayerData>("player");
        playerTransform.position = data.playerPosition;
        playerHealth.SetHealth(data.health);
        inventory.LoadInventory();
    }
}
