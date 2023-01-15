using UnityEngine;

public class InputManager : MonoBehaviour {
    private void Awake() {
        if(RefManager.inputManager != null) {
            Destroy(gameObject);
            return;
        }
        RefManager.inputManager = this;
    }
}