using UnityEngine;

public abstract class TraitsScript : ScriptableObject
{
    public string traitName;
    public string hiddenTraitDescription;
    public string visibleTraitDescription;
    public string traitArchetype;


    public int resolveChance;

    public virtual void OnTraitApplied(BaseCharacter character)
    {
        // Implement trait application logic here
    }
    public virtual void OnTraitRemoved(BaseCharacter character)
    {
        // Implement trait removal logic here
    }
    public virtual void OnCombatStart(BaseCharacter character)
    {
        // Implement trait activation logic here
    }
    public virtual void OnCombatEnd(BaseCharacter character)
    {
        // Implement trait deactivation logic here
    }
    public virtual void OnTurnStart(BaseCharacter character)
    {
        // Implement trait turn start logic here
    }
    public virtual void OnTurnEnd(BaseCharacter character)
    {
        // Implement trait turn end logic here
    }
    public virtual void OnDamageTaken(BaseCharacter character, float damage)
    {
        // Implement trait damage taken logic here
    }
    public virtual void OnDamageDealt(BaseCharacter character, float damage)
    {
        // Implement trait damage dealt logic here
    }
    public virtual void OnDeath(BaseCharacter character)
    {
        // Implement trait death logic here
    }
    public virtual void OnEventStart(BaseCharacter character, string eventName)
    {
        // Implement trait event start logic here make a class for events and pass the event object instead of just the name
    }

}
