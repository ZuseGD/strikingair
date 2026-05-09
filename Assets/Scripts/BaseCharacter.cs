using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    public enum Race
    {
        Human,
        Elf,
        Dwarf,
        Orc,
        Goblin,
        Tiefling,
        DarkElf
    }


    public Race race;
    public string characterName;
    public float health;
    public int strength;
    public int dexterity;
    public int intelligence;
    public int charisma;
    public int wisdom;
    public List<TraitsScript> acquiredTraits = new List<TraitsScript>();

    // Contructors for the generator
    public BaseCharacter(string name, Race race, float health, int strength, int dexterity, int intelligence, int charisma, int wisdom)
    {
        this.characterName = name;
        this.race = race;
        this.health = health;
        this.strength = strength;
        this.dexterity = dexterity;
        this.intelligence = intelligence;
        this.charisma = charisma;
        this.wisdom = wisdom;
    }

    // Function for adding traits
    public void AddTrait(TraitsScript trait)
    {
        acquiredTraits.Add(trait);
        trait.OnTraitApplied(this);
    }
    public void RemoveTrait(TraitsScript trait)
    {
        acquiredTraits.Remove(trait);
        trait.OnTraitRemoved(this);
    }



    void Start()
    {
        
    }
    
}
