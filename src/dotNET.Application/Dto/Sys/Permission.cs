namespace dotNET.ICommonServer
{
    /// <summary>
    /// 菜单列表
    /// </summary>
    public class Permission
    {
        public Permission()
        {
            select = false;
            multi = false;
            model = false;
            path = string.Empty;
            url = "#";
            icon = string.Empty;
        }

        public string icon { get; set; }
        public string key { get; set; }
        public bool model { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public string url { get; set; }
        public bool select { get; set; }
        public bool multi { get; set; }
    }
}