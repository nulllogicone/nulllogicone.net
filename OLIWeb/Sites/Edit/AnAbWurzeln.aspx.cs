// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Data;
using OliWeb.Klassen;

namespace OliWeb.Sites.Edit
{
    /// <summary>
    ///     AnAbMelden.
    /// </summary>
    public partial class AnAbWurzeln : MasterStammPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AnWurzelnPanel.Visible = false;
            AbWurzelnPanel.Visible = false;

            StammLabel.Text = OliUser.Stamm.StammRow.Stamm;

            if (Stamm != null &&
                Stamm.BinIchEingeloggt &&
                PostIt != null &&
                PostIt.PostItRow.RowState != DataRowState.Added)
            {
                // wenn es meins ist
                if (PostIt.BinIchMeinPostIt)
                {
                    AbWurzelnPanel.Visible = true;
                }
                    // wenn es nicht meins ist
                else
                {
                    AnWurzelnPanel.Visible = true;
                }
            }
        }



        protected void AnWurzelnButton_Click(object sender, EventArgs e)
        {
            Guid pguid = PostIt.PostItRow.PostItGuid;
            PostIt.Anwurzeln(OliUser.EingeloggterStamm);
            //			PostIt.MyStamm = null;
            OliUser.Stamm.ShowPostIt(pguid);
            OliUser.Stamm.MyPostIt = null;
//			Helper.RedirectToSite();
            Response.Redirect("~/Sites/PostItSite.aspx");
        }

        protected void AbWurzelnButton_Click(object sender, EventArgs e)
        {
            if (PostIt.StammZahlt < 0)
            {
                OliUser.Nachricht = "Wenn bezahlt Betrag kleiner null kann man sich nicht abwurzeln";
            }
            else
            {
                PostIt.Abwurzeln(OliUser.EingeloggterStamm);
                OliUser.Stamm = null;
                OliUser.ShowStamm(OliUser.EingeloggterStamm.StammRow.StammGuid);
//				Helper.RedirectToSite(); 
                Response.Redirect("~/Sites/PostItSite.aspx");
            }
        }
    }
}