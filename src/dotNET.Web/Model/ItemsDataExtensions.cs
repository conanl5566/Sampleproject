using dotNET.ICommonServer;
using dotNET.CommonServer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace dotNET.Web.Host.Model
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class ItemsDataExtensions
    {
        public static string GetFormattedBreadCrumbValue(this ItemsData department,
        IList<ItemsData> allDepartments,
        string separator = ">>")
        {
            if (department == null)
                throw new ArgumentNullException("Department");

            if (allDepartments == null)
                throw new ArgumentNullException("Department");

            string result = string.Empty;

            var alreadyProcessedDepartmentIds = new List<long>();

            while (department != null &&
                   !alreadyProcessedDepartmentIds.Contains(department.Id)) //prevent circular references
            {
                var categoryId = department.Id.ToString();
                if (String.IsNullOrEmpty(result))
                {
                    result = categoryId;
                }
                else
                {
                    result = string.Format("{0} {1} {2}", categoryId, separator, result);
                }

                alreadyProcessedDepartmentIds.Add(department.Id);

                department = (from c in allDepartments
                              where c.Id == department.ParentId
                              select c).FirstOrDefault();
            }
            return result;
        }

        public static string GetFormattedBreadCrumb(this ItemsData department,
            IList<ItemsData> allDepartments,
            string separator = ">>")
        {
            if (department == null)
                throw new ArgumentNullException("Department");

            if (allDepartments == null)
                throw new ArgumentNullException("Department");

            string result = string.Empty;

            var alreadyProcessedDepartmentIds = new List<long>();

            while (department != null &&
                   !alreadyProcessedDepartmentIds.Contains(department.Id)) //prevent circular references
            {
                var categoryName = department.Name;
                if (String.IsNullOrEmpty(result))
                {
                    result = categoryName;
                }
                else
                {
                    result = string.Format("{0} {1} {2}", categoryName, separator, result);
                }

                alreadyProcessedDepartmentIds.Add(department.Id);

                department = (from c in allDepartments
                              where c.Id == department.ParentId
                              select c).FirstOrDefault();
            }
            return result;
        }

        public static IList<ItemsData> SortDepartmentsForTree(this IList<ItemsData> source, long parentId = 0, bool ignoreDepartmentsWithoutExistingParent = false)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            var result = new List<ItemsData>();

            foreach (var cat in source.ToList().FindAll(c => c.ParentId == parentId))
            {
                result.Add(cat);
                result.AddRange(SortDepartmentsForTree(source, cat.Id, true));
            }
            if (!ignoreDepartmentsWithoutExistingParent && result.Count != source.Count)
            {
                foreach (var cat in source)
                    if (result.Where(x => x.Id == cat.Id).FirstOrDefault() == null)
                        result.Add(cat);
            }
            return result;
        }
    }

    public static class DepartmentExtensions
    {
        public static string GetFormattedBreadCrumbValue(this Department department,
        IList<Department> allDepartments,
        string separator = ">>")
        {
            if (department == null)
                throw new ArgumentNullException("Department");

            if (allDepartments == null)
                throw new ArgumentNullException("Department");

            string result = string.Empty;

            var alreadyProcessedDepartmentIds = new List<long>();

            while (department != null &&
                   !alreadyProcessedDepartmentIds.Contains(department.Id)) //prevent circular references
            {
                var categoryId = department.Id.ToString();
                if (String.IsNullOrEmpty(result))
                {
                    result = categoryId;
                }
                else
                {
                    result = string.Format("{0} {1} {2}", categoryId, separator, result);
                }

                alreadyProcessedDepartmentIds.Add(department.Id);

                department = (from c in allDepartments
                              where c.Id == department.ParentId
                              select c).FirstOrDefault();
            }
            return result;
        }

        public static string GetFormattedBreadCrumb(this Department department,
            IList<Department> allDepartments,
            string separator = ">>")
        {
            if (department == null)
                throw new ArgumentNullException("Department");

            if (allDepartments == null)
                throw new ArgumentNullException("Department");

            string result = string.Empty;

            var alreadyProcessedDepartmentIds = new List<long>();

            while (department != null &&
                   !alreadyProcessedDepartmentIds.Contains(department.Id)) //prevent circular references
            {
                var categoryName = department.Name;
                if (String.IsNullOrEmpty(result))
                {
                    result = categoryName;
                }
                else
                {
                    result = string.Format("{0} {1} {2}", categoryName, separator, result);
                }

                alreadyProcessedDepartmentIds.Add(department.Id);

                department = (from c in allDepartments
                              where c.Id == department.ParentId
                              select c).FirstOrDefault();
            }
            return result;
        }

        public static IList<Department> SortDepartmentsForTree(this IList<Department> source, long parentId = 0, bool ignoreDepartmentsWithoutExistingParent = false)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            var result = new List<Department>();

            foreach (var cat in source.ToList().FindAll(c => c.ParentId == parentId))
            {
                result.Add(cat);
                result.AddRange(SortDepartmentsForTree(source, cat.Id, true));
            }
            if (!ignoreDepartmentsWithoutExistingParent && result.Count != source.Count)
            {
                foreach (var cat in source)
                    if (result.Where(x => x.Id == cat.Id).FirstOrDefault() == null)
                        result.Add(cat);
            }
            return result;
        }
    }
}