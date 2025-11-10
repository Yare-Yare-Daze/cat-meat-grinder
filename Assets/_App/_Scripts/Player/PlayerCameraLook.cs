using System;
using UnityEngine;
using Zenject;

public class PlayerCameraLook : MonoBehaviour
{
    [SerializeField] private float sensitivity = 100f;
    [SerializeField] private Transform playerBody; // обычно камера — это дочерний объект игрока
    
    private InputSystem_Actions controls;
    private Vector2 lookInput;
    
    private float xRotation;
    private float yRotation;

    private bool _isPlayerCanvas;
    
    [Inject] private CanvasesManager _canvasesManager;
    
    private void Awake()
    {
        //controls = new PlayerControls();
        //controls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        //controls.Player.Look.canceled += _ => lookInput = Vector2.zero;

        _canvasesManager.OnCanvasTypeChanged += OnCanvasTypeChangedHandler;
    }

    private void OnCanvasTypeChangedHandler(CanvasType canvasType)
    {
        switch (canvasType)
        {
            case CanvasType.PlayerCanvas:
                SetPlayerCursor();
                break;
            case CanvasType.ComputerCanvas:
                DisablePlayerCursor();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(canvasType), canvasType, null);
        }
    }

    private void Start()
    {
        SetPlayerCursor();
    }

    private void SetPlayerCursor()
    {
        _isPlayerCanvas = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void DisablePlayerCursor()
    {
        _isPlayerCanvas = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // private void OnEnable() => controls.Enable();
    // private void OnDisable() => controls.Disable();
    
    void Update()
    {
        if(!_isPlayerCanvas) return;
        
        // Читаем мышь
        //float mouseX = lookInput.x * sensitivity * Time.deltaTime;
        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;
        //float mouseY = lookInput.y * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;
    
        // Вертикальное вращение камеры (вверх/вниз)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // ограничение углов
        
        yRotation += mouseX;
    
        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
    
        // Горизонтальное вращение всего тела игрока
        //playerBody.Rotate(Vector3.up * mouseX);
        playerBody.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
