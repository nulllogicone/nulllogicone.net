// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Data;
using OliWeb.Sites.Edit;

namespace OliWeb.Controls.Command.GetCommand.SpecialCommand
{
    ///<summary>
    ///    AnAbWurzelnCommand.
    ///</summary>
    public partial class AnAbWurzelnCommand : CommandBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // zuerst alles deaktivieren
            HyperLink1.Text = "An-, Ab- Wurzeln";
            HyperLink1.NavigateUrl = "~/Sites/Edit/AnAbWurzeln.aspx";
            HyperLink1.Visible = false;
            HyperLink1.Enabled = false;

            if (Stamm != null &&
                Stamm.BinIchEingeloggt &&
                PostIt != null &&
                PostIt.PostItRow.RowState != DataRowState.Added)
            {
                HyperLink1.Visible = true;
                HyperLink1.Enabled = true;

                // wenn die Nachricht (p) von mir(S) ist => kann ich 'abwurzeln'
                if (PostIt.BinIchMeinPostIt)
                {
                    HyperLink1.Text = "Abwurzeln";

                    // wenn ich Ur-Urheber bin (mein Zustand = 1), kann ich nicht 'abwurzeln'
                    if (PostIt.StammZust == 1)
                    {
                        HyperLink1.Visible = false;
                        HyperLink1.Enabled = false;
                    }
                }
                    // wenn die Nachricht (P) NICHT von mir (S) ist = kann ich 'anwurzeln'
                else
                {
                    HyperLink1.Text = GetGlobalResourceObject("General", "join").ToString();
                    HyperLink1.Enabled = true;
                }
            }

            if (Page is AnAbWurzeln)
            {
                HyperLink1.Visible = false;
            }
        }

    }
}
