using Intelli.DMS.Api.Helpers;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Intelli.DMS.Api.Services
{
    /// <summary>
    /// The Document Url service implementation
    /// </summary>
    public class DocumentUrlService : IDocumentUrlService
    {

        private readonly IConfiguration _configuration;
        private readonly string _repositoryRoot;

        private readonly IRepository<Batch> _repositoryBatch;
        private readonly IRepository<BatchItem> _repositoryBatchItem;
        private readonly IRepository<BatchItemPage> _repositoryBatchIPage;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentUrlService"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="DMSContext"/> will be injected</param>
        /// <param name="configuration">Instance of <see cref="IConfiguration"/> will be injected</param>
        public DocumentUrlService(DMSContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _repositoryRoot = _configuration.GetSection("BaseURl").Value.TrimEnd('/') + "/" + _configuration.GetSection("DocumentDirectoryAlias").Value;

            _repositoryBatch = new GenericRepository<Batch>(context);
            _repositoryBatchItem = new GenericRepository<BatchItem>(context);
            _repositoryBatchIPage = new GenericRepository<BatchItemPage>(context);
        }


        /// <summary>
        /// Get Document URl .
        /// </summary>
        /// <param name="batchItemId">The Batch Item Id.</param>
        /// <param name="userName">User Name From base Controller .</param>
        /// <returns>A Task of Alert DTO.</returns>
        public async Task<string> GetDocumentUrlByBatchItemId(int batchItemId)
        {

            var batchItem = await _repositoryBatchItem.GetById(batchItemId);

            var batch = await _repositoryBatch.GetById(batchItem.BatchId);


            var filePath = RepositoryHelper.BuildFilePath(_repositoryRoot,
                                                                        batch.Id,
                                                                        batchItem.FileName,
                                                                        batch.CreatedDate);
            return filePath;
        }
    }
}
