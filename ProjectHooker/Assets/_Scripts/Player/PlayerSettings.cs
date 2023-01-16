using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "ProjectHooker/PlayerSettings", order = 0)]
public class PlayerSettings : ScriptableObject {
    [Header("Movement")]
    [Header("Ground")]
    [Range(0.1f,10f)]public float MaxMoveSpeed = 5f;
    [Range(0.1f,10f)]public float Acceleration = 5f;
    [Range(0.1f,10f)]public float Deceleration = 5f;
    [Range(0.1f,10f)]public float JumpForce = 5f;
    [Header("Air")]
    [Range(0.1f,10f)]public float AirAcceleration = 5f;
    [Range(0.1f,10f)]public float AirDeceleration = 5f;
    [Range(0f,50f)] public float TerminalVelocity = 30f;
    [Space(10)]
    [Header("Jumping")]
    [Range(0f,2f)] public float CayoteeTime = 0.2f;
    [Range(0f,2f)] public float JumpBufferTime = 0.2f;


}