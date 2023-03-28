using System;
namespace Intelli.DMS.Api.DTO
{
    public class SystemLogDTO
    {
        public int Id { get; set; }
        public string RequestId { get; set; }
        public int LoggedOn { get; set; }
        public string Level { get; set; }
        public string ClassName { get; set; }
        public string Message { get; set; }
        public string Stacktrace { get; set; }
        public string Exception { get; set; }
    }
}
