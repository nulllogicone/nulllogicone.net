// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Data;
using System.Web.UI.WebControls;
using OliEngine;
using OliEngine.DataSetTypes;
using OliEngine.OliMiddleTier.OLIs;
using OliWeb.Controls.Gimicks;
using OliWeb.Klassen;

namespace OliWeb.Controls.Koerper.Organ
{
    ///<summary>
    ///    StammOrgan.
    ///</summary>
    public partial class StammOrgan : MasterControl
    {
        protected Panel StammPanel;



        //		protected System.Web.UI.WebControls.LinkButton StammLinkButton;

        // Member
        // ------

        protected KlickBild KlickBild1;

        // Page_Load()
        protected void Page_Load(object sender, EventArgs e)
        {
            Malen();
        }

        // Methoden
        // --------

        // Malen
        protected void Malen()
        {
            if (OliUser.Stamm != null)
            {
                if (!OliUser.Stamm.BinIchEingeloggt)
                {
                    //					ShowEdit = false;
                    MailHyperLink.Visible = true;
                }

                StammDataSet.StammRow s = OliUser.Stamm.StammRow;

                // Neue Stamm Row
                if (s.RowState == DataRowState.Added)
                {
                    StammHyperLink.Visible = false;
                    StammHyperLink.NavigateUrl = Request.ServerVariables["SCRIPT_NAME"] + "?sguid=" + s.StammGuid;
                }
                else
                {
                    StammHyperLink.Visible = true;
                }

                // Einzelansicht beschriften
                StammHyperLink.NavigateUrl = Request.ServerVariables["SCRIPT_NAME"] + "?sguid=" + s.StammGuid;

                StammHyperLink.Text = s.Stamm;
                StammHyperLink.ToolTip = "Diesen Stamm anzeigen";
                BeschreibungLabel.Text = s.IsBeschreibungNull() ? "" : s.Beschreibung;
                LinkHyperLink.Text = s.IsLinkNull() ? "" : s.Link;
                LinkHyperLink.NavigateUrl = s.IsLinkNull() ? "" : s.Link;
                KooKLabel.Text = OliUtil.MakeRedKook(s.KooK);
                DatumLabel.Text = s.Datum.ToShortDateString();
                DatumLabel.ToolTip = "erzeugt: " + s.Datum;
                // KlickBild
                // dem Stamm wird immer ein Bild eingef�gt - und wenns das O ist
                //KlickBild1.Breite = 20;
                if (s.IsDateiNull() || s.Datei.Length == 0)
                {
                    KlickBild1.BildName = "/O.gif";
                }
                else
                {
                    KlickBild1.BildName = s.Datei;
                }
            }
        }

        protected Guid StammGuid
        {
            get
            {
                if (OliUser.Stamm != null)
                {
                    return OliUser.Stamm.StammRow.StammGuid;
                }
                else return Guid.Empty;
            }
        }
        protected string StammName
        {
            get
            {
                if (OliUser.Stamm != null)
                {
                    return OliUser.Stamm.StammRow.Stamm;
                }
                else return "";
            }
        }
    }
}

