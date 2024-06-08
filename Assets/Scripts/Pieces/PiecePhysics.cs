using System;
using UnityEngine;

public class PiecePhysics : MonoBehaviour
{
    public event Action OnClickHandler;

    private void OnMouseDown()
    {
        OnClickHandler?.Invoke();
    }
}

