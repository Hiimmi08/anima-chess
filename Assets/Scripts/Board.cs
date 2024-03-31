using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    
    [SerializeField] private Cell cellNormal;
    [SerializeField] private Cell cellTrap;
    [SerializeField] private Cell cellCave;
    [SerializeField] private Cell cellRiver;

    private static readonly int[] CaveIds = { 27, 35 };
    private static readonly int[] RiverIds = { 12, 13, 14, 21, 22, 23, 39, 40, 41, 48, 49, 50 };
    private static readonly int[] TrapIds = { 18, 28, 36, 26, 34, 44 };

    private const int BoardWidth = 7;
    private const int BoardLength = 9;


    private void Init()
    {
        var id = 0;
        for (var i = 0; i < BoardWidth; i++)
        {
            for (var j = 0; j < BoardLength; j++)
            {
                var cellPrefab = GetCell(id);
                var cell = Instantiate(cellPrefab, this.transform);
                cell.transform.position = new Vector3(i, 0f, j);
                cell.Init(id);
                id++;
            }
        }
    }

    private Cell GetCell(int id)
    {
        // Check if cell is Cave
        if (CaveIds.Contains(id))
        {
            return cellCave;
        }

        // Check if cell is River
        if (RiverIds.Contains(id))
        {
            return cellRiver;
        }

        // Check if cell is Trap
        if (TrapIds.Contains(id))
        {
            return cellTrap;
        }

        // return Normal
        return cellNormal;
    }

    private void Start()
    {
        Init();
    }


}
