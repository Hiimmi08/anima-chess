using System;
using TMPro;
using UnityEngine;

namespace Cells
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private CellType cellType;
        public CellType Type => cellType;

        [SerializeField] private TMP_Text idLabel;

        public static event Action<Cell> OnClickHandler;

        public int Id { get; private set; }
        public bool IsClickable { get; private set; }
        public Vector3 Position { get; private set; }

        public void Init(int id, int x, int z)
        {
            this.Id = id;
            idLabel.text = $"{id}";
            this.Position = new Vector3(x, 0f, z);
            this.IsClickable = false;
        }

        public void Highlight()
        {
            Debug.Log($"Highlighted {Id} ");
            IsClickable = true;
        }

        public void ResetHighlight()
        {
            IsClickable = false;
        }

        private void OnMouseDown()
        {
            Debug.Log($"OnMouseDown {Id} {IsClickable}");
            if (IsClickable)
            {
                OnClickHandler?.Invoke(this);
            }
        }

        public bool IsRiver()
            => cellType == CellType.River;
    }
}
