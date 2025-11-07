using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionAsset _inputActionAsset;
    protected InputAction moveAction;
    public InputAction GetMoveAction() => moveAction;

    [Header("Movement")] 
    [SerializeField] private float moveSpeed;
    public float GetMoveSpeed() => moveSpeed;

    [Header("Components")]
    private Rigidbody2D _rb;
    public Rigidbody2D GetRigidbody2D() => _rb;
    void Awake()
    {
        moveAction = _inputActionAsset.FindAction("Walk");
        _rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        _inputActionAsset.FindActionMap("Player").Enable();
    }

    void OnDisable()
    {
        _inputActionAsset.FindActionMap("Player").Disable();
    }

}
