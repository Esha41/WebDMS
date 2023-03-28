using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
namespace Intelli.DMS.Api.Helpers
{
    public class NLogCustomPropertyHelper
    {
        /// <summary>
        /// Send Request Id as custom property to NLog
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="RequestId"></param>
        /// <param name="message"></param>
        public static void LogErrorWithRequestId(ILogger logger, string RequestId, string message)
        {
            Dictionary<string, object> config = AddRequestIdInLogs(RequestId);
            using (logger.BeginScope(config))
            {
                logger.LogError(message);
            }
        }
        /// <summary>
        /// Send Request Id as custom property to NLog
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="RequestId"></param>
        /// <param name="message"></param>
        public static void LogInformationWithRequestId(ILogger logger, string RequestId, string message)
        {
            Dictionary<string, object> config = AddRequestIdInLogs(RequestId);
            using (logger.BeginScope(config))
            {
                logger.LogInformation(message);
            }
        }

        private static Dictionary<string, object> AddRequestIdInLogs(string RequestId)
        {
            return new Dictionary<string, object>()
            {
                {
                    "BatchRequestId",
                     RequestId
                }
            };
        }
    }
}
