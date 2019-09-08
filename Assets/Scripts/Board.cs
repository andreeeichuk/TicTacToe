using UnityEngine;
using System.Collections.Generic;

public class Board : MonoBehaviour
{
    public Camera cam;
    public float boardStep;
    public GameObject[] signsPrefabs;
    public GameObject crossSprite;
    public GameObject zeroSprite;

    private float referenceAspect = 2f;
    private float scaleFactor;
    private Vector3 origin;

    private List<GameObject> signs = new List<GameObject>();

    private void OnEnable()
    {
        Game.Instance.board = this;
        scaleFactor = referenceAspect / (Screen.height / (float)Screen.width);
        transform.localScale = transform.localScale * scaleFactor;
        origin = transform.position - new Vector3(boardStep, boardStep);
        boardStep *= scaleFactor;
    }    

    [ContextMenu("PlaceCross")]
    public void PlaceCrossAtOrigin()
    {
        Instantiate(crossSprite, origin, Quaternion.identity, transform);
    }

    public void PlaceSign(int signIndex, int x, int y)
    {
        GameObject sign = Instantiate(signsPrefabs[signIndex-1], GetPlacePosition(x, y), Quaternion.identity, transform);
        signs.Add(sign);
    }

    public void ClearBoard()
    {
        foreach (var sign in signs)
        {
            Destroy(sign);
        }

        signs.Clear();
    }   

    private Vector3 GetPlacePosition(int x, int y)
    {
        return new Vector3(origin.x + x * boardStep, origin.y + y * boardStep);
    }

    private void OnMouseDown()
    {
        Vector3 clickPos = cam.ScreenToWorldPoint(Input.mousePosition);

        Coordinates clickCellCoordinates = GetCoordinatesByPosition(clickPos);

        Debug.Log($"Mouse coord: {clickCellCoordinates.x},{clickCellCoordinates.y}");

        Game.Instance.TryPlacePlayerSign(clickCellCoordinates);
    }

    Coordinates GetCoordinatesByPosition(Vector3 position)
    {
        int x = Mathf.RoundToInt((position.x - origin.x) / boardStep);
        int y = Mathf.RoundToInt((position.y - origin.y) / boardStep);

        return new Coordinates(x, y);
    }
}
