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
    // TODO: Fixa det på
}

public class MapGeneration : MonoBehaviour
{
    [SerializeField] private Maps map = Maps.map_1; //Kolla naming ska den heta _map eller Map eller va fan
    [SerializeField] private GameObject pathTile = null;
    [SerializeField] private GameObject obstacleTile = null;
    [SerializeField] private GameObject towerBombTile = null;
    [SerializeField] private GameObject towerFreezeTile = null;
    [SerializeField] private GameObject startTile = null;
    [SerializeField] private GameObject endTile = null;

    [SerializeField] private int cellSize = 2;

    private void Awake()
    {
        //Assertions?
        GenerateMap();
        //TileType tileType = TileMethods.TypeByIdChar[currentTile];
        //var mapTextFile = Resources.Load<TextAsset>("MapSettings/map_3.txt");
    }

    private void GenerateMap()
    {
        // TODO: Kör Resources.Load på nåt vis här istället
        string filePath = "Assets/Resources/" + ProjectPaths.RESOURCES_MAP_SETTINGS + Enum.GetName(typeof(Maps), map) + ".txt";
        
        // TODO: Kör queue eller hashset som list funkar inte det?
        List<string> lines = new List<string>();

        // Testar med textasset utan streamreader
        var mapText = Resources.Load<TextAsset>(Enum.GetName(typeof(Maps), map));
        //string filePathtest = mapText.text;

        // string[] linesFromfile = new string (mapText.text.Split(System.Environment.NewLine[0]));

        // TODO: Don't use streamreader, fix something with unitys textasset or so
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

        for (int lineIndex = lines.Count - 1, rowIndex = 0; lineIndex >= 0; lineIndex--, rowIndex++)
        {
            string line = lines[lineIndex];
            for (int columnIndex = 0; columnIndex < line.Length; columnIndex++)
            {
                char item = line[columnIndex];

                float z = rowIndex * cellSize;
                float x = columnIndex * cellSize;
                // TODO: Get this outside of the loop
                GameObject tileToSpawn;

                if (item == '1') { tileToSpawn = obstacleTile; }
                // Hashlist?
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
                        tileToSpawn = pathTile; // If there's unknown char it will default to path-tile
                        break;
                }
                Instantiate(tileToSpawn, new Vector3(x, 0, z), Quaternion.identity);
            }
        }
    }
}