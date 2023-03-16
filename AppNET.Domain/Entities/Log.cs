using AppNET.Domain.Entities.Base;
using AppNET.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNET.Domain.Entities
{
    public class Log:BaseEntity
    {
        public Log()
        {

            this.LogTime = DateTime.Now;
        }

        public LogTypes logType;
        public string LogMessage;
        public DateTime LogTime;
    }
}
