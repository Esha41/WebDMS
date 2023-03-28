using Intelli.DMS.Api.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intelli.DMS.Api.Services
{
    /// <summary>
    /// The Alerts service.
    /// </summary>
    public interface IAlertsService
    {
        /// <summary>
        /// Get the Alerts.
        /// </summary>
        /// <param name="systemUserId">The system user id.</param>
        /// <returns>List of AlertDTO</returns>
        List<AlertDTO> GetAllAlertsBySystemUserId(int systemUserId);

        /// <summary>
        /// Add the Alert.
        /// </summary>
        /// <param name="dto">The Alert dto.</param>
        /// <returns>A Task Alert DTO.</returns>
        AlertDTO AddAlert(AlertDTO dto );

        /// <summary>
        /// Marks alert as read
        /// </summary>
        /// <param name="id">The Alert dto.</param>
        /// <returns>A  Alert DTO.</returns>
        Task< AlertDTO> UpdateAlert(int id);

        /// <summary>
        ///  Add alert to specific role
        /// </summary>
        /// <param name="msg">The Alert msg.</param>
        /// <param name="roleId">The role id.</param>
        /// <returns>A  bool.</returns>
        Task<bool> SendAlertsToRole(string msg, int roleId);

        /// <summary>
        ///  Add alert to specific user
        /// </summary>
        /// <param name="msg">The Alert msg.</param>
        /// <param name="systemUserId">The system user id.</param>
        /// <returns>A  bool.</returns>
        bool SendAlertToUser(string msg, int systemUserId);

        /// <summary>
        ///  Add alert msg to all users in the system which are active
        /// </summary>
        /// <param name="msg">The Alert msg.</param>
        /// <returns>A  bool.</returns>
        Task<bool> BroadcastAlert(string msg);

        /// <summary>
        /// Marks All alert as read of User Id.
        /// </summary>
        /// <param name="userId">The User id.</param>
        /// <returns> </returns>
        void UpdateAlertReadAllAlerts(int userId);

    }
}
