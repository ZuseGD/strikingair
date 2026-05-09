using UnityEngine;

[CreateAssetMenu(fileName = "BaseCharacter", menuName = "Scriptable Objects/BaseCharacter")]
public class BaseCharacter : ScriptableObject
{
    public string characterName;
    [SerializeField] public float health;
    public int strength;
    public int dexterity;
    public int intelligence;
    public int charisma;
    public int wisdom;

    
    
}
