// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System.Web;
using OliEngine.OliDataAccess;

namespace OliWeb.Klassen
{
    /// <summary>
    ///     CacheManager ist ein <i>Singleton</i>.
    ///     Es stellt eine <see cref="PostIt" /> Tabelle und die <b>Antworten</b>
    ///     zur Verf�gung.
    /// </summary>
    public class CacheManager
    {
        // Singleton

        public static PostItList CachedPostIt
        {
            get
            {
                HttpContext ctx = HttpContext.Current;
                object pl = ctx.Cache["postitlist"];
                if (pl == null)
                {
                    pl = new PostItList();
                    ctx.Cache["postitlist"] = pl;
                }
                return (PostItList) pl;
            }
        }

        public static TopLabList CachedTopLab
        {
            get
            {
                HttpContext ctx = HttpContext.Current;
                object tl = ctx.Cache["toplablist"];
                if (tl == null)
                {
                    tl = new TopLabList();
                    ctx.Cache["toplablist"] = tl;
                }
                return (TopLabList) tl;
            }
        }
    }
}
