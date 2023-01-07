using System;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Slots;
namespace Inventory
{
    [Serializable]
    public class InventoryMap
    {
        [SerializeField]
        private List<SlotRow> _map;
        [SerializeField]
        private int _rows;
        [SerializeField]
        private int _columns;

        public InventoryMap(Vector2Int map, List<ISlot> slots)
        {
            _rows = map.y;
            _columns = map.x;
            LoadMap(_rows, _columns, slots);
        }
        private void LoadMap(int rows, int columns, List<ISlot> slots)
        {
            int itemCount = slots.Count;
            int availableSlots = slots.Count;
            if (_map == null)
            {
                _map = new List<SlotRow>();
            }

            for (int rowCount = 0; rowCount < rows; rowCount++)
            {
                int itensInRow = availableSlots;
                if (itensInRow >= columns)
                {
                    itensInRow = columns;
                    availableSlots -= columns;
                }

                List<ISlot> rowColumns = slots.GetRange((rowCount * columns), itensInRow);
                _map.Add(new SlotRow(rowColumns));
            }
        }
        private List<ISlot> AddSlotRow(List<ISlot> slots)
        {
            int slotCount = slots.Count;
            List<ISlot> remainingSlots = new List<ISlot>();
            if (slotCount > _columns)
            {
                slotCount = _columns;
                remainingSlots = slots.GetRange(_columns, slotCount);
            }
            List<ISlot> slotsToAdd = slots.GetRange(0, slotCount);
            _map.Add(new SlotRow(slotsToAdd));
            _rows++;
            return remainingSlots;
        }
        private void AddSlotRow(ISlot slot)
        {
            AddSlotRow(new List<ISlot>() { slot });
        }
        private bool SlotRowIsFull(SlotRow slotRow)
        {
            return slotRow._slots.Count >= _columns;
        }
        private void AddSlotToLastRow(ISlot slot)
        {
            _map[_rows - 1]._slots.Add(slot);
        }
        public SlotRow GetCollumn(int index)
        {
            return _map[index];
        }
        public void AddSlot(ISlot slot)
        {
            if (SlotRowIsFull(_map[_rows - 1]))
            {
                AddSlotRow(slot);
            }
            else
            {
                AddSlotToLastRow(slot);
            }
        }
        public void RemoveSlot()
        {
            if (_map[_rows - 1]._slots.Count <= 1)
            {
                _map[_rows - 1] = null;
                _rows--;
            }
            else
            {
                _map[_rows - 1]._slots.RemoveAt(-1);
            }
        }
        public ISlot GetSlot(Vector2Int position, UIControlEnum movementEnum)
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
                        for (int row = _rows - 1; row >= 0; row--)
                        {
                            if (ItemExists(row, xPos))
                            {
                                slot = _map[row]._slots[xPos];
                                break;
                            }
                        }
                    }

                }
            }
            else
            {
                if (ItemExists(0, 0))
                {
                    slot = _map[0]._slots[0];
                }
            }
            return slot;
        }
#pragma warning disable 0168 // Disable unused 'e' in catch
        private bool ItemExists(int row, int column)
        {
            try
            {
                SlotRow possibleRow = _map[row];
                if (possibleRow != null)
                {
                    if (possibleRow._slots[column] != null)
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
}