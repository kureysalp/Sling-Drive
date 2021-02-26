using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    private static ObjectPooling instance;
    public static ObjectPooling Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != this && instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    public GameObject straightRoad;
    public GameObject cornerRoadLeft_1;
    public GameObject cornerRoadLeft_2;
    public GameObject uRoadLeft;
    public GameObject cornerRoadRight_1;
    public GameObject cornerRoadRight_2;
    public GameObject uRoadRight;
    public GameObject startUnit;

    [SerializeField] private int poolCount;

    List<GameObject> straightRoads = new List<GameObject>();
    List<GameObject> cornerRoadsLeft_1 = new List<GameObject>();
    List<GameObject> cornerRoadsLeft_2 = new List<GameObject>();
    List<GameObject> uRoadsLeft = new List<GameObject>();
    List<GameObject> cornerRoadsRight_1 = new List<GameObject>();
    List<GameObject> cornerRoadsRight_2 = new List<GameObject>();
    List<GameObject> uRoadsRight = new List<GameObject>();
    List<GameObject> startUnits = new List<GameObject>();


    private void Start()
    {
        // Set up the pool.
        FillThePool();
    }

    private void FillThePool()
    {        
        for (int i = 0; i < poolCount; i++)
        {
            GameObject _spawnedStraightRoad = Instantiate(straightRoad);
            _spawnedStraightRoad.SetActive(false);
            _spawnedStraightRoad.transform.parent = transform;
            straightRoads.Add(_spawnedStraightRoad);

            GameObject _spawnedCornerRoadLeft_1 = Instantiate(cornerRoadLeft_1);
            _spawnedCornerRoadLeft_1.SetActive(false);
            _spawnedCornerRoadLeft_1.transform.parent = transform;
            cornerRoadsLeft_1.Add(_spawnedCornerRoadLeft_1);

            GameObject _spawnedCornerRoadLeft_2 = Instantiate(cornerRoadLeft_2);
            _spawnedCornerRoadLeft_2.SetActive(false);
            _spawnedCornerRoadLeft_2.transform.parent = transform;
            cornerRoadsLeft_2.Add(_spawnedCornerRoadLeft_2);

            GameObject _uRoadsLeft = Instantiate(uRoadLeft);
            _uRoadsLeft.SetActive(false);
            _uRoadsLeft.transform.parent = transform;
            uRoadsLeft.Add(_uRoadsLeft);

            GameObject _spawnedCornerRoadRight_1 = Instantiate(cornerRoadRight_1);
            _spawnedCornerRoadRight_1.SetActive(false);
            _spawnedCornerRoadRight_1.transform.parent = transform;
            cornerRoadsRight_1.Add(_spawnedCornerRoadRight_1);

            GameObject _spawnedCornerRoadRight_2 = Instantiate(cornerRoadRight_2);
            _spawnedCornerRoadRight_2.SetActive(false);
            _spawnedCornerRoadRight_2.transform.parent = transform;
            cornerRoadsRight_2.Add(_spawnedCornerRoadRight_2);

            GameObject _uRoadRight = Instantiate(uRoadRight);
            _uRoadRight.SetActive(false);
            _uRoadRight.transform.parent = transform;
            uRoadsRight.Add(_uRoadRight);

            GameObject _spawnedStartUnits = Instantiate(startUnit);
            _spawnedStartUnits.SetActive(false);
            _spawnedStartUnits.transform.parent = transform;
            startUnits.Add(_spawnedStartUnits);
        }
    }

    /// <summary>
    /// Get road object from the pool.
    /// </summary>
    /// <param name="roadType">Name of road type that will return.</param>
    /// <returns></returns>
    public GameObject GetFromPool(string roadType)
    {
        int _iterate = 0;
        switch (roadType)
        {
            case "Straight":                
                foreach (GameObject road in straightRoads)
                {
                    _iterate++;
                    // If all objects in the pool are used, expand the pool.
                    if (_iterate > straightRoads.Count)
                    {
                        FillThePool();
                        GetFromPool(roadType);
                    }

                    if (!road.activeInHierarchy)
                        return road;                    
                }
                break;
            case "Left_Corner_1":                
                foreach (GameObject road in cornerRoadsLeft_1)
                {
                    _iterate++;
                    // If all objects in the pool are used, expand the pool.
                    if (_iterate > cornerRoadsLeft_1.Count)
                    {
                        FillThePool();
                        GetFromPool(roadType);
                    }

                    if (!road.activeInHierarchy)
                        return road;                    
                }
                break;
            case "Left_Corner_2":
                foreach (GameObject road in cornerRoadsLeft_2)
                {
                    _iterate++;
                    // If all objects in the pool are used, expand the pool.
                    if (_iterate > cornerRoadsLeft_2.Count)
                    {
                        FillThePool();
                        GetFromPool(roadType);
                    }

                    if (!road.activeInHierarchy)
                        return road;                    
                }
                break;
            case "Left_U":
                foreach (GameObject road in uRoadsLeft)
                {
                    _iterate++;
                    // If all objects in the pool are used, expand the pool.
                    if (_iterate > uRoadsLeft.Count)
                    {
                        FillThePool();
                        GetFromPool(roadType);
                    }

                    if (!road.activeInHierarchy)
                        return road;                    
                }
                break;                
            case "Right_Corner_1":
                foreach (GameObject road in cornerRoadsRight_1)
                {
                    _iterate++;
                    // If all objects in the pool are used, expand the pool.
                    if (_iterate > cornerRoadsRight_1.Count)
                    {
                        FillThePool();
                        GetFromPool(roadType);
                    }

                    if (!road.activeInHierarchy)
                        return road;                    
                }
                break;
            case "Right_Corner_2":
                foreach (GameObject road in cornerRoadsRight_2)
                {
                    _iterate++;
                    // If all objects in the pool are used, expand the pool.
                    if (_iterate > cornerRoadsRight_2.Count)
                    {
                        FillThePool();
                        GetFromPool(roadType);
                    }

                    if (!road.activeInHierarchy)
                        return road;                    
                }
                break;
            case "Right_U":
                foreach (GameObject road in uRoadsRight)
                {
                    _iterate++;
                    // If all objects in the pool are used, expand the pool.
                    if (_iterate > uRoadsRight.Count)
                    {
                        FillThePool();
                        GetFromPool(roadType);
                    }

                    if (!road.activeInHierarchy)
                        return road;                    
                }
                break;
            case "Start Unit":
                foreach (GameObject road in startUnits)
                {
                    _iterate++;
                    // If all objects in the pool are used, expand the pool.
                    if (_iterate > startUnits.Count)
                    {
                        FillThePool();
                        GetFromPool(roadType);
                    }

                    if (!road.activeInHierarchy)
                        return road;                    
                }
                break;
        }

        return null;
    }

    public void ResetPool()
    {
        for (int i = 0; i < poolCount; i++)
        {
            straightRoads[i].SetActive(false);
            cornerRoadsLeft_1[i].SetActive(false);
            cornerRoadsLeft_2[i].SetActive(false);
            uRoadsLeft[i].SetActive(false);
            cornerRoadsRight_1[i].SetActive(false);
            cornerRoadsRight_2[i].SetActive(false);
            uRoadsRight[i].SetActive(false);
            startUnits[i].SetActive(false);
        }
    }
}
