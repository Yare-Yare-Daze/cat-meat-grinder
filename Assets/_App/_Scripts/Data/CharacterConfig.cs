using UnityEngine;

[CreateAssetMenu(fileName = "Character Config", menuName = "Data/Character Config")]
public class CharacterConfig : ScriptableObject
{
    [Header("Movement")] 
    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.5f;
    public float rotationLerp = 10f;
    
    [Header("Jump/Gravity")]
    public float gravity = -9.81f;
    public float jumpHeight = 1.2f;

    [Header("Grounding")] 
    public float groundedSnap = -2f;
}
