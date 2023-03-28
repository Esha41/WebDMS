using System.ComponentModel.DataAnnotations;

namespace Intelli.DMS.Api.DTO
{
    public class FileUploadResultDto
    {
        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        [MaxLength(500)]
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the file url.
        /// </summary>
        [MaxLength(500)]
        public string Url { get; set; }
    }
}
