using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public bool IsGrounded { get; private set; }
    public float LastGroundedTime { get; private set; }
    private int _groundContacts;
    [SerializeField] private Collider2D _groundCheckCollider;
    private void Awake()
    {
        if (RefManager.playerController != null)
        {
            Destroy(gameObject);
            return;
        }
        RefManager.playerController = this;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        _groundContacts++;
        IsGrounded = true;
        Debug.Log("Grounded");
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        _groundContacts--;
        if (_groundContacts == 0)
        {
            IsGrounded = false;
            LastGroundedTime = Time.time;
            Debug.Log("Not Grounded");
        }
    }

    private void OnDrawGizmos()
    {
        if (IsGrounded)
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.red;
        if (_groundCheckCollider != null)
            Gizmos.DrawWireCube(_groundCheckCollider.bounds.center, _groundCheckCollider.bounds.size);
    }
}