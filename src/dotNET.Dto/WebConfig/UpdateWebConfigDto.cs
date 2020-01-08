using System.ComponentModel.DataAnnotations;

namespace dotNET.Dto.WebConfig
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