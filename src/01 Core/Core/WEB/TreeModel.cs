using System.Collections.Generic;
using System.Linq;

namespace CompanyName.ProjectName.Core
{
    public class TreeModel
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
        public string Img { get; set; }
        public long Parentnodes { get; set; }
        public bool Showcheck { get; set; }
        public bool Isexpand { get; set; }
        public bool Complete { get; set; }
        public int Checkstate;
        public bool HasChildren { get; set; }
        public List<TreeModel> ChildNodes { get; set; }

        public static string ToJson(List<TreeModel> data)
        {
            return JsonHelper.SerializeObject(ToTree(data, 0), true, true);
        }

        private static List<TreeModel> ToTree(List<TreeModel> data, long parentId)
        {
            var childNodeList = data.FindAll(t => t.Parentnodes == parentId).ToList();

            var list = new List<TreeModel>();
            foreach (TreeModel item in childNodeList)
            {
                var cdata = data.Where(t => t.Parentnodes == item.Id).ToList();
                item.HasChildren = cdata.Count() > 0;
                if (item.HasChildren)
                {
                    item.Complete = true;
                    item.ChildNodes = ToTree(data, item.Id);
                }
                list.Add(item);
            }
            return list;
        }
    }
}