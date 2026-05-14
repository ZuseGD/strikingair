using System.Collections.Generic;
using UnityEngine;

// Enums can live in their own file eventually, but here is fine for now.
public enum Race { Human, Elf, Dwarf, Orc, Goblin, Tiefling, DarkElf }

[System.Serializable]
public class CharacterData
{
    public string characterName;
    public Race race;
    
    public float maxHealth;
    public float currentHealth;
    public int strength;
    public int dexterity;
    public int intelligence;
    public int charisma;
    public int wisdom;

    public List<TraitsScript> acquiredTraits = new List<TraitsScript>();

    // Constructor
    public CharacterData(string name, Race race, float health, int str, int dex, int intel, int cha, int wis)
    {
        this.characterName = name;
        this.race = race;
        this.maxHealth = health;
        this.currentHealth = health;
        this.strength = str;
        this.dexterity = dex;
        this.intelligence = intel;
        this.charisma = cha;
        this.wisdom = wis;
    }

    public void AddTrait(TraitsScript trait)
    {
        if (!acquiredTraits.Contains(trait))
        {
            acquiredTraits.Add(trait);
            trait.OnTraitApplied(this);
        }
    }

    public void RemoveTrait(TraitsScript trait)
    {
        if (acquiredTraits.Contains(trait))
        {
            acquiredTraits.Remove(trait);
            trait.OnTraitRemoved(this);
        }
    }
}