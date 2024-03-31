using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private CellType cellType;
    public CellType Type => cellType;

    public int Id { get; set; }

    public void Init(int id)
    {
        this.Id = id;
    }
}
