﻿using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint, endWaypoint;

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    bool isRunning = true;

    Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    // Start is called before the first frame update
    void Start()
    {
        LoadBlocks();
        ColorStartAndEnd();
        Pathfind();
    }

    private void Pathfind()
    {
        queue.Enqueue(startWaypoint);
        while (queue.Count > 0 && isRunning)
        {
            var searchCenter = queue.Dequeue();
            print("Searching from: " + searchCenter);
            HaltIfEndFound(searchCenter);
            ExploreNeighbours(searchCenter);
            searchCenter.isExplored = true;
        }

        print("Finished pathfinding");
    }

    private void HaltIfEndFound(Waypoint searchCenter)
    {
        if (searchCenter == endWaypoint)
        {
            print("Searching from end node, stopping");
            isRunning = false;
        }
    }

    private void ExploreNeighbours(Waypoint from)
    {
        if (!isRunning) { return; }

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinates = from.GetGridPos() + direction;
            try
            {
                QueueNewNeighbours(neighbourCoordinates);
            }
            catch
            {
                // do nothing
            }
        }
    }

    private void QueueNewNeighbours(Vector2Int neighbourCoordinates)
    {
        Waypoint neighbour = grid[neighbourCoordinates];
        if (!neighbour.isExplored)
        {
            neighbour.SetTopColor(Color.blue);
            queue.Enqueue(neighbour);
            print("Queueing " + neighbour);
        }
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            Vector2Int gridPos = waypoint.GetGridPos();
            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Skipping overlapping block: " + waypoint);
            }
            else
            {
                grid.Add(waypoint.GetGridPos(), waypoint);
            }
        }
        print("Loaded " + grid.Count + " blocks");
    }

    private void ColorStartAndEnd()
    {
        startWaypoint.SetTopColor(Color.green);
        endWaypoint.SetTopColor(Color.red);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
