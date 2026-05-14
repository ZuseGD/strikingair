using System.Collections.Generic;
using UnityEngine;

public static class GeneratorScript 
{
    // We map the Enum to the base modifiers. 
    // these modifiers can later be turned into ScriptableObjects so its easier to balance and expand without code changes.
    private static readonly Dictionary<Race, (int health, int strength, int dexterity, int intelligence, int charisma, int wisdom)> raceStatModifiers = new Dictionary<Race, (int, int, int, int, int, int)>
    {
        {Race.Human, (1, 1, 1, 1, 1, 1)},
        {Race.Elf, (0, -2, 2, -1, 1, -1)},
        {Race.Dwarf, (0, 0, 0, 0, 0, 0)},
        {Race.Orc, (0, 0, 0, 0, 0, 0)},
        {Race.Goblin, (0, 0, 0, 0, 0, 0)},
        {Race.Tiefling, (0, 0, 0, 0, 0, 0)},
        {Race.DarkElf, (0, 0, 0, 0, 0, 0)},
    };

    public static CharacterData GenerateCharacter(int corruptionLevel, CharacterData mentor = null)
    {
        int maxRoll = 11 + corruptionLevel; 
        
        Race rolledRace = (Race)Random.Range(0, System.Enum.GetValues(typeof(Race)).Length);
        string rolledName = "Recruit_" + Random.Range(1000, 9999); 
        
        var mods = raceStatModifiers[rolledRace];

        float maxHealth = Random.Range(80, maxRoll * 10) + (mods.health * 10);
        int rolledStrength = Random.Range(1, maxRoll) + mods.strength;
        int rolledDexterity = Random.Range(1, maxRoll) + mods.dexterity;
        int rolledIntelligence = Random.Range(1, maxRoll) + mods.intelligence;
        int rolledCharisma = Random.Range(1, maxRoll) + mods.charisma;
        int rolledWisdom = Random.Range(1, maxRoll) + mods.wisdom;
        
        CharacterData generatedCharacter = new CharacterData(
            rolledName, rolledRace, maxHealth, rolledStrength, rolledDexterity, rolledIntelligence, rolledCharisma, rolledWisdom
        );

        int traitCount = (corruptionLevel / 3) + 1; 
        for (int i = 0; i < traitCount; i++)
        {
            TraitsScript randomTrait = GetRandomTrait();
            if (randomTrait != null)
            {
                generatedCharacter.AddTrait(randomTrait);
            }
        }

        return generatedCharacter;
    }

    private static TraitsScript GetRandomTrait()
    {
        // Placeholder: Will load from Resources or Addressables later
        return null; 
    }
}