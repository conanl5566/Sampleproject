using System.ComponentModel.DataAnnotations;

namespace dotNET.ICommonServer
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