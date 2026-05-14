using System.Collections.Generic;
using UnityEngine;

// This is a simple script to test the combat system without needing to set up triggers or dialogue. Just drag and drop the Player and Enemy prefabs, and the CombatManager, then hit play!
public class TestRunner : MonoBehaviour
{
    [Header("Drag and Drop in Inspector")]
    public GameCharacterController playerUnit;   // Drag your Player prefab here
    public GameCharacterController enemyUnit;    // Drag your Enemy prefab here
    public CombatManager combatManager;      // Drag the Manager object here

    void Start()
    {
        CharacterData playerData = GeneratorScript.GenerateCharacter(0);
        CharacterData enemyData = GeneratorScript.GenerateCharacter(3); 
        
        playerUnit.Initialize(playerData);
        enemyUnit.Initialize(enemyData);


        // The CombatManager expects Lists, even if there is only 1 person on the team right now
        List<GameCharacterController> playerTeam = new List<GameCharacterController>();
        playerTeam.Add(playerUnit);

        List<GameCharacterController> enemyTeam = new List<GameCharacterController>();
        enemyTeam.Add(enemyUnit);

        // Start the combat and watch the debug logs for how it plays out!
        Debug.Log("--- STARTING COMBAT ---");
        combatManager.StartCombat(playerTeam, enemyTeam);
    }
}