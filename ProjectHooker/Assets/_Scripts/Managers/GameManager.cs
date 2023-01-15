using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Action<GameState, GameState> OnGameStateChanged = delegate { };
    public GameState state { get; private set; }
    public enum GameState {
        Menu,
        Game,
        Pause
    }
    private void Awake() {
        if (RefManager.gameManager != null) {
            Destroy(gameObject);
            return;
        }
        RefManager.gameManager = this;
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnSceneChanged(Scene current, Scene next) {
        if (next.name == "Game") {
            RefManager.playerController.gameObject.SetActive(true);
        }
    }
    
    private void ChangeState(GameState newState) {
        GameState oldState = state;
        state = newState;
        OnGameStateChanged(oldState, newState);
    }
}
