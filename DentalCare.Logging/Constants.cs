using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Logging
{
    public class Constants
    {
        public static string DEF_FATALERROR_MESSAGE =
            Environment.NewLine + @" -------------- FATAL ERROR --------------- " + Environment.NewLine + "{0}";

        public static readonly string DEF_SYSTEMNOTIFICATION_MESSAGE =
            Environment.NewLine + @" --------- SYSTEM NOTIFICATION ------------ " + Environment.NewLine + "{0}";

        public static readonly string DEF_WARRNING_MESSAGE =
            Environment.NewLine + @" ---------------- WARNING ----------------- " + Environment.NewLine + "{0}";

        public static readonly string DEF_INFO_MESSAGE =
            Environment.NewLine + @" -------------- NOTIFICATION -------------- " + Environment.NewLine + "{0}";

        public static readonly string DEF_ERROR_MESSAGE =
            Environment.NewLine + @" -------------- ERROR --------------------- " + Environment.NewLine + "{0}";

        public static readonly string DEF_DEBUG_MESSAGE =
            Environment.NewLine + @" -------------- DEBUG --------------------- " + Environment.NewLine + "{0}";

    }
}
