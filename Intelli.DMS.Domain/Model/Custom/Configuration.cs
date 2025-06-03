#nullable disable

using Intelli.DMS.Domain.Model.Core;

namespace Intelli.DMS.Domain.Model
{
    /// <summary>
    /// The configuration model.
    /// </summary>
    public partial class Configuration : IEntity
    {
        /// <summary>
        /// Gets or sets a value indicating whether is active or not.
        /// </summary>
        bool? IEntity.IsActive
        {
            get
            {
                return IsActive;
            }
            set
            {
                IsActive = value ?? value.Value;
            }
        }
    }
}
