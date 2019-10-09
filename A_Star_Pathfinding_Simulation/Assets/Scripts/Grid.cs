﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    // For creating the Grid
    [SerializeField] private Vector3 _gridWorldSize;
    [SerializeField] private LayerMask _unWalkableMasks;
    [SerializeField] private float _nodeRadius;

    private Node[,] _grid;

    // For knowing how many grid can be placed in the _gridWorldSize;
    private float _nodeDiameter;
    private int _gridSizeX, _gridSizeY;



    private void Start()
    {
        _nodeDiameter = _nodeRadius * 2;
        _gridSizeX = Mathf.RoundToInt(_gridWorldSize.x / _nodeDiameter);     // To know how many grid we can place in X
        _gridSizeY = Mathf.RoundToInt(_gridWorldSize.y / _nodeDiameter);

        CreateGrid();
    }


    private void CreateGrid()
    {
        _grid = new Node[_gridSizeX, _gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * _gridWorldSize.x / 2 - Vector3.forward * _gridWorldSize.y / 2;   // To find the starting point to  create node. here bottom left

        for(int x = 0; x < _gridSizeX; x++)
        {
            for(int y = 0; y < _gridSizeY; y ++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * _nodeDiameter + _nodeRadius) + Vector3.forward * (y * _nodeDiameter + _nodeRadius);
                bool isWalkable = !(Physics.CheckSphere(worldPoint, _nodeRadius, _unWalkableMasks));
                _grid[x, y] = new Node(isWalkable, worldPoint);
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(_gridWorldSize.x, 1f, _gridWorldSize.z));       // Drawing the Gridworld size

        if (_grid != null)
        {
            foreach (Node n in _grid)
            {
                Gizmos.color = (n.isWalkable) ? Color.white : Color.red;
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (_nodeDiameter - .1f));
            }
        }
    }

}