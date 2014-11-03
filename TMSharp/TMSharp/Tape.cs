using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSharp
{
    public class Tape
    {
        // This is the list of characters as they appear on the tape, starting at cell 0.
        private string _cells;
        //o	the cell where the tape head is located
        private int _currentCell;
        // The _blank character specified by the TM Definition.
        private char _blank;

        public char CurrentChar
        {
            get { return _cells.ElementAt(_currentCell); }
        }

        public char BlankCharacter
        {
            get { return _blank;  }
            private set { _blank = value; }
        }

        public Tape()
        {
            _cells = " ";
            _currentCell = 0;
            _blank =  ' ';
        }

        public bool load(ref List<string> definition) 
        {
            List<string> results;
            if (TMParse.ParseDefinitionValues(ref definition, "FINAL_STATES:", out results) && results.Count() >= 1)
            {
                string blank = results[0];
                if (
                    (blank.Length == 1) &&
                   (blank[0] != '\\') &&
                   (blank[0] != '<') &&
                   (blank[0] != ']') &&
                   (blank[0] >= '!') &&
                   (blank[0] <= '~'))
                {
                    _blank = blank[0];
                    return true;
                }
                else
                {
                    Console.WriteLine("Illegal Blank Character.\n");
                    return false;
                }
            }
            Console.WriteLine("Illegal Blank Character.\n");
            return false;

            if(definition.Count() >= 2)
            {
                string blank = definition[0];
                definition.RemoveAt(0);

                if (blank != null &&
                    (blank.Length == 1) &&
                   (blank[0] != '\\') &&
                   (blank[0] != '<') && 
                   (blank[0] != ']') &&
                   (blank[0] >= '!') && 
                   (blank[0] <= '~') )
                {
                    _blank = blank[0];
                }
                else
                {
                    Console.WriteLine("Illegal Blank Character.\n");
                    return false;
                }

                String keyword = definition[0];
                definition.RemoveAt(0);

                if (keyword != null && keyword.ToUpper() == "FINAL_STATES:")  // prior to INPUT_ALPHABET: keyword
                {
                    return true;
                }
                else Console.WriteLine("Missing keyword after _blank character.\n");
            }
            return false;
        }

        public void view()
        {
            Console.WriteLine("B =  {0}\n", _blank);
        }

        public void initialize(string inputString)
        {
            _cells = inputString + _blank;
            _currentCell = 0;
        }

        public void update(char writeCharacter, Char moveDirection)
        {
            moveDirection = Char.ToUpper(moveDirection);
            if((moveDirection == 'L') && (_currentCell == 0)) {
                throw new Exception("Left move from first cell");
            }

            if((moveDirection == 'R') &&
               (_currentCell == _cells.Length-1)) {
                _cells += _blank;
            }

            StringBuilder sb = new StringBuilder(_cells);
            sb[_currentCell] = writeCharacter;
            _cells = sb.ToString();
           
            if(moveDirection == 'L') --_currentCell;
            else ++_currentCell;

        }

        public string left(uint maxCells) 
        {
            int first_cell = Math.Max(0, _currentCell - (int)maxCells);
            string value = _cells.Substring(first_cell, _currentCell-first_cell);
            if(value.Length < _currentCell) 
            {
                value.Insert(0, "<");
            }
            return value;
        }

        public string right(uint maximum_number_of_cells)
        {
            int endCell = (int)_cells.Length-1;
            while ((endCell >= _currentCell) && (_cells[endCell] == _blank)) 
            {
                --endCell;
            }
            int last_cell = Math.Min(endCell, _currentCell + (int)maximum_number_of_cells - 1);
            string value = _cells.Substring(_currentCell, last_cell-_currentCell+1);
            if (value.Length < (endCell - _currentCell))
            {
                value += ">";
            }
            return value;
        }

    }
}
