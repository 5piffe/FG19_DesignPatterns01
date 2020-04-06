using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using TowerDefense;

public class MapKeyData
{
    public MapKeyData(TileType type, GameObject prefab)
    {
        Type = type;
        Prefab = prefab;
    }

    public TileType Type { get; private set; }
    public GameObject Prefab { get; private set; }
}

public class MapReader
{
    private readonly Dictionary<TileType, GameObject> m_PrefabsById;
    public MapReader(IEnumerable<MapKeyData> mapKeyData)
    {
        m_PrefabsById = new Dictionary<TileType, GameObject>();
        foreach (MapKeyData data in mapKeyData)
        {
            m_PrefabsById.Add(data.Type, data.Prefab);
        }
    }

    public void ReadMap(Maps currentMap)
    {
        string filePath = "Assets/Resources/" + ProjectPaths.RESOURCES_MAP_SETTINGS + Enum.GetName(typeof(Maps), currentMap) + ".txt";
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
                float z = rowIndex * 2; // Fix cellSize option instead of hardcoding
                float x = columnIndex * 2;
                char currentTileChar = line[columnIndex];
                TileType tileType = TileMethods.TypeByIdChar[currentTileChar];
                GameObject currentPrefab = m_PrefabsById[tileType];
                GameObject.Instantiate(currentPrefab, new Vector3(x,0,z), Quaternion.identity);
            }
        }
    }
}