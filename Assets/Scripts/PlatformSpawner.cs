 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public static PlatformSpawner instance;

    [SerializeField]
    public GameObject left_Plastform, right_Platform;

    public float left_X_Min = -4.4f, left_X_Max = -2.8f, right_X_Min = 4.4f, right_X_Max = 2.8f;
    public float y_Threshold = 2.6f;
    public float last_y;

    public int spawn_Count = 8;
    public int platform_Spawned;

    [SerializeField]
    public Transform platform_Parent;

    // more variables to spawn bird enemy
    [SerializeField]
    private GameObject bird;
    public float bird_Y = 5f;
    private float bird_X_Min = -2.3f, bird_X_Max = 2.3f;


    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        
    }

    void Start()
    {
        last_y = transform.position.y;

        SpawnPlatform();
        
    }

    // Update is called once per frame
    public void SpawnPlatform()
    {
        Vector2 temp = transform.position;
        GameObject newPlatform = null;

        for(int i=0; i < spawn_Count; i++)
        {
            temp.y = last_y;

            //we have even number
             if((platform_Spawned % 2) == 0)
            {
                temp.x = Random.Range(left_X_Min, left_X_Max);

                newPlatform = Instantiate(right_Platform , temp, Quaternion.identity);

            }

            //we have odd number
            else
            {
                temp.x = Random.Range(right_X_Min, right_X_Max);

                newPlatform = Instantiate(left_Plastform , temp, Quaternion.identity);

            }

            newPlatform.transform.parent = platform_Parent;

            last_y += y_Threshold;
            platform_Spawned++;
        }
        if (Random.Range(0, 2) > 0)
        {
            SpawnBird();
        }
    }// spawn platforms

    void SpawnBird()
    {

        Vector2 temp = transform.position;
        temp.x = Random.Range(bird_X_Min, bird_X_Max);
        temp.y += bird_Y;

        GameObject newBird = Instantiate(bird, temp, Quaternion.identity);
        newBird.transform.parent = platform_Parent;

    }

} // class
