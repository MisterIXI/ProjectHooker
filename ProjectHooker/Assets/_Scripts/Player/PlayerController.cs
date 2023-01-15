using UnityEngine;

public class PlayerController : MonoBehaviour
{



    private void Awake()
    {
        if (RefManager.playerController != null)
        {
            Destroy(gameObject);
            return;
        }
        RefManager.playerController = this;
    }
}