using System;
using System.Collections.Generic;
using UnityEngine;
public class InventoryMap
{
    [SerializeField]
    private List<SlotRow> _map = new List<SlotRow>();
    [SerializeField]
    private int _rows;
    [SerializeField]
    private int _columns;

    public InventoryMap(Vector2Int map, List<ISlot> slots)
    {
        _rows = map.y;
        _columns = map.x;
        int itemCount = slots.Count;
        int availableSlots = slots.Count;

        for (int rowCount = 0; rowCount < _rows; rowCount++)
        {
            int itensInRow = availableSlots;
            if (itensInRow >= _columns)
            {
                itensInRow = _columns;
                availableSlots -= _columns;
            }

            List<ISlot> rowColumns = slots.GetRange((rowCount * _columns), itensInRow);
            _map.Add(new SlotRow(rowColumns));
        }
    }

    public SlotRow GetCollumn(int index)
    {
        return _map[index];
    }
    public ISlot GetSlot(Vector2Int position, UIMovementEnum movementEnum)
    {
        ISlot slot = null;
        int yPos = position.y;
        int xPos = position.x;
        if (ItemExists(yPos, xPos))
        {
            slot = _map[yPos]._slots[xPos];
        }
        else if (ItemExists(yPos, 0) && movementEnum.IsHorizontalMovement())
        {
            // Row exists, checking where to send the column pointer
            SlotRow row = _map[yPos];
            if (xPos >= row._slots.Count)
            {
                if (ItemExists(yPos, 0))
                {
                    slot = row._slots[0];
                }
            }
            else if (xPos < 0)
            {
                if (ItemExists(yPos, row._slots.Count - 1))
                {
                    slot = row._slots[row._slots.Count - 1];
                }
            }
        }
        else if (movementEnum.IsVerticalMovement())
        {
            // Row didn't exists, cheking if column in the first row exists
            if (ItemExists(0, xPos))
            {
                if (yPos >= _rows - 1)
                {
                    slot = _map[0]._slots[xPos];
                }
                else if (yPos < 0)
                {
                    for(int row = _rows - 1; row >= 0; row--){
                        if(ItemExists(row, xPos)){
                            slot = _map[row]._slots[xPos];
                            break;
                        }
                    }
                }
                
            }
        }
        return slot;
    }
    private bool ItemExists(int row, int collumn)
    {
        try
        {
            SlotRow possibleRow = _map[row];
            if (possibleRow != null)
            {
                if (possibleRow._slots[collumn] != null)
                {
                    return true;
                }
            }
        }
        catch (Exception e) { }
        return false;
    }
    public Vector2Int FindSlotPosition(ISlot slot)
    {
        Vector2Int pos = new Vector2Int(0, 0);
        for (int rIndex = 0; rIndex < _map.Count; rIndex++)
        {
            List<ISlot> cSlots = _map[rIndex]._slots;
            if (cSlots.Contains(slot))
            {
                pos = new Vector2Int(rIndex, cSlots.IndexOf(slot));
            }
        }
        return pos;
    }
}