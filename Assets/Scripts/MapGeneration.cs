using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TowerDefense;
using UnityEngine;

// Enum with different maps
public enum Maps
{
    map_1,
    map_2,
    map_3,
    spiffe_maptest

    // Named this way because setting string to this plus .txt named like in folder
    // TODO: Fixa det på eget vis
}

public class MapGeneration : MonoBehaviour
{
    [SerializeField] private Maps _mapType = Maps.map_1; //Kolla naming ska den heta _map eller Map eller va fan
    [SerializeField] private GameObject pathTile = null;
    [SerializeField] private GameObject obstacleTile = null;
    [SerializeField] private GameObject towerBomb = null;
    [SerializeField] private GameObject towerFreeze = null;

    [SerializeField] private int cellSize = 2;

   // private GameObject tileToSpawn;

    //private GameObject spawnTile = null;
    //private char test = '0';

    //private int rows = 4;
    //private int columns = 6;

    private void Awake()
    {
        // fix Assertions 
        GenerateMap();
        //TileType tileType = TileMethods.TypeByIdChar[currentTile];
        //Debug.Log(currentTile);
        // Loada in textfilen till en textasset
        //var mapTextFile = Resources.Load<TextAsset>("MapSettings/map_3.txt");
    }

    private void GenerateMap()
    {
        // TODO: Kör Resources.Load på nåt vis här istället
        string filePath = ProjectPaths.RESOURCES_MAP_SETTINGS + Enum.GetName(typeof(Maps), _mapType) + ".txt";

        // TODO: Kör queue eller hashset som list funkar inte det?
        List<string> lines = new List<string>();

        // TODO: Kolla vad StreamReader är (den som använder system.io)
        // Borde gå med nån //var mapTextFile = Resources.Load<TextAsset>("MapSettings/spiffe_maptest.txt"); grej istället
        using (StreamReader sr = new StreamReader(filePath))
        {
            do
            {
                string line = sr.ReadLine();

                if (line == "#") //TODO: spiff !=# och ?:
                {
                    break;
                }

                lines.Add(line);

            } while (!sr.EndOfStream);
        }

        for (int lineIndex = lines.Count - 1; lineIndex >= 0; lineIndex--)
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
                        objectType = obstacleTile;
                        break;
                    case '2':
                        objectType = towerBomb;
                        break;
                    case '3':
                        objectType = towerFreeze;
                        break;
                    default:
                        objectType = pathTile;
                        break;
                }
                Instantiate(objectType, new Vector3(x, 0, z), Quaternion.identity);
            }
        }


        //if (test == '0')
        //{
        //    spawnTile = pathTile;
        //}
        //if (test == '1')
        //{
        //    spawnTile = obstacleTile;
        //}

        //for (int row = 0; row < rows; row++)
        //{
        //    for (int col = 0; col < columns; col++)
        //    {
        //        GameObject.Instantiate(spawnTile);
        //    }
        //}
    }
}