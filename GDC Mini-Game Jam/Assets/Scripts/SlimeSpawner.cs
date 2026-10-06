using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class SlimeSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float time = 60f;
    public float startTime = 60f;
    public TextMeshProUGUI timerDisplay;
    float nextSpawn;
    float lastSpawn;
    public GameObject slimePrefab;
    List<GameObject> slimes = new List<GameObject>();
    void Start()
    {
        time = startTime;
        lastSpawn = time; 
        nextSpawn = Random.Range(3f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        timerDisplay.text = Mathf.Floor(time).ToString();
        Slime.OnSlimeClicked += DespawnSlime;
        if (lastSpawn - time >= nextSpawn)
        {
            bool canSpawn = false;
            Vector3 spawnPosition = new Vector3(0f, 0f, 0f);
            while(!canSpawn)
            {
                float spawnX = Random.Range(-4f, 5.5f);
                float spawnY = Random.Range(-2f, 3.5f);
                canSpawn = true;
                foreach (GameObject slime in slimes){
                    if ( (slime.transform.position.x + 1f > spawnX && slime.transform.position.x - 1f < spawnX) && (slime.transform.position.y + 1f > spawnY && slime.transform.position.y - 1f < spawnY))
                    {
                        canSpawn = false;
                    }
                }
                if (canSpawn)
                {
                    spawnPosition = new Vector3(spawnX, spawnY, 0f);
                }
            }
            GameObject newSlime = Instantiate(slimePrefab, spawnPosition, transform.rotation);
            Slime slimeScript = newSlime.GetComponent<Slime>();
            slimeScript.slimeInstance = newSlime;
            newSlime.SetActive(true);
            slimes.Add(newSlime);
            lastSpawn = time;
            nextSpawn = startTime/time > 0.5f ? Random.Range(3f, 5f) : startTime/time > (5f/6f)? Random.Range(2f,3.5f): Random.Range(0.5f, 2f);
        }
    }
    void DespawnSlime(GameObject slime)
    {
        Debug.Log("Slime Removed");
        slimes.Remove(slime);
        slime.SetActive(false);
    }
}
