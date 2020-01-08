using System.ComponentModel.DataAnnotations;

namespace dotNET.Dto.WebConfig
{
    /// <summary>
    ///
    /// </summary>
    public class DeleteWebConfigDto
    {
        /// <summary>
        ///
        /// </summary>
        [Required]
        public virtual long Id { get; set; }
    }
}