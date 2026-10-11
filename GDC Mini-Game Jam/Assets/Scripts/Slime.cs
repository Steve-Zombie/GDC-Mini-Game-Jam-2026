
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;
using UnityEngine.InputSystem;
using UnityEngine.UI;



public class Slime : MonoBehaviour
{
    public Button buttonInstance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool isHard = false;
    float timeTillHard;
    public GameObject slimeInstance;//The current slime object
    public int colorNumber;
    public static event Action<GameObject> OnSlimeClicked;//The event that despawns the slime
    SpriteRenderer renderer;
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
        audio.Play();//Plays enterance sound
        slimeInstance.SetActive(true);
        timeTillHard = 3f;//Time until slime becomes hard       IMPORTANT FOR PLAYTESTING   
        colorNumber = UnityEngine.Random.Range(0, 5); 
        renderer = GetComponent<SpriteRenderer>();
        renderer.color = colors[0,colorNumber];

        //Code that makes button's position on the canvas mirror the slime's position on the screen
        buttonRect = buttonInstance.GetComponent<RectTransform>();
        targetTransform = transform;
        buttonInstance.onClick.AddListener(SlimeWasClicked);
        Vector3 screenPos = camera.WorldToScreenPoint(targetTransform.position);
        RectTransform canvasRect = buttonRect.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, null, out localPos);

        // Set button position
        buttonRect.localPosition = localPos;
        buttonInstance.onClick.AddListener(SlimeWasClicked);//Adds a listener that calls SlimeWasClicked when the corresponding button is clicked

        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!isHard)
        {
            
            timeTillHard -= Time.deltaTime;
            if (timeTillHard <= 0)
            {
                isHard = true;//Changes color when hard
                renderer.material.color = colors[1, colorNumber];
            }
            
        }
        

        
    }

    public void SlimeWasClicked()
    {

        
        if(!isHard)
        {
            OnSlimeClicked?.Invoke(slimeInstance);//Invokes the despawn method in sime spawner when a this is activated

        }
    }


}
