using UnityEngine;

// This is the main controller for the character in the scene. It holds a reference to the CharacterData, which contains all the stats and traits. 
// The GameCharacterController will handle interactions with the world, such as taking damage, applying traits, and updating visuals based on the data.
public class GameCharacterController : MonoBehaviour
{
    // This holds all the stats and logic
    public CharacterData Data; 

    public void Initialize(CharacterData generatedData)
    {
        Data = generatedData;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        // sprite renderer to update
        gameObject.name = Data.characterName;
    }

    // Example of how combat will interface with this character
    public void TakeDamage(float amount)
    {
        Data.currentHealth -= amount;
        
        // Notify traits
        foreach(var trait in Data.acquiredTraits)
        {
            trait.OnDamageTaken(Data, amount);
        }

        if (Data.currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        foreach(var trait in Data.acquiredTraits)
        {
            trait.OnDeath(Data);
        }

        // shout to the rest of the game that this character died
        GameEvents.TriggerCharacterDied(Data);

        // Visually turn off the character so they disappear from the screen. We can add death animations and FX later.
        gameObject.SetActive(false);
    }
}