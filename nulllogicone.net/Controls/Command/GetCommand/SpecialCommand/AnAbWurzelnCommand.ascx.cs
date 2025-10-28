// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using System.Data;
using OliWeb.Klassen;

namespace OliWeb.Controls.Command.GetCommand.SpecialCommand
{
    ///<summary>
    ///    AnAbWurzelnCommand.
    ///</summary>
    public class AnAbWurzelnCommand : CommandBase
    {
        protected System.Web.UI.WebControls.HyperLink HyperLink1;

        private void Page_Load(object sender, System.EventArgs e)
        {
            // zuerst alles deaktivieren
            HyperLink1.Text = "An-, Ab- Wurzeln";
            HyperLink1.NavigateUrl = Helper.MakeBaseLink() + "Sites/Edit/AnAbWurzeln.aspx";
            HyperLink1.Visible = false;
            HyperLink1.Enabled = false;


            if (Stamm != null &&
                Stamm.BinIchEingeloggt &&
                PostIt != null &&
                PostIt.PostItRow.RowState != DataRowState.Added)
            {
                HyperLink1.Visible = true;

                // wenn es meins ist
                if (PostIt.BinIchMeinPostIt)
                {
                    HyperLink1.Text = "Abwurzeln";
                    // wenn ich nicht der Ur-Urheber bin
                    if (PostIt.StammZust != 1)
                    {
                        HyperLink1.Enabled = true;
                    }
                }
                    // wenn es nicht meins ist
                else
                {
                    HyperLink1.Text = "Anwurzeln";
                    HyperLink1.Enabled = true;
                }
            }

            if (Page is Sites.Edit.AnAbWurzeln)
            {
                HyperLink1.CssClass = "ButtonSel";
            }
        }

        #region Vom Web Form-Designer generierter Code

        protected override void OnInit(EventArgs e)
        {
            // CODEGEN: Dieser Aufruf ist f�r den ASP.NET Web Form-Designer erforderlich.
            InitializeComponent();
            base.OnInit(e);
        }

        ///<summary>
        ///    Erforderliche Methode f�r die Designerunterst�tzung
        ///</summary>
        private void InitializeComponent()
        {
            Load += Page_Load;
        }

        #endregion
    }
}
