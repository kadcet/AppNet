using AppNET.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNET.App
{
    public interface ILogService
    {
        void Error( string message);

        void Warning( string message);

        void Information( string message);
    }
}
