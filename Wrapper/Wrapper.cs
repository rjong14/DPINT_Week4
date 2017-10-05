using Sudoku;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Box
    {
        public Box()
        {
            X = -1;
            Y = -1;
            Value = -1;
        }

        public short X { get; set; }

        public short Y { get; set; }

        public short Value { get; set; }

        public bool IsEditable { get; set; }
    }
    public class Wrapper
    {
        private IGame game;

        /// <summary>
        /// Constructor and initializing
        /// </summary>
        public Wrapper()
        {
            game = new Game();
        }

        /// <summary>
        /// Create a new game
        /// </summary>
        public void NewGame()
        {
            game.create();
        }

        /// <summary>
        /// Check if value can be set
        /// </summary>
        /// <param name="box">Value with location that the user wants to set</param>
        /// <returns></returns>
        public bool SetValue(Box box)
        {
            int success;
            game.set(box.X, box.Y, box.Value, out success);
            return success == 1;
        }

        /// <summary>
        /// Get current value for given position
        /// </summary>
        /// <param name="box"></param>
        public void GetValue(Box box)
        {
            short val;
            game.get(box.X, box.Y, out val);
            box.Value = val;
        }

        /// <summary>
        /// Validate to check if the current filled numbers are correct
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            int isValid;
            game.isValid(out isValid);
            return isValid == 1;
        }

        public bool IsCompleted()
        {
            //Check if all current filled in values are correct
            if (!IsValid())
                return false;
            return GetBoard().Where(x => x.Value > 0).Count() == 81;
        }

        #region NotUsed

        /// <summary>
        /// Not sure when to use this
        /// </summary>
        /// <returns></returns>
        public bool Read()
        {
            int succeeded;
            game.read(out succeeded);
            return succeeded == 1;
        }

        /// <summary>
        /// Not sure when to use this
        /// </summary>
        /// <returns></returns>
        public bool Write()
        {
            int succeeded;
            game.write(out succeeded);
            return succeeded == 1;
        }

        #endregion

        /// <summary>
        /// GetBoard for getting a complete list with all coordinates and values
        /// </summary>
        /// <returns></returns>
        public List<Box> GetBoard()
        {
            var boxes = new List<Box>(81);
            for (short x = 1; x <= 9; x++)
            {
                for (short y = 1; y <= 9; y++)
                {
                    var p = new Box();
                    p.X = x;
                    p.Y = y;
                    boxes.Add(p);
                    GetValue(p);
                }
            }
            return boxes;
        }

        public Box GetHint()
        {
            short x, y, value;
            int succeeded;
            game.hint(out x, out y, out value, out succeeded);
            //We didn't succeed to retrieve a value
            if (succeeded != 1)
                return null;

            return new Box()
            {
                X = x,
                Y = y,
                Value = value
            };
        }
    }
}
