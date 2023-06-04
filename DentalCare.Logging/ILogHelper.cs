using System;
using System.Threading.Tasks;

namespace DentalCare.Logging
{
    public interface ILogHelper
    {
        void Debug(string message);
        void Info(string message);
        void Warn(string message);
        void Error(string message);
        void Fatal(string message);
    }

}
