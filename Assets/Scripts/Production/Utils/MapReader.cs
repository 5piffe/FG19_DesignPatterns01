using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public void ReadMap()
    {
        //TODO: Use this and tilemethods for mapgeneration
        char currentTileChar = '1';
        TileType tileType = TileMethods.TypeByIdChar[currentTileChar];
        GameObject currentPrefab = m_PrefabsById[tileType];
        GameObject.Instantiate(currentPrefab);
    }
}
