
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public interface IClickable
{
    void OnClick();
}

public class Slime : MonoBehaviour, IClickable
{
    public Button buttonInstance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool isHard = false;
    float timeTillHard;
    public GameObject slimeInstance;
    private bool attemptedSuicide = false; //Remove this when done testing
    public int colorNumber;
    public static event Action<GameObject> OnSlimeClicked;
    Renderer renderer;
    private AudioSource audio;
    public Camera camera;
    private RectTransform buttonRect;
    private Transform targetTransform;
    Color[,] colors = {{new Color(255f/255f, 0f, 0f, 1f), new Color(255f/255f, 0f, 170f/255f, 1f), new Color(0f/255f, 142f/255f, 255f/255f, 1f), new Color(0f/255f, 255f/255f, 47f/255f, 1f), new Color(241f/255f, 255f/255f, 71f/255f, 1f)}, {new Color(196f/255f, 69f/255f, 69f/255f, 1f), new Color(209f/255f, 92f/255f, 170f/255f, 1f), new Color(86f/255f, 149f/255f, 200f/255f, 1f), new Color(88f/255f, 198f/255f, 108f/255f, 1f), new Color(216f/255f, 255f/255f, 71f/255f, 1f)}};

    void Start()
    {
        
    }
    void Awake()
    {
        audio = GetComponent<AudioSource>();
        audio.Play();
        slimeInstance.SetActive(true);
        timeTillHard = 3f;
        colorNumber = UnityEngine.Random.Range(0, 5); 
        renderer = GetComponent<Renderer>();
        renderer.material.color = colors[0,colorNumber];
        buttonRect = buttonInstance.GetComponent<RectTransform>();
        targetTransform = transform;
        buttonInstance.onClick.AddListener(SlimeWasClicked);
        Vector3 screenPos = camera.WorldToScreenPoint(targetTransform.position);
        RectTransform canvasRect = buttonRect.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, null, out localPos);

        // Set button position
        buttonRect.localPosition = localPos;

        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!isHard)
        {
            
            timeTillHard -= Time.deltaTime;
            if(timeTillHard <= 1 && timeTillHard > 0 && ! attemptedSuicide)
            {
                attemptedSuicide = true;
                if(UnityEngine.Random.Range(0, 2) == 1)
                {
                    OnSlimeClicked?.Invoke(slimeInstance);
                }
                
            }
            if (timeTillHard <= 0)
            {
                isHard = true;
                renderer.material.color = colors[1, colorNumber];
            }
            
        }
        buttonInstance.onClick.AddListener(SlimeWasClicked);

        ClickScript.DidMouseClick +=  SlimeWasClicked;
    }
    public void OnClick()
    {
        Debug.Log("This shit actually works");
    }
    public void SlimeWasClicked()
    {

        Debug.Log("Was Clicked");
        if(!isHard)
        {
            OnSlimeClicked?.Invoke(slimeInstance);
        }
    }


}
