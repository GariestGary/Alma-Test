using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ImageSlicer : MonoBehaviour
{
    [SerializeField] private GameObject piecePrefab;
    [SerializeField] private List<DifficultyPattern> patterns;
    [SerializeField] private Transform piecesRoot;

    public List<Piece> SliceImage(Sprite sprite, Difficulty difficulty)
    {
        DifficultyPattern pattern = patterns.First(p => p.difficulty == difficulty);
        List<Piece> pieces = new List<Piece>();

        Vector2 resolution = GameManager.GetResolution(difficulty);

        for (int i = 0; i < resolution.x; i++)
        {
            for (int j = 0; j < resolution.y; j++)
            {
                Piece piece = Instantiate(piecePrefab, Vector3.zero, Quaternion.identity, piecesRoot).GetComponent<Piece>();
                
                piece.Initialize(sprite, pattern.piecesPattern, new Vector2(i, j), resolution);
                
                pieces.Add(piece);
            }
        }

        return pieces;
    }
}

[System.Serializable]
public class DifficultyPattern
{
    public Difficulty difficulty;
    public Sprite piecesPattern;
}
