using System;
using System.Collections.Generic;
using System.IO;
using TowerDefense;
using UnityEngine;
using UnityEngine.Assertions;

public enum MapTypes
{
    map_1,
    map_2,
    map_3
}

public class MapManager : MonoBehaviour
{
    [SerializeField] private MapTypes _mapType = MapTypes.map_1;
    [SerializeField] private GameObject _pathTile = null;
    [SerializeField] private GameObject _obstacleTile = null;
    [SerializeField] private GameObject _towerDefenseOne = null;
    [SerializeField] private GameObject _towerDefenseTwo = null;

    [SerializeField] private int cellSize = 2;

    private void Awake()
    {
        Assert.IsNotNull(_pathTile, "Path tile game object has no reference.");
        Assert.IsNotNull(_obstacleTile, "Obstacle Tile game object has no refrence.");
        Assert.IsNotNull(_towerDefenseOne, "Tower Defense type 1 has no reference.");
        Assert.IsNotNull(_towerDefenseTwo, "Tower Defense type 2 has no reference.");

        GenerateMap();
    }

    private void GenerateMap()
    {
        string filePath = ProjectPaths.RESOURCES_MAP_SETTINGS + Enum.GetName(typeof(MapTypes), _mapType) + ".txt";

        List<string> lines = new List<string>();
        using (StreamReader sr = new StreamReader(filePath))
        {
            do
            {
                string line = sr.ReadLine();
                if (line == "#")
                    break;
                lines.Add(line);

            } while (!sr.EndOfStream);            
        }

        for (int lineIndex = lines.Count -1; lineIndex >= 0; lineIndex--)
        {
            string line = lines[lineIndex];
            for (int coulumnIndex = 0; coulumnIndex < line.Length; coulumnIndex++)
            {
                char item = line[coulumnIndex];

                float z = lineIndex * cellSize;
                float x = coulumnIndex * cellSize;
                GameObject objectType;
                switch (item)
                {
                    case '1':
                        objectType = _obstacleTile;
                        break;
                    case '2':
                        objectType = _towerDefenseOne;
                        break;
                    case '3':
                        objectType = _towerDefenseTwo;
                        break;
                    default:
                        objectType = _pathTile;
                        break;
                }
                Instantiate(objectType, new Vector3(x, 0, z), Quaternion.identity);
            }            
        }
    }
}
