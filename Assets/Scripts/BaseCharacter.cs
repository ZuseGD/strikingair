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



    void Start()
    {
        
    }
    
}
