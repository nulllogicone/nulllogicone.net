// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;

namespace OliEngine.OliMiddleTier.OLIs
{
    /// <summary>
    ///     EingeloggterStamm.
    ///     Nur der kann alles d�rfen
    /// </summary>
    public class EingeloggterStamm : Stamm
    {
        // Konstruktor

        // TODO: is here really the core logic to compare secret with password? Should be refactored!
        public EingeloggterStamm(OliUser user, Guid sguid, string pwd) : base(user, sguid)
        {
            if (!stamm.StammRow.IsUnterschriftNull() && stamm.StammRow.Unterschrift != pwd)
            {
                throw new Exception("Falsches Kennwort");
            }
        }

        // Methoden
        // --------

        // NewAngler()

        public Angler NewAngler()
        {
            Angler = new Angler(this);
            return Angler;
        }

        // NewPostIt()

        public PostIt NewPostIt()
        {
            PostIt = new PostIt(this);
            return PostIt;
        }

        // NewTopLab(PostIt)

        public TopLab NewTopLab(PostIt postIt)
        {
            TopLab = new TopLab(this, postIt);
            return TopLab;
        }
    }
}
