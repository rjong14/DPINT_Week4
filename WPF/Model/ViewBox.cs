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
    public class ViewBox : INotifyPropertyChanged
    {
        private Wrapper game;
        private Box box;

        public event PropertyChangedEventHandler PropertyChanged;

        public ViewBox (Wrapper game, Box box) {
            this.game=game;
            this.box=box;
        }
        public short Value {
            get => box.Value;
            set {
                if (value>9||value<1) {
                    MessageBox.Show ("Give a value between 1 & 9", "Invalid value error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                box.Value=value;
                game.SetValue (box);
                if (game.IsCompleted ()) {
                    MessageBox.Show ("Game is completed", "Sudoku!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
