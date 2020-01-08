namespace dotNET.Dto
{
    public class ModuleButtonOption : Option
    {
        public string Search { get; set; }
        public long? ModuleId { get; set; }
        public long? ParentId { get; set; }
        public bool? IsEnabled { get; set; }
    }
}