using System.ComponentModel.DataAnnotations;

namespace CompanyName.ProjectName.ICommonServer
{
    /// <summary>
    ///
    /// </summary>
    public class UpdateWebConfigDto : CreateWebConfigDto
    {
        /// <summary>
        ///
        /// </summary>
        [Required]
        public virtual long Id { get; set; }
    }
}