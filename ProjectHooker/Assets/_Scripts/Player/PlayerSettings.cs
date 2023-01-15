using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "ProjectHooker/PlayerSettings", order = 0)]
public class PlayerSettings : ScriptableObject {
    [Header("Movement")]
    public float moveSpeed = 5f;
    
}