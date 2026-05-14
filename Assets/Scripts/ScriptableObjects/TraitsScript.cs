using UnityEngine;

[CreateAssetMenu(fileName = "New Trait", menuName = "Game Data/Trait")]
public abstract class TraitsScript : ScriptableObject
{
    [Header("Lore & UI")]
    public string traitName;
    [TextArea] public string hiddenTraitDescription;
    [TextArea] public string visibleTraitDescription;
    public string traitArchetype;

    [Header("Mechanics")]
    public int resolveChance;

    // Passing the raw Data allows the trait to modify stats mathematically
    public virtual void OnTraitApplied(CharacterData character) { }
    public virtual void OnTraitRemoved(CharacterData character) { }
    public virtual void OnCombatStart(CharacterData character) { }
    public virtual void OnCombatEnd(CharacterData character) { }
    public virtual void OnTurnStart(CharacterData character) { }
    public virtual void OnTurnEnd(CharacterData character) { }
    public virtual void OnDamageTaken(CharacterData character, float damage) { }
    public virtual void OnDamageDealt(CharacterData character, float damage) { }
    public virtual void OnDeath(CharacterData character) { }
    
    // Using a generic object or custom EventData class is better than just a string for flexibility, but this is fine for now.
    public virtual void OnEventStart(CharacterData character, string eventName) { }
}