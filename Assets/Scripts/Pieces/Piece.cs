using Cells;
using UnityEngine;

namespace Pieces
{
    public class Piece : MonoBehaviour
    {
        [SerializeField] private Rank baseRank;
        [SerializeField] private PieceAnimationController animationController;

        public int Id { get; private set; }
        public Rank BaseRank => baseRank;
        public Rank CurrentRank { get; private set; }
        public PieceStatus Status { get; private set; }
        public PieceDirection Direction { get; private set; }
        public int PlayerId { get; private set; }

        public void Init(PieceDirection direction, int playerId)
        {
            this.Direction = direction;
            this.PlayerId = playerId;
            OnSetDirection();

            CurrentRank = BaseRank;
            Status = PieceStatus.Alive;

            animationController.Init();
        }

        // Start showing animation for being clicked
        public void SetOnClick()
        {
            animationController.OnClick();
        }

        public void ResetOnClick()
        {
            animationController.Idle();
        }

        public void OnMoveOutOfTrapCell()
        {
            CurrentRank = BaseRank;
        }

        public void MoveToCell(Cell destination, float spawnY)
        {
            if (destination == null || !IsMovable())
            {
                return;
            }

            Debug.Log($"Moving to cell: {destination.Id}");
            transform.position = destination.Position + new Vector3(0f, spawnY, 0f);
            // tat anim
        }

        public bool IsMovable()
        {
            return Status == PieceStatus.Alive;
        }

        private void OnSetDirection()
        {
            Vector3 rotation = Direction switch
            {
                PieceDirection.North => new Vector3(0f, 180f, 0f),
                PieceDirection.South => Vector3.zero,
                _ => Vector3.zero,
            };

            transform.eulerAngles = rotation;
        }
    }
}
