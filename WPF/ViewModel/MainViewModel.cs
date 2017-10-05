using Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using System.Windows;
using System.Collections.Generic;
using System;


namespace WPF.ViewModel {
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase {

        private Wrapper sudoku;

        public ObservableCollection<GameBox> GameBoxes { get; set; }
        public short CurrentValue { get; set; }

        public ICommand NewGameCommand { get; private set; }
        public ICommand CheatCommand { get; private set; }
        public ICommand CheckCommand { get; private set; }

        public MainViewModel () {
            sudoku=new Wrapper ();

            NewGame ();
        }


        public bool CanNewGame => true;

        public bool CanCheckGame => true;

        public bool CanCheatGame => true;


        public void NewGame () {
            //Let class library create a new game
            sudoku.Create ();
            //Iterate over all existing locations
            var boxes = new List<GameBox> ();
            for (int i = 0;i<81;i++) {
                var p = new Box {
                    X=Convert.ToInt16 (((i/9)+1)),
                    Y=Convert.ToInt16 (((i%9)+1))
                };
                sudoku.GetValue (p);
                //Lock default values
                p.IsEditable=p.Value==0;
                boxes.Add (new GameBox (sudoku, p));
            }
            GameBoxes=new ObservableCollection<GameBox> (boxes);
            //Make notification of changed locations
            RaisePropertyChanged ("GameBoxes");
        }

        public void CheckGame () {
            //Check if current board values are correct, notify user with a messagebox
            string message;
            message=sudoku.IsValid () ? "De cijfers kloppen." : "Éen of meerder cijfers zijn Fout.";
            MessageBox.Show (message, "Spel controle");
        }

        public void CheatGame () {
            int toSolve = GameBoxes.Where (x => x.Value==0).Count ()-2;
            for (int i = 0;i<toSolve;i++) {
                //Ask for a hint
                var position = sudoku.GetHint ();
                //Check at what location the hint is positioned
                //We are always able to grab the first, since it is a hint from the dll
                var loc = GameBoxes.Where (x => x.X==position.X&&x.Y==position.Y).First ();
                loc.Value=position.Value;
            }
        }
    }
}