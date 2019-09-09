using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Board : MonoBehaviour, IPointerClickHandler
{
    public Camera cam;
    public float referenceAspect = 2f;
    public float boardStep;
    public GameObject[] signsPrefabs;
    public GameObject crossSprite;
    public GameObject zeroSprite;
    
    private float scaleFactor;
    private Vector3 origin;

    private List<GameObject> signs = new List<GameObject>();

    private void Awake()
    {
        Game.Instance.Board = this;
        scaleFactor = referenceAspect / (Screen.height / (float)Screen.width);
        transform.localScale = transform.localScale * scaleFactor;
        boardStep *= scaleFactor;
        origin = transform.position - new Vector3(boardStep, boardStep);        
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

    private Coordinates GetCoordinatesByPosition(Vector3 position)
    {
        int x = Mathf.RoundToInt((position.x - origin.x) / boardStep);
        int y = Mathf.RoundToInt((position.y - origin.y) / boardStep);

        return new Coordinates(x, y);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 clickPos = eventData.pointerPressRaycast.worldPosition;

        Coordinates clickCellCoordinates = GetCoordinatesByPosition(clickPos);

        Game.Instance.TryPlacePlayerSign(clickCellCoordinates);        
    }
}
