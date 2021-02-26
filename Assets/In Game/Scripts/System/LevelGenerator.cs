using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Direction
{
    Left = -90,
    Right = 90,
    Forward = 0
}

public class LevelGenerator : MonoBehaviour
{
    Direction nextTurn;
    Direction gameDirection;

    bool nextU;
    bool isStart;

    string roadType;

    int levelCountAtStart = 2;

    [SerializeField] private int cornerCountForLevel;
    [SerializeField] private float spawnerMoveDistanceStraight;
    [SerializeField] private float spawnerMoveDistanceTurn;

    public Transform spawner;

    private void Start()
    {
        isStart = true;
        GenerateGame();

        EventManager.LevelPassed += GenerateLevel;
        EventManager.GameRestarted += GenerateGame;
    }

    private void GenerateGame()
    {
        gameDirection = Direction.Forward;
        nextU = false;
        spawner.position = new Vector3(0, 0, -30);
        spawner.rotation = Quaternion.LookRotation(Vector3.forward);

        ObjectPooling.Instance.ResetPool();

        int generateTime = 0;        
        while (levelCountAtStart > generateTime)
        {
            GenerateLevel();
            generateTime++;
        }
    }

    private void GenerateLevel()
    {
        isStart = true;

        int _cornerCount = 0;        
        while (_cornerCount < cornerCountForLevel)
        {
            if (isStart)
                DeployStartUnit();
            else
            {
                // Decide turn direction to prevent dead ends.
                switch(gameDirection)
                {                   
                    case Direction.Forward:
                        if (Random.value < .5f)
                        {
                            nextTurn = Direction.Left;
                            gameDirection = Direction.Left;
                            roadType = "Left_Corner_1";
                        }
                        else
                        {
                            nextTurn = Direction.Right;
                            gameDirection = Direction.Right;
                            roadType = "Right_Corner_1";
                        }                            
                        break;
                    case Direction.Left:
                        nextTurn = Direction.Right;

                        if (Random.value < .3f)
                        {
                            nextU = true;
                            gameDirection = Direction.Right;
                            roadType = "Right_U";
                        }
                        else
                        {
                            nextU = false;
                            gameDirection = Direction.Forward;
                            roadType = "Right_Corner_2";
                        }
                        break;
                    case Direction.Right:
                        nextTurn = Direction.Left;

                        if (Random.value < .3f)
                        {
                            nextU = true;
                            gameDirection = Direction.Left;
                            roadType = "Left_U";
                        }
                        else
                        {
                            nextU = false;
                            gameDirection = Direction.Forward;
                            roadType = "Left_Corner_2";
                        }
                        break;
                }

                if (nextU)
                    SetU(ObjectPooling.Instance.GetFromPool(roadType));
                else
                    SetCorner(ObjectPooling.Instance.GetFromPool(roadType));

                SetStraight(ObjectPooling.Instance.GetFromPool("Straight"));
                _cornerCount++;
            }
        }        
    }

    private void DeployStartUnit()
    {
        GameObject _startUnit = ObjectPooling.Instance.GetFromPool("Start Unit");
        SetStraight(_startUnit);

        // Move the spawner to the next position for placing next road.        
        spawner.Translate(Vector3.forward * spawnerMoveDistanceStraight * 3);
        isStart = false;
    }


    private void SetStraight(GameObject road)
    {
        road.SetActive(true);
        road.transform.position = spawner.position;
        road.transform.rotation = spawner.rotation;

        // Move the spawner to the next position for placing next road.
        spawner.Translate(Vector3.forward * spawnerMoveDistanceStraight);
    }

    private void SetCorner(GameObject road)
    {
        road.SetActive(true);
        road.transform.position = spawner.position;

        // Move the spawner to the next position for placing next road.
        spawner.Translate(Vector3.forward * spawnerMoveDistanceTurn);
        spawner.Rotate(Vector3.up * (float)nextTurn);
        spawner.Translate(Vector3.forward * spawnerMoveDistanceTurn);
    }

    private void SetU(GameObject road)
    {
        road.SetActive(true);
        road.transform.position = spawner.position;
        
        // Move the spawner to the next position for placing next road.
        spawner.Translate(Vector3.forward * spawnerMoveDistanceTurn);
        spawner.Rotate(Vector3.up * (float)nextTurn);
        spawner.Translate(Vector3.forward * spawnerMoveDistanceTurn);
        spawner.Translate(Vector3.forward * spawnerMoveDistanceTurn);
        spawner.Rotate(Vector3.up * (float)nextTurn);
        spawner.Translate(Vector3.forward * spawnerMoveDistanceTurn);
    }

}
