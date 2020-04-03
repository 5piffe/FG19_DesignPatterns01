using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TowerDefense;
using UnityEngine;

// TODO: Separate stuff. Make use of the mapreader, TileMethods etc.

public enum Maps
{
    map_1,
    map_2,
    map_3,
    spiffe_maptest
    // TODO: Fixa nåt annat.
}

public class MapGeneration : MonoBehaviour
{
    private GameObject enemy = null;

    [SerializeField] private Maps map = Maps.map_1;
    [SerializeField] private GameObject pathTile = null;
    [SerializeField] private GameObject obstacleTile = null;
    [SerializeField] private GameObject towerBombTile = null;
    [SerializeField] private GameObject towerFreezeTile = null;
    [SerializeField] private GameObject startTile = null;
    [SerializeField] private GameObject endTile = null;

    [SerializeField] private int cellSize = 2; // TODO: Serializing just for fun testing stuff.

    private void Awake()
    {
        //Assertions?
        
        GenerateMap();

    }

    private void GenerateMap()
    {
        string filePath = "Assets/Resources/" + ProjectPaths.RESOURCES_MAP_SETTINGS + Enum.GetName(typeof(Maps), map) + ".txt";
        List<string> lines = new List<string>();

        using (StreamReader sr = new StreamReader(filePath))
        {
            do
            {
                string line = sr.ReadLine();

                if (line == "#")
                {
                    break;
                }

                lines.Add(line);

            } while (!sr.EndOfStream);
        }

        for (int lineIndex = lines.Count - 1, rowIndex = 0; lineIndex >= 0; lineIndex--, rowIndex++)
        {
            string line = lines[lineIndex];
            for (int columnIndex = 0; columnIndex < line.Length; columnIndex++)
            {
                char item = line[columnIndex];

                float z = rowIndex * cellSize;
                float x = columnIndex * cellSize;
                GameObject tileToSpawn;

                if (item == '1') { tileToSpawn = obstacleTile; }
                // TODO: Make use of TileMethods etc instead.
                switch (item)
                {
                    case '1':
                        tileToSpawn = obstacleTile;
                        break;
                    case '2':
                        tileToSpawn = towerBombTile;
                        break;
                    case '3':
                        tileToSpawn = towerFreezeTile;
                        break;
                    case '8':
                        tileToSpawn = startTile;
                        break;
                    case '9':
                        tileToSpawn = endTile;
                        break;
                    default:
                        tileToSpawn = pathTile;
                        break;
                }
                Instantiate(tileToSpawn, new Vector3(x, 0, z), Quaternion.identity);
            }
        }
    }
}