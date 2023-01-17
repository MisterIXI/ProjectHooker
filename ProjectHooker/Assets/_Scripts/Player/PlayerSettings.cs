using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "ProjectHooker/PlayerSettings", order = 0)]
public class PlayerSettings : ScriptableObject {
    [Header("Movement")]
    [Header("Ground")]
    [Range(0.1f,100f)]public float MaxMoveSpeed = 5f;
    [Range(0f,300f)]public float Acceleration = 5f;
    [Range(0f,300f)]public float Deceleration = 5f;
    [Header("Air")]
    [Range(0.1f,10f)]public float AirAcceleration = 5f;
    [Range(0.1f,10f)]public float AirDeceleration = 5f;
    [Range(0f,50f)] public float TerminalVelocity = 30f;
    [Space(10)]
    [Header("Jumping")]
    [Range(0f,2f)] public float CoyoteTime = 0.2f;
    [Range(0f,2f)] public float JumpBufferTime = 0.2f;
    [Range(0.1f,10f)]public float JumpVelocity = 5f;
    [Tooltip("The percentage of the JumpVelocity that the vertical velocity is clamped to on release of the jump button")]
    [Range(0f,1f)]public float VarHeightModifier= 0.5f;


}