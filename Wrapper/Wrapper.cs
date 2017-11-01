using Sudoku;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain {

    public class Wrapper {
        private IGame game;

        public Wrapper () => game=new Game ();

        public void Create () => game.create ();

        public bool SetValue (Box box) {
            game.set (box.X, box.Y, box.Value, out int success);
            return success==1;
        }

        public void GetValue (Box box) {
            game.get (box.X, box.Y, out short val);
            box.Value=val;
        }

        public bool IsValid () {
            int isValid;
            game.isValid (out isValid);
            return isValid==1;
        }

        public bool IsCompleted () {
            //Check if all current filled in values are correct
            if (!IsValid ())
                return false;
            return GetBoard ().Where (x => x.Value>0).Count ()==81;
        }

        public List<Box> GetBoard () {
            var boxes = new List<Box> (81);
            for (short y = 1;y<=9;y++) {
                for (short x = 1;x<=9;x++) {
                    var p = new Box {
                        X=x,
                        Y=y
                    };
                    boxes.Add (p);
                    GetValue (p);
                }
            }
            return boxes;
        }
        public Box GetHint () {
            game.hint (out short x, out short y, out short value, out int succeeded);
            //We didn't succeed to retrieve a value
            if (succeeded!=1) {
                return null;
            }

            return new Box () {
                X=x,
                Y=y,
                Value=value
            };
        }
        public void Set (short x, short y, short value, out int succeeded) => game.set (x, y, value, out succeeded);
        public void Get (short x, short y, out short value) => game.get (x, y, out value);
        //public void IsValid (out int valid) => game.isValid (out valid);
        public void Hint (out short x, out short y, out short value, out int succeeded) => game.hint (out x, out y, out value, out succeeded);
        public void Read (out int canRead) => game.read (out canRead);
        public void Write (out int canWrite) => game.write (out canWrite);
    }
}
