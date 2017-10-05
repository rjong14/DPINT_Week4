using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.Controllers {
    public class HomeController : Controller {
        private Wrapper curgame;

        public HomeController () { }

        public ActionResult Index () {
            RetrieveGameSession ();
            return View (curgame);
        }

        public ActionResult NewGame () {
            NewGameSession ();
            return RedirectToAction ("Index");
        }

        public ActionResult Hint () {
            RetrieveGameSession ();
            int toSolve = curgame.GetBoard ().Where (x => x.Value==0).Count ()-2;
            for (int x = 0;x<toSolve;x++) {
                Box box = curgame.GetHint ();
                curgame.SetValue (box);
            }
            return RedirectToAction ("Index");
        }

        #region Ajax

        public void SetValue (short posx, short posy, short value) {
            RetrieveGameSession ();
            curgame.SetValue (new Box () { X=posx, Y=posy, Value=value });
            if (curgame.IsCompleted ()) {
                RedirectToAction ("Index");
            }
        }

        #endregion

        private void NewGameSession () {
            var game = new Wrapper ();
            game.Create ();
            Session ["game"]=game;
        }

        private void RetrieveGameSession () {
            curgame=(Wrapper) Session ["game"];
            if (curgame==null) {
                NewGameSession ();
                curgame=(Wrapper) Session ["game"];
            }
        }
    }
}