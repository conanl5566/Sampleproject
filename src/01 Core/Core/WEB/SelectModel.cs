using System.Collections.Generic;
using System.Linq;

namespace CompanyName.ProjectName.Core
{
    /// <summary>
    /// 前端Select2模型
    /// </summary>
    public class SelectModel
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public long ParentId { get; set; }

        public static string ToJson(List<SelectModel> data)
        {
            return JsonHelper.SerializeObject(ToSelect(data, 0, ""), true, true);
        }

        private static List<SelectModel> ToSelect(List<SelectModel> data, long parentId, string blank)
        {
            var childNodeList = data.FindAll(t => t.ParentId == parentId).ToList();
            var tabline = "";
            if (parentId != 0)
            {
                tabline = "　　";
            }
            if (childNodeList.Count > 0)
                tabline = tabline + blank;

            var list = new List<SelectModel>();
            foreach (SelectModel item in childNodeList)
            {
                SelectModel select = new SelectModel
                {
                    Id = item.Id,
                    Text = tabline + item.Text,
                    ParentId = item.ParentId
                };
                list.Add(select);
                list.AddRange(ToSelect(data, item.Id, tabline));
            }
            return list;
        }
    }
}