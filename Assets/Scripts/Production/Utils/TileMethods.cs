using System.Collections.Generic;
using UnityEngine;

public static class TileMethods
{
    //public static IReadOnlyDictionary<int, TileType> TypeById { get; } = new Dictionary<int, TileType>
    //{
    //    { 0,  TileType.Path },
    //    { 1,  TileType.Obstacle },
    //    { 2,  TileType.TowerOne },
    //    { 3,  TileType.TowerTwo},
    //    { 8,  TileType.Start },
    //    { 9,  TileType.End },
    //};

    public static IReadOnlyDictionary<char, TileType> TypeByIdChar { get; } = new Dictionary<char, TileType>
    {
        { '0',  TileType.Path },
        { '1',  TileType.Obstacle },
        { '2',  TileType.TowerOne },
        { '3',  TileType.TowerTwo},
        { '8',  TileType.Start },
        { '9',  TileType.End },
    };

    public class ObjectConverter
    {
        private readonly GameObject m_Prefab;

        public ObjectConverter(GameObject prefab)
        {
            m_Prefab = prefab;
        }

        public GameObject GetPrefab()
        {
            return m_Prefab;
        }

    }

    public static bool IsWalkable(TileType type)
    {
        return type == TileType.Path || type == TileType.Start || type == TileType.End;
    }
}