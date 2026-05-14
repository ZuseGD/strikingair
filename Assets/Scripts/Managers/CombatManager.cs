using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [Header("Team Rosters")]
    public List<GameCharacterController> playerTeam = new List<GameCharacterController>();
    public List<GameCharacterController> enemyTeam = new List<GameCharacterController>();

    // This queue holds everyone alive in the fight, sorted by who goes next
    private List<GameCharacterController> turnQueue = new List<GameCharacterController>();

    [Header("Combat Settings")]
    public float timeBetweenTurns = 1.5f; // How long to wait between actions so the player can process what happened

    private bool isCombatActive = false;
    private int roundNumber = 0;

    // combat start function that takes in the player and enemy teams. You can call this from anywhere, like a trigger volume in the world or an event after dialogue.
    public void StartCombat(List<GameCharacterController> players, List<GameCharacterController> enemies)
    {
        Debug.Log("<color=yellow>=== COMBAT INITIATED ===</color>");
        playerTeam = players;
        enemyTeam = enemies;
        isCombatActive = true;
        roundNumber = 0;

        GameEvents.OnCharacterDied += HandleCharacterDeath;

        StartCoroutine(CombatLoopRoutine());
    }

    private IEnumerator CombatLoopRoutine()
    {
        // Initial setup traits (e.g., "OnCombatStart" effects)
        TriggerCombatStartTraits();

        while (isCombatActive)
        {
            roundNumber++;
            Debug.Log($"<color=cyan>--- ROUND {roundNumber} START ---</color>");
            
            BuildTurnQueue();

            // Process each character's turn
            foreach (GameCharacterController activeUnit in turnQueue.ToList()) 
            {
                // If combat ended or the unit died before their turn, skip them
                if (!isCombatActive || activeUnit.Data.currentHealth <= 0) continue;

                yield return StartCoroutine(TakeTurnRoutine(activeUnit));

                // Check for win/loss conditions after every single turn
                CheckCombatEnd();
            }
            
        }
    }

    private void BuildTurnQueue()
    {
        turnQueue.Clear();
        turnQueue.AddRange(playerTeam);
        turnQueue.AddRange(enemyTeam);

        turnQueue = turnQueue.OrderByDescending(unit => unit.Data.dexterity).ToList();
        
        // Debug exactly who is in the queue and in what order
        string queueOrder = "Turn Order: ";
        foreach(var unit in turnQueue) queueOrder += $"{unit.Data.characterName}(Dex:{unit.Data.dexterity}) -> ";
        Debug.Log(queueOrder);
    }

    private IEnumerator TakeTurnRoutine(GameCharacterController unit)
    {
        // turn start
        foreach (var trait in unit.Data.acquiredTraits)
            trait.OnTurnStart(unit.Data);

        // find target and attack
        GameCharacterController target = FindTargetFor(unit);
        
        if (target != null)
        {
            Debug.Log($"{unit.Data.characterName} attacks {target.Data.characterName}!");
            
            // damage calculation based on Strength can be changed later
            float damage = unit.Data.strength * Random.Range(0.8f, 1.2f); 
            int finalDamage = Mathf.RoundToInt(damage); 
            Debug.Log($"<color=orange>{unit.Data.characterName} attacks {target.Data.characterName} for {finalDamage} damage!</color>");
            target.TakeDamage(finalDamage);
        }
        else
        {
            Debug.Log($"{unit.Data.characterName} could not find a target!");
        }

        // wait for animations/FX to finish before the next person goes
        Debug.Log($"<i>Waiting {timeBetweenTurns} seconds...</i>");
        yield return new WaitForSeconds(timeBetweenTurns);

        // turn end
        foreach (var trait in unit.Data.acquiredTraits)
            trait.OnTurnEnd(unit.Data);
        Debug.Log($"<color=grey>[{unit.Data.characterName}'s Turn] ends.</color>");
    }

    private GameCharacterController FindTargetFor(GameCharacterController attacker)
    {
        //  this just picks a random living target from the opposing team. Expandable later for player targeting, AOE attacks, etc.
        List<GameCharacterController> opposingTeam = playerTeam.Contains(attacker) ? enemyTeam : playerTeam;
        List<GameCharacterController> livingTargets = opposingTeam.Where(t => t.Data.currentHealth > 0).ToList();

        if (livingTargets.Count > 0)
        {
            int randomIndex = Random.Range(0, livingTargets.Count);
            return livingTargets[randomIndex];
        }

        return null;
    }

    private void HandleCharacterDeath(CharacterData deadData)
    {
        Debug.Log($"<color=red><b>☠️ {deadData.characterName} HAS DIED! ☠️</b></color>");
        // remove dead characters from the internal lists
        // can also trigger death-related traits or effects here if needed
        playerTeam.RemoveAll(c => c.Data == deadData);
        enemyTeam.RemoveAll(c => c.Data == deadData);
        turnQueue.RemoveAll(c => c.Data == deadData);
    }

    private void CheckCombatEnd()
    {
        if (playerTeam.Count == 0)
        {
            isCombatActive = false;
            Debug.Log("DEFEAT! The party has been wiped out.");
        }
        else if (enemyTeam.Count == 0)
        {
            isCombatActive = false;
            Debug.Log("VICTORY! The party survived.");
        }
    }

    private void OnDestroy()
    {
        // prevent memory leaks
        GameEvents.OnCharacterDied -= HandleCharacterDeath;
    }
    
    private void TriggerCombatStartTraits()
    {
        foreach (var unit in playerTeam.Concat(enemyTeam))
        {
            foreach (var trait in unit.Data.acquiredTraits)
            {
                trait.OnCombatStart(unit.Data);
            }
        }
    }
}