using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSharp
{
    public class Transition
    {
        public Transition(string sourceState, char readCharacter, string destinationState, char writeCharacter, char moveDirection)
        {	 
            SourceState = sourceState;
            ReadCharacter = readCharacter;
            DestinationState = destinationState;
            WriteCharacter = writeCharacter;
            MoveDirection = moveDirection;
        }

        //	The starting (source) state of the transition
        public string SourceState { get; private set; }

        //	The character that is read by the tape head
        public char ReadCharacter { get; private set; }

        //	The destination state after performing the transition
        public string DestinationState { get; private set; }

        //	The character to be written to the current cell when performing the transition
        public char WriteCharacter { get; private set; }

        //	The direction to move the tape head when performing the transition
        public char MoveDirection { get; private set; }
    }
}
