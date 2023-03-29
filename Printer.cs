using ConsoleTables;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Principal;
using System.Diagnostics;
using System.Runtime.InteropServices;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Globalization;
using System.Drawing;

namespace Sorting
{
    /// <summary>
    /// The class Printer is created to print the array and algortihms information to the files.<br/>
    /// At this moment it can create txt- and excel- reports.<br/>
    /// How to use it?   
    /// <code>
    /// AdvancedArray &lt; sbyte &gt; sbyteArray = new AdvancedArray &lt; sbyte &gt;(100); // Creating an array with the class AdvancedArray
    /// 
    /// object arrayBefore = sbyteArray.Clone(); 
    /// 
    /// Algorithm &lt; sbyte &gt; quickSort = new QuickSor &lt; sbyte &gt; ();
    /// Algorithm &lt; sbyte &gt; mergeSort = new MergeSort &lt; sbyte &gt; (); // Using two sorting algorithms
    /// 
    /// quickSort.Sort(sbyteArray.TArray);
    /// arrayAfter.TArray = mergeSort.Sort(sbyteArray.TArray); // Sorting and saving the second reference to the output array
    /// 
    /// object arrayAfter = sbyteArray; // Saving the references to the input and output array.
    /// 
    /// /*
    ///  * Here we adding those algorithms to the AlgorithmsList.
    ///  * We can add 1 or more algorithms.
    ///  * If we don't add any algorithm, the report will be without the part of the algorithm information.
    ///  */
    /// PrintableAlgorithmInfo printableAlgorithmInfo = new PrintableAlgorithmInfo();
    /// printableAlgorithmInfo.AlgorithmsList.Add(quickSort);
    /// printableAlgorithmInfo.AlgorithmsList.Add(mergeSort);
    /// 
    /// PrintableArrayInfo printableArrayInfo = new PrintableArrayInfo(arrayBefore, arrayAfter); // Saving our two arrays to the creating report.
    /// 
    /// /*
    ///  * The main part: creating a new object of Printer with arrays and algorithms.
    ///  * Next part: method Print with parameters:
    ///  * Format.Txt or Format.Excel - an obligatory parameter
    ///  * startAfterPrint - if true the report will open after creating, an obligatory parameter
    ///  * ReportType.WithArray or ReportType.WithoutArray - will the report contain arrays (It can take a while if we use large arrays). Default value - ReportType.WithArray.
    ///  * elementsPerRow - how many array elements will be in the line. Default value - 8000.
    ///  * It may be complicated to print large arrays (with the size more 1_000_000) and then you should not print arrays (set the parameter ReportType.WithoutArray).
    ///  */
    /// Printer printer = new Printer(printableArrayInfo, printableAlgorithmInfo);
    /// printer.Print(Format.Txt, true, ReportType.WithArray, 8000);
    /// printer.Print(Format.Excel, true, ReportType.WithArray, 8000);
    /// </code>
    /// </summary>
    public class Printer
    {
        private readonly string ReportsFolderName = Resource.ReportsFolderName;
        public ArraysInfo ArrayInfo { get; set; }
        public AlgorithmsInfo AlgorithmInfo { get; set; }
        private string UserDocumentsPath { get; set; }
        private string SortingReportsFolderPath { get; set; }

        public Printer(ArraysInfo arrayInfo, AlgorithmsInfo algorithmInfo)
        {
            UserDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            SortingReportsFolderPath = Path.Combine(UserDocumentsPath, ReportsFolderName);
            ArrayInfo = arrayInfo;
            AlgorithmInfo = algorithmInfo;
        }

        public void Print(Format format, bool startAfterPrint = false, ReportType reportType = ReportType.WithArray, int elementsPerRow = 8000)
        {
            if (!Directory.Exists(SortingReportsFolderPath))
            {
                Directory.CreateDirectory(SortingReportsFolderPath);
            }
            string FileName = GenerateFileName(format);
            string FilePath = Path.Combine(SortingReportsFolderPath, FileName);
            if (File.Exists(FilePath))
            {
                FilePath = FilePath.Insert(FilePath.LastIndexOf('.'), Resource.New);
            }
            if (format.Equals(Format.Txt))
            {
                GenerateTextReport(FilePath, reportType);
            }
            else if (format.Equals(Format.Excel))
            {
                GenerateExcelReport(FilePath, elementsPerRow, reportType);
            }
            if (startAfterPrint)
            {
                OpenFile(FilePath);
            }
        }

        private void GenerateTextReport(string path, ReportType reportType)
        {
            using FileStream fs = File.Create(path);
            fs.Close();

            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                byte[] buffer = Encoding.Default.GetBytes(GenerateStringReport(reportType).ToString());
                fileStream.Write(buffer);
            }
        }

        public string GenerateStringReport(ReportType reportType)
        {
            StringBuilder report = new StringBuilder();
            StringBuilder tempBuilder = new StringBuilder();

            // Common information
            report.AppendLine(string.Concat(Enumerable.Repeat("*", 80)));
            report.AppendLine(Resource.Report + ": " + Resource.ApplicationName);
            report.AppendLine(Resource.Created + ": " + DateTime.Now);
            report.AppendLine(Resource.User + ": " + GetUserName());
            report.AppendLine(string.Concat(Enumerable.Repeat("*", 80)));
            report.AppendLine();

            // Input array information
            report.AppendLine(string.Concat(Enumerable.Repeat("*", 80)));
            report.AppendLine(Resource.InputArray + ": ");
            string temp = string.Empty;
            if (reportType.Equals(ReportType.WithArray))
            {
                foreach (dynamic element in ArrayInfo.InputArray)
                {
                    tempBuilder.Append(element + " ");
                }
                tempBuilder.Length--;
                temp = tempBuilder.ToString();
                temp = Regex.Replace(temp, "(?<=\\G.{80})(?=.)", "\n");
                report.AppendLine(temp);
            }
            report.AppendLine(Resource.InputArrayLength + ": " + ArrayInfo.InputArraySize);
            report.AppendLine(Resource.IsArraySorted + ": " + (ArrayInfo.IsInputArraySorted ? Resource.True : Resource.False));
            report.AppendLine(Resource.ArrayType + ": " + GetArrayType(ArrayInfo.InputArrayType));

            // Output array information
            report.AppendLine("");
            report.AppendLine(Resource.OutputArray + ": ");
            temp = string.Empty;
            tempBuilder = new StringBuilder();
            if (reportType.Equals(ReportType.WithArray))
            {
                foreach (dynamic element in ArrayInfo.OutputArray)
                {
                    tempBuilder.Append(element + " ");
                }
                tempBuilder.Length--;
                temp = tempBuilder.ToString();
                temp = Regex.Replace(temp, "(?<=\\G.{80})(?=.)", "\n");
                report.AppendLine(temp);
            }
            report.AppendLine(Resource.OutputArrayLength + ": " + ArrayInfo.OutputArraySize);
            report.AppendLine(Resource.IsArraySorted + ": " + (ArrayInfo.IsOutputArraySorted ? Resource.True : Resource.False));
            report.AppendLine(Resource.ArrayType + ": " + GetArrayType(ArrayInfo.OutputArrayType));
            report.AppendLine(string.Concat(Enumerable.Repeat("*", 80)));

            // 1 Algorithm information
            report.AppendLine("");
            report.AppendLine(string.Concat(Enumerable.Repeat("*", 80)));
            if (AlgorithmInfo.AlgorithmsList.Count == 1)
            {
                report.AppendLine(Resource.SortingAlgorithm + ": " + GetAlgorithmName(AlgorithmInfo.AlgorithmsList[0].Name));
                report.AppendLine(Resource.Time + ": " + AlgorithmInfo.AlgorithmsList[0].Time.ElapsedMilliseconds + " " + Resource.ms);
                report.AppendLine(Resource.IsTheAlgorithmStabil + ": " + (AlgorithmInfo.AlgorithmsList[0].IsStabil ? Resource.True : Resource.False));
                report.AppendLine(Resource.Comparisons + ": " + AlgorithmInfo.AlgorithmsList[0].Comparisons);
                report.AppendLine(Resource.Swaps + ": " + AlgorithmInfo.AlgorithmsList[0].Swaps);
            }

            // 2 or more algorithms information
            if (AlgorithmInfo.AlgorithmsList.Count > 1)
            {
                ConsoleTable table = new ConsoleTable(Resource.SortingAlgorithm,
                    Resource.Time, Resource.IsTheAlgorithmStabil, Resource.Comparisons, Resource.Swaps);
                foreach (dynamic algorithm in AlgorithmInfo.AlgorithmsList)
                {
                    table.AddRow(GetAlgorithmName(algorithm.Name), algorithm.Time.ElapsedMilliseconds,
                        algorithm.IsStabil ? Resource.True : Resource.False, algorithm.Comparisons, algorithm.Swaps);
                }
                report.AppendLine(table.ToString());
            }
            return report.ToString();
        }

        private void GenerateExcelReport(string path, int elementsPerRow, ReportType excelReportType)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(Resource.Report);

                (int row, int column) lastCoordinates = AddExcelCommonInformation(worksheet, 1, 1);
                lastCoordinates = AddInputArrayInformation(worksheet, lastCoordinates.row + 2, 1, elementsPerRow, excelReportType);
                lastCoordinates = AddOutputArrayInformation(worksheet, lastCoordinates.row + 1, 1, elementsPerRow, excelReportType);

                if (AlgorithmInfo.AlgorithmsList.Count == 1)
                {
                    lastCoordinates = AddAlgorithmInformation(worksheet, lastCoordinates.row + 1, 1);
                }
                else if (AlgorithmInfo.AlgorithmsList.Count > 1)
                {
                    lastCoordinates = AddAlgorithmsInformation(worksheet, lastCoordinates.row + 1, 1);
                }

                worksheet.Cells.AutoFitColumns();
                FileInfo xlFile = new FileInfo(path);
                package.SaveAs(xlFile);
            }
        }

        private (int row, int column) AddExcelCommonInformation(ExcelWorksheet excelWorksheet, int startRow, int startCol)
        {
            excelWorksheet.Cells[startRow, startCol].Value = Resource.Report + ":";
            excelWorksheet.Cells[startRow, startCol + 1].Value = Resource.ApplicationName;
            excelWorksheet.Cells[startRow + 1, startCol].Value = Resource.Created + ":";
            excelWorksheet.Cells[startRow + 1, startCol + 1].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.FullDateTimePattern;
            excelWorksheet.Cells[startRow + 1, startCol + 1].Value = DateTime.Now.ToString();
            excelWorksheet.Cells[startRow + 2, startCol].Value = Resource.User + ":";
            excelWorksheet.Cells[startRow + 2, startCol + 1].Value = GetUserName();
            excelWorksheet.Cells[startRow, startCol, startRow + 2, startCol].Style.Fill.PatternType = ExcelFillStyle.Solid;
            excelWorksheet.Cells[startRow, startCol, startRow + 2, startCol].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#C5DEDD"));
            BorderCell(excelWorksheet, startRow, startCol, startRow + 2, startCol + 1);
            return (startRow + 2, startCol + 1);
        }

        private (int row, int column) AddInputArrayInformation(ExcelWorksheet excelWorksheet, int startRow, int startCol, int elementsPerRow, ReportType reportType)
        {
            int rowCounter = default;
            excelWorksheet.Cells[startRow, startCol].Value = Resource.InputArray + ":";
            excelWorksheet.Cells[startRow + 1, startCol].Value = Resource.InputArrayLength + ":";
            excelWorksheet.Cells[startRow + 1, startCol + 1].Value = ArrayInfo.InputArraySize;
            excelWorksheet.Cells[startRow + 2, startCol].Value = Resource.IsArraySorted + ":";
            excelWorksheet.Cells[startRow + 2, startCol + 1].Value = ArrayInfo.IsInputArraySorted ? Resource.True : Resource.False;
            excelWorksheet.Cells[startRow + 3, startCol].Value = Resource.ArrayType + ":";
            excelWorksheet.Cells[startRow + 3, startCol + 1].Value = GetArrayType(ArrayInfo.InputArrayType);
            excelWorksheet.Cells[startRow, startCol, startRow + 3, startCol].Style.Fill.PatternType = ExcelFillStyle.Solid;
            excelWorksheet.Cells[startRow, startCol, startRow + 3, startCol].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#BCD4E6"));
            BorderCell(excelWorksheet, startRow, startCol);
            BorderCell(excelWorksheet, startRow + 1, startCol, startRow + 3, startCol + 1);

            if (reportType.Equals(ReportType.WithArray))
            {
                for (int i = 0, j = startRow + 4, k = startCol; i < ArrayInfo.InputArray.Length; i++)
                {
                    excelWorksheet.Cells[j, k].Value = ArrayInfo.InputArray[i];
                    BorderCell(excelWorksheet, j, k);
                    if (k >= elementsPerRow)
                    {
                        j++;
                        k = 1;
                        rowCounter = j;
                    }
                    else
                    {
                        k++;
                        rowCounter = j + 1;
                    }
                }
            }
            else
            {
                rowCounter = startRow + 4;
            }
            return (rowCounter, elementsPerRow);
        }

        private (int row, int column) AddOutputArrayInformation(ExcelWorksheet excelWorksheet, int startRow, int startCol, int elementsPerRow, ReportType reportType)
        {
            int rowCounter = default;
            excelWorksheet.Cells[startRow, startCol].Value = Resource.OutputArray + ":";
            excelWorksheet.Cells[startRow + 1, startCol].Value = Resource.OutputArrayLength + ":";
            excelWorksheet.Cells[startRow + 1, startCol + 1].Value = ArrayInfo.OutputArraySize;
            excelWorksheet.Cells[startRow + 2, startCol].Value = Resource.IsArraySorted + ":";
            excelWorksheet.Cells[startRow + 2, startCol + 1].Value = ArrayInfo.IsOutputArraySorted ? Resource.True : Resource.False;
            excelWorksheet.Cells[startRow + 3, startCol].Value = Resource.ArrayType + ":";
            excelWorksheet.Cells[startRow + 3, startCol + 1].Value = GetArrayType(ArrayInfo.OutputArrayType);
            excelWorksheet.Cells[startRow, startCol, startRow + 3, startCol].Style.Fill.PatternType = ExcelFillStyle.Solid;
            excelWorksheet.Cells[startRow, startCol, startRow + 3, startCol].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FAD2E1"));
            BorderCell(excelWorksheet, startRow, 1);
            BorderCell(excelWorksheet, startRow + 1, startCol, startRow + 3, startCol + 1);

            if (reportType.Equals(ReportType.WithArray))
            {
                for (int i = 0, j = startRow + 4, k = 1; i < ArrayInfo.OutputArray.Length; i++)
                {
                    excelWorksheet.Cells[j, k].Value = ArrayInfo.OutputArray[i];
                    BorderCell(excelWorksheet, j, k);
                    if (k >= elementsPerRow)
                    {
                        j++;
                        k = 1;
                        rowCounter = j;
                    }
                    else
                    {
                        k++;
                        rowCounter = j + 1;
                    }
                }
            }
            else
            {
                rowCounter = startRow + 4;
            }
            return (rowCounter, elementsPerRow);
        }

        private (int row, int column) AddAlgorithmInformation(ExcelWorksheet excelWorksheet, int startRow, int startCol)
        {
            excelWorksheet.Cells[startRow, startCol].Value = Resource.SortingAlgorithm + ":";
            excelWorksheet.Cells[startRow, startCol + 1].Value = GetAlgorithmName(AlgorithmInfo.AlgorithmsList[0].Name);
            excelWorksheet.Cells[startRow + 1, startCol].Value = Resource.Time + ":";
            excelWorksheet.Cells[startRow + 1, startCol + 1].Value = AlgorithmInfo.AlgorithmsList[0].Time.ElapsedMilliseconds;
            excelWorksheet.Cells[startRow + 1, startCol + 2].Value = Resource.ms;
            excelWorksheet.Cells[startRow + 2, startCol].Value = Resource.IsTheAlgorithmStabil + ":";
            excelWorksheet.Cells[startRow + 2, startCol + 1].Value = AlgorithmInfo.AlgorithmsList[0].IsStabil ? Resource.True : Resource.False;
            excelWorksheet.Cells[startRow + 3, startCol].Value = Resource.Comparisons + ":";
            excelWorksheet.Cells[startRow + 3, startCol + 1].Value = AlgorithmInfo.AlgorithmsList[0].Comparisons;
            excelWorksheet.Cells[startRow + 4, startCol].Value = Resource.Swaps + ":";
            excelWorksheet.Cells[startRow + 4, startCol + 1].Value = AlgorithmInfo.AlgorithmsList[0].Swaps;

            excelWorksheet.Cells[startRow + 5, startCol, startRow + 7, startCol].Merge = true;
            excelWorksheet.Cells[startRow + 5, startCol].Value = Resource.TimeComplexity + ":";
            excelWorksheet.Cells[startRow + 5, startCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            excelWorksheet.Cells[startRow + 5, startCol].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            excelWorksheet.Cells[startRow + 5, startCol].Style.WrapText = true;

            excelWorksheet.Cells[startRow + 5, startCol + 1].Value = Resource.BestCase + ":";
            excelWorksheet.Cells[startRow + 5, startCol + 2].Value = AlgorithmInfo.AlgorithmsList[0].BestCase;
            excelWorksheet.Cells[startRow + 6, startCol + 1].Value = Resource.AverageCase + ":";
            excelWorksheet.Cells[startRow + 6, startCol + 2].Value = AlgorithmInfo.AlgorithmsList[0].AverageCase;
            excelWorksheet.Cells[startRow + 7, startCol + 1].Value = Resource.WorstCase + ":";
            excelWorksheet.Cells[startRow + 7, startCol + 2].Value = AlgorithmInfo.AlgorithmsList[0].WorstCase;

            excelWorksheet.Cells[startRow + 8, startCol].Value = Resource.SpaceComplexity + ":";
            excelWorksheet.Cells[startRow + 8, startCol + 1].Value = Resource.WorstCase + ":";
            excelWorksheet.Cells[startRow + 8, startCol + 2].Value = AlgorithmInfo.AlgorithmsList[0].WorstCaseSpaceComplexity;

            excelWorksheet.Cells[startRow, 1, startRow + 8, startCol].Style.Fill.PatternType = ExcelFillStyle.Solid;
            excelWorksheet.Cells[startRow, 1, startRow + 8, startCol].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFF1E6"));
            BorderCell(excelWorksheet, startRow, 1, startRow + 8, startCol + 2);

            return (startRow + 7, startCol + 1);
        }

        private (int row, int column) AddAlgorithmsInformation(ExcelWorksheet excelWorksheet, int startRow, int startCol)
        {
            int rowCounter = default;
            excelWorksheet.Cells[startRow, startCol, startRow + 1, startCol].Merge = true;
            excelWorksheet.Cells[startRow, startCol].Value = Resource.SortingAlgorithm;

            excelWorksheet.Cells[startRow, startCol + 1, startRow + 1, startCol + 1].Merge = true;
            excelWorksheet.Cells[startRow, startCol + 1].Value = Resource.Time + " (" + Resource.ms + ")";

            excelWorksheet.Cells[startRow, startCol + 2, startRow + 1, startCol + 2].Merge = true;
            excelWorksheet.Cells[startRow, startCol + 2].Value = Resource.IsTheAlgorithmStabil;
            excelWorksheet.Cells[startRow, startCol + 2].Style.WrapText = true;

            excelWorksheet.Cells[startRow, startCol + 3, startRow + 1, startCol + 3].Merge = true;
            excelWorksheet.Cells[startRow, startCol + 3].Value = Resource.Comparisons;
            excelWorksheet.Cells[startRow, startCol + 3].Style.WrapText = true;

            excelWorksheet.Cells[startRow, startCol + 4, startRow + 1, startCol + 4].Merge = true;
            excelWorksheet.Cells[startRow, startCol + 4].Value = Resource.Swaps;

            excelWorksheet.Cells[startRow, startCol + 5, startRow, startCol + 7].Merge = true;
            excelWorksheet.Cells[startRow, startCol + 5].Value = Resource.TimeComplexity;
            excelWorksheet.Cells[startRow, startCol + 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            excelWorksheet.Cells[startRow + 1, startCol + 5].Value = Resource.BestCase + ":";
            excelWorksheet.Cells[startRow + 1, startCol + 6].Value = Resource.AverageCase + ":";
            excelWorksheet.Cells[startRow + 1, startCol + 7].Value = Resource.WorstCase + ":";

            excelWorksheet.Cells[startRow, startCol + 8].Value = Resource.SpaceComplexity + ":";
            excelWorksheet.Cells[startRow + 1, startCol + 8].Value = Resource.WorstCase + ":";

            excelWorksheet.Cells[startRow, startCol, startRow + 1, startCol + 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
            excelWorksheet.Cells[startRow, startCol, startRow + 1, startCol + 8].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFF1E6"));

            foreach (dynamic algorithm in AlgorithmInfo.AlgorithmsList)
            {
                excelWorksheet.Cells[rowCounter + startRow + 2, startCol].Value = GetAlgorithmName(algorithm.Name);
                excelWorksheet.Cells[rowCounter + startRow + 2, startCol + 1].Value = algorithm.Time.ElapsedMilliseconds;
                excelWorksheet.Cells[rowCounter + startRow + 2, startCol + 2].Value = algorithm.IsStabil ? Resource.True : Resource.False;
                excelWorksheet.Cells[rowCounter + startRow + 2, startCol + 3].Value = algorithm.Comparisons;
                excelWorksheet.Cells[rowCounter + startRow + 2, startCol + 4].Value = algorithm.Swaps;
                excelWorksheet.Cells[rowCounter + startRow + 2, startCol + 5].Value = algorithm.BestCase;
                excelWorksheet.Cells[rowCounter + startRow + 2, startCol + 6].Value = algorithm.AverageCase;
                excelWorksheet.Cells[rowCounter + startRow + 2, startCol + 7].Value = algorithm.WorstCase;
                excelWorksheet.Cells[rowCounter + startRow + 2, startCol + 8].Value = algorithm.WorstCaseSpaceComplexity;

                excelWorksheet.Cells[rowCounter + startRow + 2, startCol, rowCounter + startRow + 2, startCol + 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                excelWorksheet.Cells[rowCounter + startRow + 2, startCol, rowCounter + startRow + 2, startCol + 8].Style.Fill.BackgroundColor.SetColor(((rowCounter + startRow + 2) % 2 != 0) ? Color.White : ColorTranslator.FromHtml("#FFF1E6"));
                rowCounter++;
            }
            BorderCell(excelWorksheet, startRow, startCol, rowCounter + startRow + 1, startCol + 8);
            return (rowCounter, startCol + 8);
        }

        private void BorderCell(ExcelWorksheet excelWorksheet, int Row, int Col)
        {
            excelWorksheet.Cells[Row, Col].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            excelWorksheet.Cells[Row, Col].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            excelWorksheet.Cells[Row, Col].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            excelWorksheet.Cells[Row, Col].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        }

        private void BorderCell(ExcelWorksheet excelWorksheet, int FromRow, int FromCol, int ToRow, int ToCol)
        {
            excelWorksheet.Cells[FromRow, FromCol, ToRow, ToCol].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            excelWorksheet.Cells[FromRow, FromCol, ToRow, ToCol].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            excelWorksheet.Cells[FromRow, FromCol, ToRow, ToCol].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            excelWorksheet.Cells[FromRow, FromCol, ToRow, ToCol].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        }

        private static string GetAlgorithmName(SortingAlgorithm algorithm)
        {
            string s = algorithm switch
            {
                SortingAlgorithm.DefaultSort => Resource.DefaultSort,
                SortingAlgorithm.BubbleSort => Resource.BubbleSort,
                SortingAlgorithm.CocktailSort => Resource.CocktailSort,
                SortingAlgorithm.InsertionSort => Resource.InsertionSort,
                SortingAlgorithm.GnomeSort => Resource.GnomeSort,
                SortingAlgorithm.MergeSort => Resource.MergeSort,
                SortingAlgorithm.TreeSort => Resource.TreeSort,
                SortingAlgorithm.SelectionSort => Resource.SelectionSort,
                SortingAlgorithm.CombSort => Resource.CombSort,
                SortingAlgorithm.ShellSort => Resource.ShellSort,
                SortingAlgorithm.HeapSort => Resource.HeapSort,
                SortingAlgorithm.QuickSort => Resource.QuickSort,
                SortingAlgorithm.StoogeSort => Resource.StoogeSort,
                SortingAlgorithm.BogoSort => Resource.BogoSort,
                _ => Resource.DefaultSort
            };
            return s;
        }

        private static string GetArrayType(ArrayType arrayType)
        {
            string s = arrayType switch
            {
                ArrayType.Random => Resource.Random,
                ArrayType.Sorted => Resource.Sorted,
                ArrayType.NearlySorted => Resource.NearlySorted,
                ArrayType.Reversed => Resource.Reversed,
                ArrayType.FewUnique => Resource.FewUnique,
                _ => Resource.Random
            };
            return s;
        }

        private static void OpenFile(string path)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = path;
                psi.UseShellExecute = true;
                Process.Start(psi);
            }
            catch
            {
                throw new Exception();
            }
        }

        private static string GenerateFileName(Format format)
        {
            StringBuilder name = new StringBuilder();
            name.Append(GetUserName());
            name.Append("-Report#");
            name.Append(new Random().NextInt64());
            name.Append("-");
            name.Append(DateTime.Now);
            if (format.Equals(Format.Txt))
            {
                name.Append(".txt");
            }
            else if (format.Equals(Format.Excel))
            {
                name.Append(".xlsx");
            }
            return name.Replace(@"\", @"-").Replace(" ", "").Replace(":", "").ToString();
        }

        private static string GetUserName()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return WindowsIdentity.GetCurrent().Name;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return Environment.UserName;
            }
            else
            {
                return "Unknown";
            }
        }
    }
}