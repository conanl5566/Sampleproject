using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace dotNET.Core
{
    /// <summary>
    /// 文件导入导出
    /// </summary>
    public class Export
    {
        #region 导出excel
        /// <summary>
        /// 导出excel
        /// </summary>
        public  async static Task ExportExcel<T>( List<T> list, string format, string reportName,   string path, List<string> format2 = null)
        {



         //   string gp = Path.Combine( "Export", Guid.NewGuid().ToString());
            var newFile = path;// Path.Combine(Path.Combine("wwwroot" , gp )   , reportName + @".xlsx");
         //   new FileHelper().CreateFiles(Path.Combine("wwwroot" , gp), true);
         //   path = Path.Combine(gp ,reportName + @".xlsx");

            using (var fs = new FileStream(newFile, FileMode.Create, FileAccess.Write))
            {

                IWorkbook workbook = new XSSFWorkbook();

                /////////////////////////


                ICellStyle cellstyle = workbook.CreateCellStyle();
                cellstyle.Alignment = HorizontalAlignment.Center;
                cellstyle.VerticalAlignment = VerticalAlignment.Center;
                int num = list.Count / 60000;
                int num2 = 60000;

                for (int i = 0; i <= num; i++)
                {
                    #region MyRegion
                    List<T> list2 = list.Skip(i * num2).Take(num2).ToList<T>();
                    string text = reportName + ((i == 0) ? "" : ("-" + i.ToString()));
                    ISheet sheet = workbook.CreateSheet(text);
                    #region 标题
                    IRow row = sheet.CreateRow(0);
                    for (int j = 0; j < format.Split(new char[]
                    {
                    ';'
                    }).Count<string>(); j++)
                    {
                        string text2 = format.Split(new char[]
                        {
                        ';'
                        })[j];
                        string cellValue = text2.Split(new char[]
                        {
                        '|'
                        })[1];
                        ICell cell = row.CreateCell(j);
                        cell.SetCellValue(cellValue);
                    }
                    #endregion
                    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
                    #region MyRegion
                    for (int j = 0; j < list2.Count; j++)
                    {
                        IRow row2 = sheet.CreateRow(j + 1);
                        #region MyRegion
                        for (int k = 0; k < format.Split(new char[]
                                    {
                        ';'
                                    }).Count<string>(); k++)
                        {
                            string text2 = format.Split(new char[]
                            {
                            ';'
                            })[k];
                            string text3 = text2.Split(new char[]
                            {
                            '|'
                            })[0];
                            if (properties.Count <= 0)
                            {
                                return;
                            }
                            #region MyRegion
                            for (int l = 0; l < properties.Count; l++)
                            {
                                if (properties[l].Name == text3)
                                {
                                    ICell cell = row2.CreateCell(k);
                                    string text4 = string.Empty;
                                    if (properties[text3].GetValue(list2[j]) != null)
                                    {
                                        text4 = properties[text3].GetValue(list2[j]).ToString();
                                    }
                                    Type propertyType = properties[text3].PropertyType;
                                    if (propertyType == typeof(decimal) || propertyType == typeof(int) || propertyType == typeof(double))
                                    {
                                        cell.SetCellValue(Convert.ToDouble(properties[text3].GetValue(list2[j])));
                                    }
                                    else
                                    {
                                        if (properties[text3].GetValue(list2[j]) != null)
                                            cell.SetCellValue(properties[text3].GetValue(list2[j]).ToString());
                                        else
                                        {
                                            cell.SetCellValue(string.Empty);
                                        }
                                    }
                                }


                            }

                            #endregion

                        }
                        #endregion
                    }
                    #endregion 
                    #endregion



                    sheet.ForceFormulaRecalculation = true;

                    #region MyRegion
                    if (num == i)
                    {
                        if (format2 != null && format2.Count() > 0)
                        {
                            int z = list2.Count;

                            foreach (var item in format2)
                            {
                                IRow row3 = sheet.CreateRow(z + 1);
                                #region MyRegion
                                for (int j = 0; j < format.Split(new char[]
                                                {
                                    ';'
                                                }).Count<string>(); j++)
                                {
                                }
                                #endregion

                                #region 合并
                                for (int j = 0; j < item.Split(new char[]
                                                {
                                    ';'
                                                }).Count<string>(); j++)
                                {


                                    string text2 = item.Split(new char[] { ';' })[j];


                                    //值
                                    string cellValue = text2.Split(new char[] { '|' })[1];


                                    string cellindex = text2.Split(new char[] { '|' })[0];
                                    var arr = cellindex.Split(new char[] { '-' });
                                    //合并
                                    CellRangeAddress cellRangeAddress = new CellRangeAddress(z + 1, z + 1, ZConvert.StrToInt(arr[0]), ZConvert.StrToInt(arr[1]));
                                    sheet.AddMergedRegion(cellRangeAddress);

                                    ICell cell = row3.CreateCell(ZConvert.StrToInt(arr[0]));

                                    ICellStyle cellstyle2 = workbook.CreateCellStyle();
                                    cellstyle2.Alignment = HorizontalAlignment.Right;
                                    cellstyle2.VerticalAlignment = VerticalAlignment.Center;
                                    cell.CellStyle = cellstyle2;
                                    cell.SetCellValue(cellValue);
                                }
                                #endregion


                                z++;

                            }
                        }
                    }
                    #endregion

                }


                ////////////////////////////////







                workbook.Write(fs);
            }

        }

        #endregion

        #region 导出excel 合并单元格的
        /// <summary>
        /// 导出excel  合并单元格的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="list"></param>
        /// <param name="format"></param>
        /// <param name="reportName"></param>
        /// <param name="MergeField"></param>
        /// <param name="format2"></param>
        public async static Task ExportExcelMergeField<T>( List<T> list, string format, string reportName, string MergeField, string path, List<string> format2 = null)
        {
          //  Thread.Sleep(999999);
            //string gp = "Export/" + Guid.NewGuid().ToString() + "/";
            //var newFile = "wwwroot/"+ gp  + reportName + @".xlsx";
            //new FileHelper().CreateFiles("wwwroot/" + gp, true);
            //path = gp + reportName + @".xlsx";


            // string gp = Path.Combine("Export", Guid.NewGuid().ToString());
            var newFile = path;// Path.Combine(Path.Combine("wwwroot", gp), reportName + @".xlsx");
         //   new FileHelper().CreateFiles(Path.Combine("wwwroot", gp), true);
         //   path = Path.Combine(gp, reportName + @".xlsx");

            using (var fs = new FileStream(newFile, FileMode.Create, FileAccess.Write))
            {

                IWorkbook workbook = new XSSFWorkbook();

                /////////////////////////


                ICellStyle cellstyle2 = workbook.CreateCellStyle();
                cellstyle2.Alignment = HorizontalAlignment.Center;
                cellstyle2.VerticalAlignment = VerticalAlignment.Center;
                int num = list.Count / 25000;
                int num2 = 25000;

                for (int i = 0; i <= num; i++)
                {



                    List<T> list2 = list.Skip(i * num2).Take(num2).ToList<T>();
                    string text = reportName + ((i == 0) ? "" : ("-" + i.ToString()));
                    ISheet sheet = workbook.CreateSheet(text);
                    IRow row = sheet.CreateRow(0);
                    for (int j = 0; j < format.Split(new char[]
                    {
                    ';'
                    }).Count<string>(); j++)
                    {
                        string text2 = format.Split(new char[]
                        {
                        ';'
                        })[j];
                        string cellValue = text2.Split(new char[]
                        {
                        '|'
                        })[1];
                        ICell cell = row.CreateCell(j);
                        cell.CellStyle = cellstyle2;
                        cell.SetCellValue(cellValue);
                    }
                    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
                    int column = 0;
                    for (int j = 0; j < list2.Count; j++)
                    {
                        int count = 1;
                        if (properties[MergeField].GetValue(list2[j]) != null)
                        {
                            count = properties[MergeField].GetValue(list2[j]).ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Count();
                        }
                        IRow row2 = sheet.CreateRow(column + 1);
                        for (int k = 0; k < format.Split(new char[]
                        {
                        ';'
                        }).Count<string>(); k++)
                        {
                            string text2 = format.Split(new char[]
                            {
                            ';'
                            })[k];
                            string text3 = text2.Split(new char[]
                            {
                            '|'
                            })[0];
                            if (properties.Count <= 0)
                            {
                                return;
                            }
                            List<string> df = new List<string>();
                            for (int l = 0; l < properties.Count; l++)
                            {

                                if (properties[MergeField].GetValue(list2[j]) != null)
                                {
                                    if (df.Count() == 0)
                                    {

                                        string[] teshu = properties[MergeField].GetValue(list2[j]).ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                                        foreach (var te in teshu)
                                        {
                                            string[] t = te.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                            foreach (var er in t)
                                            {
                                                df.Add(er.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries)[0].ToString());
                                            }
                                            break;
                                        }
                                    }
                                    if (df.Contains(text3))
                                    {
                                        int sum = column + 1;
                                        string[] teshu = properties[MergeField].GetValue(list2[j]).ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                                        foreach (var te in teshu)
                                        {
                                            string[] t = te.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                            foreach (var er in t)
                                            {
                                                string[] values = er.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                                                if (values[0] == text3)
                                                {
                                                    IRow row3 = sheet.GetRow(sum);
                                                    if (row3 == null)
                                                        row3 = sheet.CreateRow(sum);
                                                    ICell cell = row3.CreateCell(k);
                                                    cell.CellStyle = cellstyle2;
                                                    cell.SetCellValue(values[1]);
                                                }

                                            }
                                            sum++;
                                        }
                                    }
                                }
                                if (properties[l].Name == text3)
                                {
                                    CellRangeAddress range = new CellRangeAddress(column + 1, column + count, k, k);
                                    sheet.AddMergedRegion(range);
                                    ICell cell = row2.CreateCell(k);
                                    cell.CellStyle = cellstyle2;
                                    string text4 = string.Empty;
                                    if (properties[text3].GetValue(list2[j]) != null)
                                    {
                                        text4 = properties[text3].GetValue(list2[j]).ToString();
                                    }
                                    Type propertyType = properties[text3].PropertyType;
                                    if (propertyType == typeof(decimal) || propertyType == typeof(int) || propertyType == typeof(double))
                                    {
                                        cell.SetCellValue(Convert.ToDouble(properties[text3].GetValue(list2[j])));
                                    }
                                    else
                                    {
                                        if (properties[text3].GetValue(list2[j]) != null)
                                            cell.SetCellValue(properties[text3].GetValue(list2[j]).ToString());
                                        else
                                        {
                                            cell.SetCellValue(string.Empty);
                                        }
                                    }
                                }
                            }
                        }
                        column = count + column;

                        sheet.ForceFormulaRecalculation = true;

                        #region MyRegion
                        if (num == i)
                        {
                            if (format2 != null && format2.Count() > 0)
                            {
                                int z = list2.Count;

                                foreach (var item in format2)
                                {
                                    IRow row3 = sheet.CreateRow(z + 1);
                                    #region MyRegion
                                    for (int j2 = 0; j2 < format.Split(new char[]
                                                    {
                                    ';'
                                                    }).Count<string>(); j2++)
                                    {
                                    }
                                    #endregion

                                    #region 合并
                                    for (int j3 = 0; j3 < item.Split(new char[]
                                                    {
                                    ';'
                                                    }).Count<string>(); j3++)
                                    {


                                        string text2 = item.Split(new char[] { ';' })[j3];


                                        //值
                                        string cellValue = text2.Split(new char[] { '|' })[1];


                                        string cellindex = text2.Split(new char[] { '|' })[0];
                                        var arr = cellindex.Split(new char[] { '-' });
                                        //合并
                                        CellRangeAddress cellRangeAddress = new CellRangeAddress(z + 1, z + 1, ZConvert.StrToInt(arr[0]), ZConvert.StrToInt(arr[1]));
                                        sheet.AddMergedRegion(cellRangeAddress);

                                        ICell cell = row3.CreateCell(ZConvert.StrToInt(arr[0]));

                                        ICellStyle cellstyle = workbook.CreateCellStyle();
                                        cellstyle.Alignment = HorizontalAlignment.Right;
                                        cellstyle.VerticalAlignment = VerticalAlignment.Center;
                                        cell.CellStyle = cellstyle;
                                        cell.SetCellValue(cellValue);
                                    }
                                    #endregion


                                    z++;

                                }
                            }
                        }
                        #endregion

                    }


                    ////////////////////////////////







                    workbook.Write(fs);
                }

            }





        }

        #endregion
    }
  }