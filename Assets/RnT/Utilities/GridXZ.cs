﻿using System;
using UnityEngine;

namespace RnT.Utilities {
    public class GridXZ<TGridObject> {

        public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
        public class OnGridObjectChangedEventArgs : EventArgs {
            public int x;
            public int z;
        }

        private int width;
        private int height;
        private float cellSize;
        private Vector3 originPosition;
        private TGridObject[,] gridArray;

        public GridXZ(int width, int height, float cellSize, Vector3 originPosition, Func<GridXZ<TGridObject>, int, int, TGridObject> createGridObject) {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;

            gridArray = new TGridObject[width, height];

            for(int x = 0; x < gridArray.GetLength(0); x++) {
                for(int z = 0; z < gridArray.GetLength(1); z++) {
                    gridArray[x, z] = createGridObject(this, x, z);
                }
            }

            bool showDebug = true;
            if(showDebug) {
                TextMesh[,] debugTextArray = new TextMesh[width, height];

                for(int x = 0; x < gridArray.GetLength(0); x++) {
                    for(int z = 0; z < gridArray.GetLength(1); z++) {
                        Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z + 1), Color.white, 100f);
                        Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z), Color.white, 100f);
                    }
                }
                Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

                OnGridObjectChanged += (sender, eventArgs) => {
                    debugTextArray[eventArgs.x, eventArgs.z].text = gridArray[eventArgs.x, eventArgs.z]?.ToString();
                };
            }
        }

        public int GetWidth() {
            return width;
        }

        public int GetHeight() {
            return height;
        }

        public float GetCellSize() {
            return cellSize;
        }

        public Vector3 GetWorldPosition(int x, int z) {
            return new Vector3(x, 0, z) * cellSize + originPosition;
        }

        public Vector3 GetWorldPositionCellCenter(int x, int z) {
            var value = cellSize / 2;
            var centerPosition = new Vector3(x, 0, z) * cellSize + originPosition; ;
            return new Vector3(centerPosition.x + value, 0, centerPosition.z + value);
        }

        public Vector3 GetWorldPositionCellCenter(Vector3 worldPosition) {
            GetXZ(worldPosition, out int x, out int z);
            var value = cellSize / 2;
            var centerPosition = new Vector3(x, 0, z) * cellSize + originPosition; ;
            return new Vector3(centerPosition.x + value, 0, centerPosition.z + value);
        }

        public void GetXZ(Vector3 worldPosition, out int x, out int z) {
            x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
            z = Mathf.FloorToInt((worldPosition - originPosition).z / cellSize);
        }

        public void SetGridObject(int x, int z, TGridObject value) {
            if(x >= 0 && z >= 0 && x < width && z < height) {
                gridArray[x, z] = value;
                TriggerGridObjectChanged(x, z);
            }
        }

        public void TriggerGridObjectChanged(int x, int z) {
            OnGridObjectChanged?.Invoke(this, new OnGridObjectChangedEventArgs { x = x, z = z });
        }

        public void SetGridObject(Vector3 worldPosition, TGridObject value) {
            GetXZ(worldPosition, out int x, out int z);
            SetGridObject(x, z, value);
        }

        public TGridObject GetGridObject(int x, int z) {
            if(x >= 0 && z >= 0 && x < width && z < height) {
                return gridArray[x, z];
            } else {
                return default;
            }
        }

        public TGridObject GetGridObject(Vector3 worldPosition) {
            int x, z;
            GetXZ(worldPosition, out x, out z);
            return GetGridObject(x, z);
        }

        public Vector2Int ValidateGridPosition(Vector2Int gridPosition) {
            return new Vector2Int(
                Mathf.Clamp(gridPosition.x, 0, width - 1),
                Mathf.Clamp(gridPosition.y, 0, height - 1)
            );
        }
    }
}