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

        public ObservableCollection<ViewBox> ViewBoxes { get; set; }
        public short CurrentValue { get; set; }

        public ICommand NewGameCommand { get; private set; }
        public ICommand CheatCommand { get; private set; }
        public ICommand CheckCommand { get; private set; }
        public ICommand HintCommand { get; private set; }

        public MainViewModel () {
            sudoku=new Wrapper ();

            NewGame ();
            NewGameCommand=new RelayCommand (NewGame);
            CheckCommand=new RelayCommand (CheckGame);
            CheatCommand=new RelayCommand (CheatGame);
            HintCommand=new RelayCommand (HintGame);
        }

        public void NewGame () {
            sudoku.Create ();
            var boxes = new List<ViewBox> ();
            for (int i = 0;i<81;i++) {
                var p = new Box {
                    X=Convert.ToInt16 (((i/9)+1)),
                    Y=Convert.ToInt16 (((i%9)+1))
                };
                sudoku.GetValue (p);
                p.IsEditable=p.Value==0;
                boxes.Add (new ViewBox (sudoku, p));
            }
            ViewBoxes=new ObservableCollection<ViewBox> (boxes);
            RaisePropertyChanged ("ViewBoxes");
        }
        public void CheckGame () {
            string message;
            message=sudoku.IsValid () ? "Game == valid" : "Game != valid";
            MessageBox.Show (message, "Check");
        }
        public void HintGame () {
            int toSolve = ViewBoxes.Where (x => x.Value==0).Count ();

            if (toSolve>1) {
                var box = sudoku.GetHint ();

                var vBox = ViewBoxes.Where (x => x.X==box.X&&x.Y==box.Y).First ();
                vBox.Value=box.Value;
            } else {
                MessageBox.Show ("No more Hints!", "Hints");
            }
            
        }
        public void CheatGame () {
            int toSolve = ViewBoxes.Where (x => x.Value==0).Count ();
            for (int i = 0;i<toSolve;i++) {
                var box = sudoku.GetHint ();
                var vBox = ViewBoxes.Where (x => x.X==box.X&&x.Y==box.Y).First ();
                vBox.Value=box.Value;
            }
        }
    }
}