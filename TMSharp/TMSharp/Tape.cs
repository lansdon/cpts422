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

        public Tape()
        {
            _cells = " ";
            _currentCell = 0;
            _blank =  ' ';
        }
        public void load(ref StreamReader definition, ref bool valid) 
        {
	        bool next_keyword_found = false;
            string line = null;
 			if((line = definition.ReadLine()) != null &&
                (line.Length == 1) &&
               (line.Length == 1) && (line[0] != '\\') &&
               (line[0] != '<') && (line[0] != ']') &&
               (line[0] >= '!') && (line[0] <= '~')) {
                _blank = line[0];
			} 
            else 
            {
				Console.WriteLine("Illegal Blank Character.\n");
				valid = false;
			}
            if((line = definition.ReadLine()) != null && line.ToUpper() != "FINAL_STATES:")  // prior to INPUT_ALPHABET: keyword
            {		
                Console.WriteLine("Missing keyword after _blank character.\n");
                valid = false;
            }       
        }

        void view() {
            Console.WriteLine("B =  {0}\n", _blank);
        }

        void initialize(string inputString) {
            _cells = inputString + _blank;
            _currentCell = 0;
        }

        void update(char writeCharacter, Char moveDirection) {
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

        string left(uint maxCells) {
            int first_cell = Math.Max(0, _currentCell - (int)maxCells);
            string value = _cells.Substring(first_cell, _currentCell-first_cell);
            if(value.Length < _currentCell) {
                value.Insert(0, "<");
            }
            return value;
        }

        string right(uint maximum_number_of_cells) {
            int endCell = (int)_cells.Length;
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
