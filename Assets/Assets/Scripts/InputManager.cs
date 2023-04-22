using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    public static InputManager Instance { get; private set; }

    private PlayersControls playerControls;

    private void Awake() {
        if(Instance != null && Instance != this) {
            Destroy(this.gameObject);
        } else {
            Instance = this;
        }

        playerControls = new PlayersControls();
        Cursor.visible = false;
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void OnDisable() {
        playerControls.Disable();
    }

    public Vector2 GetPlayerMovement() {
        return playerControls.Player.Movement.ReadValue<Vector2>();
    }
    
    public Vector2 GetMouseDelta() {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }

    public bool PlayerJumpedThisFrame() {
        return playerControls.Player.Jump.triggered;
    }

    public bool PlayerFiredThisFrame() {
        return playerControls.Player.Fire.triggered;
    }
}
