// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Data;
using System.Web.UI.WebControls;
using OliWeb.Klassen;

namespace OliWeb.Controls.Floor.Journal
{
    // TODO: All dieser Code muss wesentlich einfacher gehen!
    // Aber das muss einer machen und alle Flankeneffekte beachten!
    // TODO:


    ///<summary>
    ///    Das Control für die neuesten <a href="SAPCT.htm">[SAPCT]</a> - Einträge in die Datenbank.
    ///</summary>
    public partial class JournalControl : MasterControl
    {
        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        ///<summary>
        ///    Erforderliche Methode für die Designerunterstützung.
        ///    Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        ///</summary>
        private void InitializeComponent()
        {
            JournalDataGrid.SortCommand += JournalDataGrid_SortCommand;
            JournalDataGrid.ItemDataBound += JournalDataGrid_ItemDataBound;
        }


        // Member
        // ------

        /// <summary>
        ///     Es soll nicht möglich sein, alle Datensätze aus einer
        ///     Tabellen anzuzeigen. Hier wird die Obergrenze festgelegt.
        /// </summary>
        private int MAXZEILEN = 200;

        #region Eigenschaften

        // Eigenschaften
        // -------------

        /// <summary>
        ///     journal-Daten in/aus Session gespeichert. Wird beim ersten Aufruf
        ///     dieses Control festgelegt.
        ///     TODO: Könnte eigentlich auch in den Application Cache!
        /// </summary>
        private OliEngine.OliDataAccess.Views.Journal journal
        {
            get
            {
                var j = (OliEngine.OliDataAccess.Views.Journal) Session["journal"];
                if (j == null)
                {
                    j = new OliEngine.OliDataAccess.Views.Journal(ZeilenZahl);
                    Session["journal"] = j;
                }
                return (OliEngine.OliDataAccess.Views.Journal) Session["journal"];
            }
            set { Session["journal"] = value; }
        }

        /// <summary>
        ///     Anzahl der anzuzeigenden Zeilen. Der Wert wird aus der ZeilenTextBox auf dem Formular genommen und
        ///     wenn nötig auf zwischen 1 und MAXZEILEN korrigiert.
        /// </summary>
        protected int ZeilenZahl
        {
            get
            {
                // holen
                int zz = int.Parse(ZeilenTextBox.Text);
                if (zz < 1)
                {
                    zz = 1;
                }
                if (zz > MAXZEILEN)
                {
                    zz = MAXZEILEN;
                }
                return (zz);
            }
            set { ZeilenTextBox.Text = value.ToString(); }
        }

        /// <summary>
        ///     Zeichenfolge nach der sortiert werden soll.
        /// </summary>
        /// <remarks>
        ///     Defaultwert ist 'datum' (also die neuesten zuerst)
        /// </remarks>
        private string sortString
        {
            get
            {
                if (ViewState["ss"] == null)
                {
                    ViewState["ss"] = "datum";
                    desc = true;
                }
                return (ViewState["ss"].ToString());
            }
            set { ViewState["ss"] = value; }
        }

        /// <summary>
        ///     wenn absteigend sortiert werden soll, diesen Wert auf true setzen
        /// </summary>
        private bool desc
        {
            get
            {
                if (ViewState["desc"] == null)
                {
                    ViewState["desc"] = true;
                }
                return ((bool) ViewState["desc"]);
            }
            set { ViewState["desc"] = value; }
        }

        #endregion

        #region Methoden

        // Methoden
        // --------

        /// <summary>
        ///     Beim ersten Aufruf diese Controls wird die interne journal-Tabelle gefüllt
        ///     und der ZeilenZahlTextBox dem Enter-Key ereignis zugeordnet.
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Visible)
            {
                journal = new OliEngine.OliDataAccess.Views.Journal(ZeilenZahl);
            }

            ZeilenTextBox.Attributes.Add("onKeyDown",
                                         "javascript:if(event.keyCode == 13 || event.which == 13 ){event.returnValue=false;event.cancel=true;JournalControl1_AlleButton.click();}");
        }

        /// <summary>
        ///     vor dem Rendern dieses Control wird die journal-Tabelle nach dem
        ///     <see cref="sortString" /> auf- oder absteigend sortiert und gebunden.
        /// </summary>
        /// <param name="e"> </param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            DataView dv = new DataView(journal.UnionJournale);
            if (desc)
            {
                dv.Sort = sortString + " DESC";
            }
            else
            {
                dv.Sort = sortString;
            }
            JournalDataGrid.DataSource = dv;
            JournalDataGrid.DataBind();
        }

        /// <summary>
        ///     Beim Binden eines jeden Journal-Elementes wird die <b>CssClass</b> auf den 
        ///     passenden Typ (SAPT) eingestellt.
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        private void JournalDataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string z = JournalDataGrid.DataKeys[e.Item.ItemIndex].ToString();
                var li = e.Item.FindControl("PanelItem") as WebControl;
                switch (z)
                {
                    case "P":
                        li.CssClass = "PostIt";
                        break;
                    case "S":
                        li.CssClass = "Stamm";
                        break;
                    case "T":
                        li.CssClass = "TopLab";
                        break;
                    case "A":
                        li.CssClass = "Angler";
                        break;
                    case "X":
                        li.CssClass = "Bewertung";
                        break;
                }
            }
        }

        protected string SubDir(string zeichen)
        {
            if (zeichen.ToLower() == "x")
            {
                return "T";
            }
            else
            {
                return zeichen;
            }
        }

        // AlleButton_Click()
        protected void AlleButton_Click(object sender, EventArgs e)
        {
            journal = new OliEngine.OliDataAccess.Views.Journal(ZeilenZahl);
        }

        // StammButton_Click()
        protected void StammButton_Click(object sender, EventArgs e)
        {
            journal = new OliEngine.OliDataAccess.Views.Journal("S", ZeilenZahl);
        }

        // PostItButton_Click()
        protected void PostItButton_Click(object sender, EventArgs e)
        {
            journal = new OliEngine.OliDataAccess.Views.Journal("P", ZeilenZahl);
        }

        // AnglerButton_Click()
        protected void AnglerButton_Click(object sender, EventArgs e)
        {
            journal = new OliEngine.OliDataAccess.Views.Journal("A", ZeilenZahl);
        }

        // TopLabButton_Click()
        protected void TopLabButton_Click(object sender, EventArgs e)
        {
            journal = new OliEngine.OliDataAccess.Views.Journal("T", ZeilenZahl);
        }

        // JournalDataGrid_SortCommand()
        private void JournalDataGrid_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            if (sortString == e.SortExpression)
            {
                desc = !desc;
            }
            sortString = e.SortExpression;
        }

        #endregion
    }
}