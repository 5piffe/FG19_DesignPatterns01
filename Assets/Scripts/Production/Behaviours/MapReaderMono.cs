﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Maps
{
    map_1,
    map_2,
    map_3,
    spiffe_maptest
}

public class MapReaderMono : MonoBehaviour
{
    [SerializeField] private Maps map = Maps.map_1;
    [SerializeField] private TileType[] m_Types;
    [SerializeField] private GameObject[] m_Prefabs;

    private MapReader m_MapReader;
    private void Awake()
    {
        List<MapKeyData> data = new List<MapKeyData>();
        int i = 0;
        foreach (TileType tileType in m_Types)
        {
            data.Add(new MapKeyData(tileType, m_Prefabs[i]));
            i++;
        }

        m_MapReader = new MapReader(mapKeyData: data);
        m_MapReader.ReadMap(map);
    }
}