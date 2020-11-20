﻿using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Color exploredColor;

    // public ok here as is a data class
    public bool isExplored = false;
    public bool isPlaceable = true;
    public Waypoint exploredFrom;

    const int gridSize = 10;

    public int GetGridSize()
    {
        return gridSize;
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
        );
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isPlaceable)
            {
                var towerFactory = FindObjectOfType<TowerFactory>();
                towerFactory.AddTower(this);
            } else
            {
                print("Can't place here");
            }
        }
    }
}
