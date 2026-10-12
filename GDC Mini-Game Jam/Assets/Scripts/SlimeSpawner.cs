using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;

public class SlimeSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float time = 90f; //Current time
    public float startTime = 90f; //Starting time
    public TextMeshProUGUI timerDisplay; //TextMeshPro text
    float nextSpawn;//Next spawn time
    float lastSpawn;//The time the last slime spawned
    AudioSource audio;
    public GameObject slimePrefab;//Original slime object
    public Button buttonPrefab;//Buttin
    [SerializeField] private Transform parentTransform; //Canvas object
    List<GameObject> slimes = new List<GameObject>();
    void Start()
    {
        audio = GetComponent<AudioSource>();//Plays the spawn sound
        time = startTime;
        lastSpawn = time; 
        nextSpawn = Random.Range(3f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (time >= 0.0f)
        {
            time -= Time.deltaTime;//Iterates over time
            timerDisplay.text = Mathf.Floor(time).ToString();
            Slime.OnSlimeClicked += DespawnSlime; //Adds despawnSlime method to OnSlimeClicked event
            if (lastSpawn - time >= nextSpawn)
            {
                bool canSpawn = false;
                Vector3 spawnPosition = new Vector3(0f, 0f, 0f);
                while(!canSpawn)//Generates random position so long as it doesn't fall within another existing sime's radius
                {
                    float spawnX = Random.Range(-8.2f, 8.2f);//Range of possible x and y positions a slime can spawn in,        IMPORTANT FOR PLAYTESTING
                    float spawnY = Random.Range(-4.5f, 4.5f);
                    canSpawn = true;
                    foreach (GameObject slime in slimes){
                        if ( (slime.GetComponent<Slime>().truePosition.x + 0.5f > spawnX && slime.GetComponent<Slime>().truePosition.x - 0.5f < spawnX) && (slime.GetComponent<Slime>().truePosition.y + 0.5f > spawnY && slime.GetComponent<Slime>().truePosition.y - 0.5f < spawnY))
                        {
                            canSpawn = false;
                        }
                    }
                    if (canSpawn)
                    {
                        spawnPosition = new Vector3(spawnX, spawnY, 0f);
                    }
                }
                GameObject newSlime = Instantiate(slimePrefab, spawnPosition, transform.rotation);//Creates slime clone
                Slime slimeScript = newSlime.GetComponent<Slime>();//Gets script from cloned slime
                slimeScript.slimeInstance = newSlime;
                slimeScript.buttonInstance = Instantiate(buttonPrefab, parentTransform);
                newSlime.SetActive(true);//Activates new slime
                slimes.Add(newSlime);
                lastSpawn = time;
                nextSpawn = (startTime - time)/startTime < 0.5f ? (float)Random.Range(1.5f, 2f) : (startTime - time)/startTime < (5f/6f)? (float)Random.Range(0.75f,1.5f): (float)Random.Range(0.4f, 0.75f); //Changes the rate of spawn depending on what percentage of the total time is left    IMPORTANT FOR PLAY TESTING
                //nextSpawn = Random.Range(0.1f, 2f); Alternative implimentation of random slime spawning
            }
        }
        else//Win/lose at end of game
        {
            if (100f * ((float)slimes.Count/148f) >= 40)
            {
                //Lose
            }
            else
            {
                //Win
            }
        }
    }
    void DespawnSlime(GameObject slime)
    {
        audio.Play();
        slimes.Remove(slime);
        slime.SetActive(false);
    }
}
