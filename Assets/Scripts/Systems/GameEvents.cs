using System;
using UnityEngine;

// A simple static event class that any script can access
public static class GameEvents 
{
    // C# Actions act as megaphones. 
    public static event Action<CharacterData, float> OnCharacterDamaged;
    public static event Action<CharacterData> OnCharacterDied;
    public static event Action<CharacterData, CharacterData> OnAttackFired;

    // A method to trigger the megaphone
    public static void TriggerCharacterDamaged(CharacterData target, float damage)
    {
        OnCharacterDamaged?.Invoke(target, damage); // The '?' checks if anyone is listening
    }
    public static void TriggerCharacterDied(CharacterData deadCharacter)
    {
        OnCharacterDied?.Invoke(deadCharacter);
    }
}