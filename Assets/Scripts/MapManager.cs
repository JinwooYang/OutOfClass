using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TileState { BEFORE_INQUIRY, OPENED, CLOSED, WALL };

class Tile
{
    public Tile parent;

    public TileState state = TileState.BEFORE_INQUIRY;
    public int gCost = 0;
    public int hCost = 0;
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public void Reset()
    {
        state = TileState.BEFORE_INQUIRY;
        gCost = hCost = 0;
    }

    public bool IsNonWalkable()
    {
        return (state == TileState.CLOSED || state == TileState.WALL);
    }
}


public class MapManager : MonoBehaviour 
{
    public BoxCollider2D[] walls;
    
    const int TILE_COL = 2547, TILE_ROW = 2562;
    Tile[,] tiles = new Tile[TILE_COL, TILE_ROW];

    //const int DIAGONAL_G_COST = 14;
    //const int NON_DIAGONAL_G_COST = 10;

    //List<Tile> openTileList = new List<Tile>();

    //public void SetTileState(int col, int row, TileState state)
    //{
    //    tiles[col, row].state = state;
    //}

    //public void SetTileState(Vector2 pos, TileState state)
    //{
    //    tiles[(int)pos.x, (int)pos.y].state = state;
    //}

    //public void GetPath(Stack<Vector2> outStack, Vector2 startPos, Vector2 targetPos)
    //{
    //    MapReset();

    //    Tile selectedTile = GetTileFromPos(startPos);
    //    selectedTile.state = TileState.CLOSED;

    //    //working...
    //}

    //Tile GetTileFromPos(Vector2 pos)
    //{
    //    return tiles[(int)pos.x, (int)pos.y];
    //}

    //void SearchNeighborTile(Stack<Vector2> outStack, int stdCol, int stdRow, int targetCol, int targetRow)
    //{
    //    Tile stdTile = tiles[stdCol, stdRow];
    //    Tile targetTile = tiles[targetCol, targetRow];

    //    int minCol = Mathf.Clamp(stdCol - 1, 0, TILE_COL - 1);
    //    int maxCol = Mathf.Clamp(stdCol + 1, 0, TILE_COL - 1);
    //    int minRow = Mathf.Clamp(stdRow - 1, 0, TILE_ROW - 1);
    //    int maxRow = Mathf.Clamp(stdRow + 1, 0, TILE_ROW - 1);

    //    for (int tileIdx = minCol * minRow; tileIdx <= maxCol * maxRow; ++tileIdx)
    //    {
    //        int col = tileIdx % TILE_COL;
    //        int row = tileIdx / TILE_COL;

    //        Tile curTile = tiles[col, row];

    //        if((col == stdCol && row == stdRow) ||
    //            curTile.IsNonWalkable())
    //        {
    //            continue;
    //        }

    //        int tempGCost = stdTile.gCost;
    //        tempGCost += (stdCol - col != 0 && stdRow - row != 0) ? DIAGONAL_G_COST : NON_DIAGONAL_G_COST;

    //        switch (curTile.state)
    //        {
    //            case TileState.BEFORE_INQUIRY:
    //                openTileList.Add(curTile);
    //                curTile.parent = stdTile;
    //                curTile.gCost = tempGCost;
    //                break;

    //            case TileState.OPENED:
    //                if(tempGCost < curTile.gCost)
    //                {
    //                    curTile.gCost = tempGCost;
    //                    curTile.parent = stdTile;
    //                }
    //                break;
    //        }

    //        if (curTile == targetTile)
    //        {
    //            for (Tile tile = targetTile; tile != null; tile = tile.parent)
    //            {
    //                //working...
    //            }
    //        }
    //    }
    //}

    void MapInit()
    {
        //타일 전체를 탐색 전으로 초기화하고
        for (int tileIdx = 0; tileIdx < TILE_COL * TILE_ROW; ++tileIdx)
        {
            int col = tileIdx % TILE_COL;
            int row = tileIdx / TILE_COL;

            tiles[col, row].Reset();
        }

        //벽에 포함되는 타일들을 벽으로 설정한다.
        for (int wallIdx = 0; wallIdx < walls.Length; ++wallIdx)
        {
            Bounds bounds = walls[wallIdx].bounds;
            int minX = Mathf.Clamp((int)bounds.min.x, 0, TILE_COL - 1);
            int maxX = Mathf.Clamp((int)bounds.max.x, 0, TILE_COL - 1);
            int minY = Mathf.Clamp((int)bounds.min.y, 0, TILE_ROW - 1);
            int maxY = Mathf.Clamp((int)bounds.max.y, 0, TILE_ROW - 1);

            for (int col = minX; col <= maxX; ++col)
            {
                for (int row = minY; row <= maxY; ++row)
                {
                    tiles[col, row].state = TileState.WALL;
                }
            }
        }
    }

    void MapReset()
    {
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
	
	void Update () 
    {
	
	}
}
