// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;

namespace OliEngine.OliMiddleTier.ZellHaufen
{
    /// <summary>
    ///     VerteilZelle.
    /// </summary>
    public abstract class VerteilZelle : Zelle
    {
        // Member
        // ------

        public int VgbOLIs = -1;
        public int VgbGet = -1;
        public int VgbILOs = -1;
        public int VgbFit = -1;

        public bool BuntePunkteEdit;

        // Konstruktor
        // -----------

        public VerteilZelle(Guid guid, int ebene) : base(guid, ebene)
        {
        }

        public VerteilZelle(Guid guid, int ebene, SammelZelle parent) : base(guid, ebene)
        {
            Parent = parent;
        }

        // Eigenschaften
        // -------------

        // Parent
        public SammelZelle Parent { get; set; }

        // Weiter
        public SammelZelle Weiter { get; set; }

        // Markiert
        public bool Markiert
        {
            get
            {
                if (VgbOLIs >= 0 || VgbGet >= 0 || VgbILOs >= 0 || VgbFit >= 0)
                {
                    return (true);
                }
                else
                {
                    return (false);
                }
            }
        }

        // TieferMarkiert
        public bool TieferMarkiert
        {
            get
            {
                bool tm = false;
                int abEbene = Ebene;

                Zelle z = Next();

                while (z != null && z.Ebene > abEbene)
                {
                    if (z is VerteilZelle)
                    {
                        if (((VerteilZelle) z).Markiert)
                        {
                            tm = true;
                            abEbene = 99;
                        }
                    }
                    else
                    {
                        // wenn es eine Sammelzelle ist => gleich noch eins weiter
                        z = z.Next();
                    }
                    z = z.Next();
                }

                return (tm);
            }
        }

        // Methoden
        // --------

        // RemoveMe()
        public override void RemoveMe()
        {
            Parent.RemoveChild(this);
        }

        // MakeWeiter
        public abstract SammelZelle MakeWeiter();
    }
}