using Intelli.DMS.Api.DTO;
using System.Threading.Tasks;

namespace Intelli.DMS.Api.Services.ConfigFromDB
{
     /// <summary>
     /// The Config From Database service.
     /// </summary>
    public interface IConfigFromDBService
    {
        /// <summary>
        /// Get Config From Database.
        /// </summary>
        /// <param name="EnumValue">The Enum Value.</param>
        /// <returns>A Task Config .</returns>
        Task<BopConfigsDTO> GetConfigFromDataBase(string EnumValue);
    }
}
