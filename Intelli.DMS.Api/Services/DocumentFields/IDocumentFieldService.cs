using Intelli.DMS.Api.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intelli.DMS.Api.Services.DocumentFields
{
    public interface IDocumentFieldService
    {
        List<UpdatedFieldValuesDTO> GetPrevFieldValues(int batchId);
        Task<List<BatchMetaCheckOutDTO>> UpdateFieldValues(List<BatchMetaCheckOutDTO> dtoList);
    }
}
