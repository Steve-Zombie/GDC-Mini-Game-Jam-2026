using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ClickScript : MonoBehaviour
{
    [SerializeField] private Camera camera;
    private InputAction clickAction;
    public static event Action DidMouseClick;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void Awake()
    {
        clickAction = new InputAction(binding: "<Mouse>/leftButton");
        clickAction.performed += ctx => OnPointerClick();
        clickAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void OnPointerClick()
    {
        Debug.Log("Mouse had been clicked which is why this is heppening.");
        RaycastHit hit;
        Vector3 pos = Mouse.current.position.ReadValue();
        if(Physics.Raycast(camera.ScreenPointToRay(pos), out hit))
        {
            Debug.Log("Funny third option");
            hit.collider.GetComponent<IClickable>()?.OnClick();
        }
    }
}
