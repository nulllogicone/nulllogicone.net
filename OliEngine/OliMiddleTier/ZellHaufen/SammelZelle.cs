// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using System.Collections;

namespace OliEngine.OliMiddleTier.ZellHaufen
{
    /// <summary>
    ///     SammelZelle.
    /// </summary>
    public abstract class SammelZelle : Zelle
    {
        // Member
        // ------


        private ArrayList childs = new ArrayList();

        // Konstruktor
        // -----------

        public SammelZelle(Guid guid, int ebene) : base(guid, ebene)
        {
        }

        public SammelZelle(Guid guid, int ebene, VerteilZelle von) : base(guid, ebene)
        {
            Von = von;
        }

        // Eigenschaften
        // -------------

        // Beschreibung
        public string Beschreibung { get; set; }

        // Bild
        //public string Bild
        //{
        //    get { return (OliUtil.MakeImageHtml(datei, 40)); }
        //    set { datei = value; }
        //}

        // Von
        public VerteilZelle Von { get; set; }

        // Childs
        public ArrayList Childs
        {
            get { return (childs); }
            set { childs = value; }
        }

        // Methoden
        // --------

        // NewChild
        public abstract VerteilZelle NewChild();

        // MakeChild(guid)
        public abstract VerteilZelle MakeChild(Guid guid);

        // MakeAllMyChilds()
        public abstract void MakeAllMyChilds();

        // RemoveMe()
        public abstract override void RemoveMe();

        // RemoveChild(VerteilZelle)
        public void RemoveChild(VerteilZelle vz)
        {
            if (Childs.Contains(vz))
            {
                Childs.Remove(vz);
            }
        }
    }
}