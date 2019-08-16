using System;
using System.Collections.Generic;
using System.Text;

namespace dotNET.Core
{
    public class MenuModel
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public bool IsOpen { get; set; }

        public string Icon { get; set; }

        public string Url { get; set; }

        public string TargetType { get; set; }

        public List<MenuModel> Children { get; set; }
    }
}
