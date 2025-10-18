// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Data;
using System.Web.UI.WebControls;
using OliWeb.Klassen;

namespace OliWeb.Controls.Koerper
{
    ///<summary>
    ///    das Element für die aktuelle Nachricht. Sie wird durch das <see cref="Organ.PostItOrgan" /> 
    ///    mit Bild und Frist und Wert dargestellt. Oben rechts kann man sie mit x schließen.
    ///</summary>
    public partial class PostItKoerper : MasterControl
    {
        protected Panel buttons;

        /// <summary>
        ///     es wird entweder der An- oder Abwurzeln Button aktiviert
        ///     und für geschlossene PostIt das schleier.css eingestellt
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // wenn die Nachricht nicht mehr geöffnet bzw. geschlossen ist,
            // wird ein Schleier angezeigt
            if (!PostIt.IsOpen || PostIt.StammClosed)
            {
                PostItPanel.CssClass = "schleier";
            }

            // QLabel
            QLabel.Text = OliUser.Stamm.Q.P;

            // VonStamm wird der Urheber angezeigt ausser ... es ist neu
            if (PostIt.PostItRow.RowState != DataRowState.Added)
            {
                VonLabel.Text = PostIt.MyStammHtmlList;

//				// BezahltLabel
//				BezahltLabel.Text = OliUtil.MakeRedKook(PostIt.StammZahlt);
//				// FristLabel
//				FristLabel.Text = OliUtil.MakeDateTimeDiff(PostIt.StammFrist);

                // UrheberStamm
                if (PostIt.BinIchMeinPostIt && Stamm.BinIchEingeloggt)
                {
                    // MeinsPanel
//					MeinsPanel.Visible = true;
//					// BezahltLabel
//					BezahltLabel.Text = OliUtil.MakeRedKook(PostIt.StammZahlt);
//					// FristLabel
//					FristLabel.Text = OliUtil.MakeDateTimeDiff(PostIt.StammFrist);
                    // EditHyperLink
                    EditHyperLink.Visible = true;
                }
                else
                {
                    EditHyperLink.Visible = false;
                }
            }
        }
    }
}