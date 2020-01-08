using System.ComponentModel.DataAnnotations;

namespace dotNET.Web.Host.Model
{
    public class agentModel
    {
        public long Id { get; set; }

        [Display(Name = "名称")]
        [Required(ErrorMessage = "{0}必填")]
        [StringLength(50, ErrorMessage = "{0}最多{1}字符")]
        public string Name { get; set; }

        [Display(Name = "联系电话")]
        [StringLength(50, ErrorMessage = "{0}最多{1}字符")]
        public string ContactNumber { get; set; }

        [Display(Name = "Key")]
        [StringLength(50, ErrorMessage = "{0}最多{1}字符")]
        public string Key { get; set; }

        [Display(Name = "Secret")]
        [StringLength(50, ErrorMessage = "{0}最多{1}字符")]
        public string Secret { get; set; }

        [Display(Name = "状态")]
        public bool State { get; set; }

        [Display(Name = "备注")]
        [StringLength(200, ErrorMessage = "{0}最多{1}字符")]
        public string Remarks { get; set; }
    }
}