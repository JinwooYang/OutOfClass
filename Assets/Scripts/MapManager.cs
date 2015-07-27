using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum TileState { BEFORE_INQUIRY, OPENED, CLOSED, WALL };

class Tile : IComparable<Tile>
{
    public Tile parent;

    public TileState state;
    public int col, row;
    public int gCost;
    public int hCost;
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public Tile(int col, int row)
    {
        this.col = col;
        this.row = row;

        gCost = hCost = 0;
        state = TileState.BEFORE_INQUIRY;
    }

    public void Reset()
    {
        state = TileState.BEFORE_INQUIRY;
        gCost = hCost = 0;
        parent = null;
    }

    public bool IsNonWalkable()
    {
        return (state == TileState.CLOSED || state == TileState.WALL);
    }

    public int CompareTo(Tile tile)
    {
        return this.fCost - tile.fCost;
    }
}


public class MapManager : MonoBehaviour 
{
    public bool showGrid = true;

    public BoxCollider2D[] obstacles;

    const int TILE_SIZE = 21;
    const int MAP_IMAGE_SIZE = 2562;

    const int TILE_COL = MAP_IMAGE_SIZE / TILE_SIZE, TILE_ROW = MAP_IMAGE_SIZE / TILE_SIZE;
    Tile[,] tiles = new Tile[TILE_COL, TILE_ROW];

    const int DIAGONAL_G_COST = 14;
    const int NON_DIAGONAL_G_COST = 10;

    List<Tile> openTileList = new List<Tile>();

    public void SetTileState(int col, int row, TileState state)
    {
        tiles[col, row].state = state;
    }


    public void GetPath(Stack<Vector2> outStack, Vector2 startPos, Vector2 targetPos)
    {
        MapReset();

        int startCol = Mathf.Clamp((int)startPos.x / TILE_SIZE, 0, TILE_COL - 1);
        int startRow = Mathf.Clamp((int)startPos.y / TILE_SIZE, 0, TILE_ROW - 1);
        int targetCol = Mathf.Clamp((int)targetPos.x / TILE_SIZE, 0, TILE_COL - 1);
        int targetRow = Mathf.Clamp((int)targetPos.y / TILE_SIZE, 0, TILE_ROW - 1);

        Tile startTile = tiles[startCol, startRow];
        Tile targetTile = tiles[targetCol, targetRow];

        startTile.state = TileState.OPENED;
        openTileList.Add(startTile);

        while (openTileList.Count > 0)
        {
            Tile curTile = openTileList[0];

            if (curTile == targetTile)
            {
                for (Tile tile = curTile; tile != null; tile = tile.parent)
                {
                    Vector2 pos = (Vector2.right * tile.col + Vector2.up * tile.row) * TILE_SIZE;
                    outStack.Push(pos);
                }
                return;
            }   

            openTileList.Remove(curTile);
            curTile.state = TileState.CLOSED;

            SearchNeighborTile(curTile, targetTile);
        }
    }

    //인접한 타일을 검사하여 g,h,f값 계산, 부모 지정, 열린노드지정 처리
    void SearchNeighborTile(Tile stdTile, Tile targetTile)
    {
        int minCol = Mathf.Clamp(stdTile.col - 1, 0, TILE_COL - 1);
        int maxCol = Mathf.Clamp(stdTile.col + 1, 0, TILE_COL - 1);
        int minRow = Mathf.Clamp(stdTile.row - 1, 0, TILE_ROW - 1);
        int maxRow = Mathf.Clamp(stdTile.row + 1, 0, TILE_ROW - 1);

        //인접한 타일들을 검사한다.
        for (int col = minCol; col <= maxCol; ++col)
        {
            for (int row = minRow; row <= maxRow; ++row)
            {
                //인접한 타일을 현재 타일로 설정
                Tile curTile = tiles[col, row];

                //만약 현재 타일이 기준 타일과 같거나 이동 불가 타일이라면
                if ((col == stdTile.col && row == stdTile.row) || curTile.IsNonWalkable())
                {
                    //현재 타일 검사를 생략한다.
                    continue;
                }

                //임시로 기준 타일을 통해 이동했을 때의 g값을 계산한다.
                int tempGCost = stdTile.gCost;
                tempGCost += (stdTile.col - col != 0 && stdTile.row - row != 0) ? DIAGONAL_G_COST : NON_DIAGONAL_G_COST;

                //h값 계산을 위한 가로 길이 계산
                int widthDist;
                if(targetTile.col > curTile.col)
                {
                    widthDist = targetTile.col - curTile.col;
                }
                else
                {
                    widthDist = curTile.col - targetTile.col;
                }

                //h값 계산을 위한 세로 길이 계산
                int heightDist;
                if (targetTile.row > curTile.row)
                {
                    heightDist = targetTile.row - curTile.row;
                }
                else
                {
                    heightDist = curTile.row - targetTile.row;
                }


                int hCost = (widthDist + heightDist) * NON_DIAGONAL_G_COST;
                curTile.hCost = hCost;

                switch (curTile.state)
                {
                    //타일이 검사 전 상태라면 현재 타일을 오픈 리스트에 추가한 뒤, 현재 타일의 g값을 임시 g값으로 한다.
                    case TileState.BEFORE_INQUIRY:
                        openTileList.Add(curTile);
                        curTile.state = TileState.OPENED;
                        curTile.parent = stdTile;
                        curTile.gCost = tempGCost;

                        break;

                    //이미 열린 타일이라면 임시 g값과 기준 노드의 g값을 비교하여 임시 g값이 더 작으면 
                    //기준 노드를 통해 가는게 더 좋은 길이라는 의미이므로 현재 타일의 g값과 부모를 재설정한다.
                    case TileState.OPENED:
                        if (/*curTile.parent != stdTile ||*/ tempGCost < curTile.gCost)
                        {
                            curTile.gCost = tempGCost;
                            curTile.parent = stdTile;
                        }
                        break;
                }
            }
        }

        openTileList.Sort();
    }

    void MapInit()
    {
        //타일 전체를 탐색 전으로 초기화하고
        for (int tileIdx = 0; tileIdx < TILE_COL * TILE_ROW; ++tileIdx)
        {
            int col = tileIdx % TILE_COL;
            int row = tileIdx / TILE_COL;

            tiles[col, row] = new Tile(col, row);
        }

        //벽에 포함되는 타일들을 벽으로 설정한다.
        for (int obsIdx = 0; obsIdx < obstacles.Length; ++obsIdx)
        {
            Bounds bounds = obstacles[obsIdx].bounds;
            int minCol = Mathf.Clamp((int)bounds.min.x / TILE_SIZE, 0, TILE_COL - 1);
            int maxCol = Mathf.Clamp((int)bounds.max.x / TILE_SIZE, 0, TILE_COL - 1);
            int minRow = Mathf.Clamp((int)bounds.min.y / TILE_SIZE, 0, TILE_ROW - 1);
            int maxRow = Mathf.Clamp((int)bounds.max.y / TILE_SIZE, 0, TILE_ROW - 1);

            for (int col = minCol; col <= maxCol; ++col)
            {
                for (int row = minRow; row <= maxRow; ++row)
                {
                    tiles[col, row].state = TileState.WALL;
                }
            }
        }
    }

    void MapReset()
    {
        openTileList.Clear();

        //벽이 아닌 타일들을 모두 조사 전 상태로 되돌린다.
        for (int tileIdx = 0; tileIdx < TILE_COL * TILE_ROW; ++tileIdx)
        {
            int col = tileIdx % TILE_COL;
            int row = tileIdx / TILE_COL;

            if(tiles[col, row].state != TileState.WALL)
            {
                tiles[col, row].Reset();

            }
        }
    }

	void Awake () 
    {
        MapInit();
	}
	
	void OnDrawGizmos () 
    {
        if (!showGrid)
            return;

        for (int tileIdx = 0; tileIdx < TILE_COL * TILE_ROW; ++tileIdx)
        {
            int col = tileIdx % TILE_COL;
            int row = tileIdx / TILE_COL;

            if (tiles[col, row] != null)
            {
                Vector3 pos = (Vector3.right * col + Vector3.up * row) * TILE_SIZE;

                Gizmos.color = tiles[col, row].IsNonWalkable() ? Color.red : 
                    tiles[col, row].state == TileState.OPENED ? Color.green :
                                                                Color.white;

                Vector3 halfSize = Vector3.one * ((float)TILE_SIZE * 0.5f);

                Gizmos.DrawCube(pos + halfSize, halfSize);
            }
        }
    }
}
