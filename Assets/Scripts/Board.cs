﻿using UnityEngine;
using System;
using System.Collections.Generic;

public class Board : MonoBehaviour
{
    public Camera cam;
    public float boardStep;
    public GameObject crossSprite;
    public GameObject zeroSprite;

    private float referenceAspect = 2f;
    private float scaleFactor;
    private Vector3 origin;

    private List<GameObject> signs = new List<GameObject>();

    void Start()
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

    public void PlaceSign(int playerIndex, int x, int y)
    {
        switch(playerIndex)
        {
            case 1:
                PlaceCross(x, y);
                break;
            case 2:
                PlaceZero(x, y);
                break;
            default:
                throw new ApplicationException("Impossible playerIndex");
                
        }
    }

    public void ClearBoard()
    {
        foreach (var sign in signs)
        {
            Destroy(sign);
        }

        signs.Clear();
    }

    private void PlaceCross(int x, int y)
    {
        Instantiate(crossSprite, GetPlacePosition(x, y), Quaternion.identity, transform);
    }

    private void PlaceZero(int x, int y)
    {
        Instantiate(zeroSprite, GetPlacePosition(x, y), Quaternion.identity, transform);
    }

    private Vector3 GetPlacePosition(int x, int y)
    {
        return new Vector3(origin.x + x * boardStep, origin.y + y * boardStep);
    }

    private void OnMouseDown()
    {
        Vector3 clickPos = cam.ScreenToWorldPoint(Input.mousePosition);

        Coordinates clickCellCoordinates = GetCoordinatesByPosition(clickPos);

        Game.Instance.TryPlacePlayerSign(clickCellCoordinates);
    }

    Coordinates GetCoordinatesByPosition(Vector3 position)
    {
        int x = Mathf.RoundToInt((position.x - origin.x) / boardStep);
        int y = Mathf.RoundToInt((position.y - origin.y) / boardStep);

        return new Coordinates(x, y);
    }
}
