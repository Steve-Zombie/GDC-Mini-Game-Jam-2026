
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;


public class Slime : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool isHard = false;
    float timeTillHard;
    public GameObject slimeInstance;
    public int colorNumber;
    public static event Action<GameObject> OnSlimeClicked;
    Renderer renderer;
    Color[,] colors = {{new Color(255f/255f, 0f, 0f, 1f), new Color(255f/255f, 0f, 170f/255f, 1f), new Color(0f/255f, 142f/255f, 255f/255f, 1f), new Color(0f/255f, 255f/255f, 47f/255f, 1f), new Color(241f/255f, 255f/255f, 71f/255f, 1f)}, {new Color(196f/255f, 69f/255f, 69f/255f, 1f), new Color(209f/255f, 92f/255f, 170f/255f, 1f), new Color(86f/255f, 149f/255f, 200f/255f, 1f), new Color(88f/255f, 198f/255f, 108f/255f, 1f), new Color(216f/255f, 255f/255f, 71f/255f, 1f)}};

    void Start()
    {
        
    }
    void Awake()
    {
        slimeInstance.SetActive(true);
        timeTillHard = 3f;
        colorNumber = UnityEngine.Random.Range(0, 5); 
        renderer = GetComponent<Renderer>();
        renderer.material.color = colors[0,colorNumber];
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!isHard)
        {
            timeTillHard -= Time.deltaTime;
            if (timeTillHard <= 0)
            {
                isHard = true;
                renderer.material.color = colors[1, colorNumber];
            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Was Clicked");
        if(!isHard)
        {
            OnSlimeClicked?.Invoke(slimeInstance);
        }
    }


}
