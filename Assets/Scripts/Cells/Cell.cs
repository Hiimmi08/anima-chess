using TMPro;
using UnityEngine;

namespace Cells
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private CellType cellType;
        public CellType Type => cellType;

        [SerializeField] private TMP_Text idLabel;

        public int Id { get; set; }

        public void Init(int id)
        {
            this.Id = id;
            idLabel.text = $"{id}";
        }
    }
}
