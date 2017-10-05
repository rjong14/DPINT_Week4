using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF.ViewModel
{
    public class GameBox : INotifyPropertyChanged
    {
        private Wrapper game;
        private Box box;

        public event PropertyChangedEventHandler PropertyChanged;

        public GameBox (Wrapper game, Box box) {
            this.game=game;
            this.box=box;
        }
        public short Value {
            get => box.Value;
            set {
                //Validate value
                if (value>9||value<1) {
                    MessageBox.Show ("Vul een waarde van 1 t/m 9 in.", "Ongeldige waarde", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                //Value has been validated, set the property
                box.Value=value;
                //Update class library value
                game.SetValue (box);
                //Check if game has been finished
                if (game.IsCompleted ()) {
                    MessageBox.Show ("De Sudoku is opgelost", "Sudoku opgelost", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                OnPropertyChanged ();
            }
        }

        public short X {
            get => box.X;
            set => box.X=value;
        }

        public short Y {
            get => box.Y;
            set => box.Y=value;
        }

        public bool IsEditable {
            get => box.IsEditable;
            set => box.IsEditable=value;
        }

        protected void OnPropertyChanged ([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (propertyName));
    }
}
