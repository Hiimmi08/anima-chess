using TMPro;
using UnityEngine;

namespace Pieces
{
    public class Piece : MonoBehaviour
    {
        [SerializeField] private Rank baseRank;
        [SerializeField] private TMP_Text nameLabel;

        public int Id { get; private set; }
        public Rank BaseRank => baseRank;
        public Rank CurrentRank { get; private set; }
        public int CurrentCell { get; private set; }

        public void Init(int id)
        {
            this.Id = id;
            CurrentRank = BaseRank;
            nameLabel.text = BaseRank.GetPieceName();
        }

        public void OnMoveToTrapCell()
        {
            CurrentRank = Rank.Default;
        }

        public void OnMoveOutOfTrapCell()
        {
            CurrentRank = BaseRank;
        }
    }
}
