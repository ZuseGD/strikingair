using UnityEngine;

public static class GeneratorScript : MonoBehaviour
{
    BaseCharacter character;

    private static Dictionary<Race, (int health, int strength, int dexterity, int intelligence, int charisma, int wisdom)> raceStatModifiers = new Dictionary<Race, (int, int, int, int, int, int)>
    {
        {Race.Human, (1, 1, 1, 1, 1, 1)},
        {Race.Elf, (0, -2, 2, -1, 1, -1)},
        {Race.Dwarf, (0, 0, 0, 0, 0, 0)},
        {Race.Orc, (0, 0, 0, 0, 0, 0)},
        {Race.Goblin, (0, 0, 0, 0, 0, 0)},
        {Race.Tiefling, (0, 0, 0, 0, 0, 0)},
        {Race.DarkElf, (0, 0, 0, 0, 0, 0)},
    };


    public static BaseCharacter GenerateCharacter(int corruptionLevel, BaseCharacter mentor)
    {
        maxRoll = 11 + corruptionLevel; // The higher the corruption level, the lower the max roll for stats, making the character weaker overall. This can be adjusted as needed.  
        Race rolledRace = (Race)Random.Range(0, System.Enum.GetValues(typeof(Race).length));
        // Update Names Later
        string rolledName = "Generated Character";
        float maxHealth = Random.Range(80, maxRoll*10) + (raceStatModifiers[rolledRace].health * 10);
        float currentHealth = maxHealth;
        int rolledStrength = Random.Range(1, maxRoll);
        int rolledDexterity = Random.Range(1, maxRoll);
        int rolledIntelligence = Random.Range(1, maxRoll);
        int rolledCharisma = Random.Range(1, maxRoll);
        int rolledWisdom = Random.Range(1, maxRoll);
        

        BaseCharacter generatedCharacter = new BaseCharacter(rolledName, rolledRace, maxHealth, rolledStrength, rolledDexterity, rolledIntelligence, rolledCharisma, rolledWisdom);
        int traitCount = (corruptionLevel / 3) + 1; // every 3 levels of corruption guarantees an additional trait, starting with 1 trait at level 0. This can be adjusted as needed.
        // Add bad/good trait filter here later, for now it just adds random traits regardless of alignment.
        return generatedCharacter;
    }

    private static TraitsScript GetRandomTrait()
    {
        // this will load a random trait from a pool of traits. For now, it just returns null, but it can be implemented later by loading traits from a Resources folder or using a predefined list of traits.
        return null; // Placeholder return statement
    }


}
