using AppNET.Domain.Entities;
using AppNET.Domain.Enums;
using AppNET.Domain.Interfaces;
using AppNET.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNET.App
{
    public class LogService : ILogService
    {
        private readonly IRepository<Log> _logRepository;
        public LogService()
        {
            _logRepository = IOCContainer.Resolve<IRepository<Log>>();
        }

        public void Error( string message)
        {
            Log log = new Log();
            log.logType=LogTypes.Error;
            log.LogMessage = message;
            _logRepository.Add(log);
        }

        public void Information(string message)
        {
            Log log = new Log();
            log.logType = LogTypes.Information;
            log.LogMessage = message;
            _logRepository.Add(log);
        }

        public void Warning( string message)
        {
            Log log = new Log();
            log.logType =LogTypes.Warning;
            log.LogMessage = message;
            _logRepository.Add(log);
        }
    }
}
