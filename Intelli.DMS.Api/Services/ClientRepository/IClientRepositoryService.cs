using Intelli.DMS.Api.DTO;
using Intelli.DMS.Domain.Model;
using Intelli.DMS.Domain.Model.Views;
using System.Collections.Generic;

namespace Intelli.DMS.Api.Services.ClientRepository
{
    public interface IClientRepositoryService
    {

        /// <summary>
        /// Get Client Detail With RepositoryId
        /// </summary>
        /// <param name="repositoryId"></param>
        /// <param name="entity"> Instance of <see cref="ClientRepositoryView"/></param>
        /// <returns> ClientDTO </returns>
        ClientDTO GetClientDetailWithRepositoryId(int repositoryId, ClientRepositoryView entity);

        /// <summary>
        /// Make Document Version List and Get Document Version List
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns> List of DocumentVersionListDTO </returns>
        List<DocumentVersionListDTO> MakeDocumentVersionList(int documentId);

        /// <summary>
        /// Make Document Version List and Get Document Version List
        /// </summary>
        /// <param name="batchItemId"></param>
        /// <param name="entity"> Instance of <see cref="BatchItem"/> out parameter</param>
        /// <param name="customer"> Instance of <see cref="Client"/> out parameter</param>
        /// <param name="documentClass"> Instance of <see cref="DocumentClass"/> out parameter</param>
        /// <param name="documentFieldValueDTOs"> List of <see cref="DocumentFieldValueDTO"/> out parameter</param>
        /// <returns> </returns>
        void GetDocumentFieldValues(int batchItemId,
                                            out BatchItem entity,
                                            out Client customer,
                                            out DocumentClass documentClass,
                                            out bool isDocumentClassModifiable,
                                            out List<DocumentFieldValueDTO> documentFieldValueDTOs);

        /// <summary>
        /// Get Client Document With ClientId
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="repositoryRoot"></param>
        /// <returns> ClientDocumentsDTO </returns>
        ClientDocumentsOldDTO GetClientDocumentWithClientId(int clientId, string repositoryRoot);

        /// Get Document Checkout History
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns> List of DocumentCheckOutLogsView </returns>
        List<DocumentCheckOutLogsView> GetDocumentCheckoutHistory(int documentId);

    }
}
