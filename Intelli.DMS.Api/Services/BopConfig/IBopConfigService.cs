using Intelli.DMS.Api.Services.Email.Impl;

namespace Intelli.DMS.Api.Services.BopConfig
{
    public interface IBopConfigService
    {
        // <summary>
        /// get StmtpEmailSettings from bop config table.
        /// </summary>
        /// <returns></returns>
        SmtpEmailSettings getStmtpEmailSettings();
    }
}
