// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using System.Data;

namespace OliEngine.OliMiddleTier.ZellHaufen
{
    /// <summary>
    ///     Zelle.
    /// </summary>
    public abstract class Zelle
    {
        // Member 
        // ------

        // Konstruktor
        // -----------

        // Zelle(int,int)
        public Zelle(Guid guid, int ebene)
        {
            Guid = guid;
            Ebene = ebene;
        }

        // Eigenschaften
        // -------------

        // Guid
        public Guid Guid { get; set; }

        // MyRow
        public virtual DataRow MyRow
        {
            get { return (null); }
        }

        // Ebene
        public int Ebene { get; set; }

        // Methoden
        // --------

        // Next()
        public abstract Zelle Next();

        // RemoveMe()
        public abstract void RemoveMe();

        // Top()
        public abstract NetzZelle Top();
    }
}