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
using static OfficeOpenXml.ExcelErrorValue;
using System.Security;
using System.Xml.Linq;
using System.IO;
using System;
using System.ComponentModel;

namespace Sorting
{
    /// <summary>
    /// The class Printer represents capabilities for printing of
    /// arrays and algorithms information to .txt- and .-xlsx files or
    /// creating string report.<br/>
    /// The class does not check if the arrays are sorted by means of
    /// the current sorting algorithms.<br/>
    /// The class works like a printer - without checking the correctness of algorithms,
    /// arrays and so on. It prints the information to the .txt- or .xlsx-file.<br/>
    /// The classes ArraysInfo and AlgorithmsInfo form the basis of all reports.<br/>
    /// The txt-reports are creating by means of the class ConsoleTables:
    /// https://github.com/khalidabuhakmeh/ConsoleTables<br/>
    /// The Microsoft Office Excel reports are creating with the help of the EPPlus library
    /// (version 6.1.3 current as of 04/01/2023):
    /// https://www.epplussoftware.com/
    /// </summary>
    public class Printer
    {
        private static string reportsFolderName = Resource.ReportsFolderName;

        /// <summary>
        /// The name of the folder for reports.<br/>
        /// The default name is "Sorting algorithms reports".<br/>
        /// It changes according to the system settings.
        /// </summary>
        /// <exception cref="ArgumentNullException">The ArgumentNullException is thrown due to the incorrect setting of the property.<br/>
        /// The property is null.</exception>
        /// <exception cref="ArgumentException">The ArgumentException is thrown due to the incorrect setting of the property.<br/>
        /// The property may be empty.<br/>
        /// It may contain the not allowed names: LPT1, LPT2, LPT3, LPT4, LPT5, LPT6, LPT7, LPT8, LPT9, 
        /// COM1, COM2, COM3, COM4, COM5, COM6, COM7, COM8, COM9, PRN, AUX, NUL, CON, CLOCK$,
        /// dot character (.), and two dot characters (..).<br/>
        /// It may contain the following reserved characters: &lt; T&gt; : " / \ | ? *.<br/>
        /// It may contain invalid file or path name characters.</exception>
        /// <exception cref="PathTooLongException">The PathTooLongException is thrown due to the incorrect setting of the property.<br/>
        /// The property has more than 255 characters.</exception>
        /// <exception cref="SecurityException">The SecurityException is thrown due to the incorrect setting of the property.<br/>
        /// The caller does not have the required permissions.</exception>
        /// <exception cref="NotSupportedException">The NotSupportedException is thrown due to the incorrect setting of the property.<br/>
        /// The property contains a colon (:) in the middle of the string.</exception>
        public static string ReportsFolderName
        {
            get
            {
                return reportsFolderName;
            }

            set
            {
                string newValue = value;
                try
                {
                    bool valid = IsFolderFileNameValid(newValue);

                    newValue = newValue.TrimEnd();
                    string path = Path.Combine(WorkingFolderPath, newValue);
                    valid = IsPathValid(path);

                    if (valid)
                    {
                        reportsFolderName = newValue;
                        SortingReportsFolderPath = Path.Combine(WorkingFolderPath, ReportsFolderName);
                    }
                }
                catch (ArgumentNullException argumentNullException)
                {
                    throw new ArgumentNullException($"The ArgumentNullException was thrown due to the incorrect setting {nameof(ReportsFolderName)}.", argumentNullException);
                }
                catch (ArgumentException argumentException)
                {
                    throw new ArgumentException($"The ArgumentException was thrown due to the incorrect setting {nameof(ReportsFolderName)}.", argumentException);
                }
                catch (PathTooLongException pathTooLongException)
                {
                    throw new PathTooLongException($"The PathTooLongException was thrown due to the incorrect setting {nameof(ReportsFolderName)}.", pathTooLongException);
                }
                catch (SecurityException)
                {
                    throw new SecurityException($"The SecurityException was thrown due to the incorrect setting {nameof(ReportsFolderName)}. " +
                        "There are no required permissions.");
                }
                catch (NotSupportedException)
                {
                    throw new NotSupportedException($"The NotSupportedException was thrown due to the incorrect setting {nameof(ReportsFolderName)}. " +
                        @"The string contains a colon ("":"") that is not part of a volume identifier (for example, ""c:\"").");
                }
            }
        }

        private static string workingFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        /// <summary>
        /// The path to the working folder (the folder for reports).
        /// The folder is not included to this path.<br/>
        /// The default path is the directory "My documents".
        /// </summary>
        /// <exception cref="ArgumentNullException">The ArgumentNullException is thrown due to the incorrect setting of the property.<br/>
        /// The property is null.</exception>
        /// <exception cref="ArgumentException">The ArgumentException is thrown due to the incorrect setting of the property.<br/>
        /// The property is empty, contains only white spaces, or contains invalid characters.</exception>
        /// <exception cref="PathTooLongException">The PathTooLongException is thrown due to the incorrect setting of the property.<br/>
        /// The property exceed the system-defined maximum length.</exception>
        /// <exception cref="UnauthorizedAccessException">The UnauthorizedAccessException is thrown due to the incorrect setting of the property.<br/>
        /// Access to the path in the property is denied.</exception>
        /// <exception cref="SecurityException">The SecurityException is thrown due to the incorrect setting of the property.<br/>
        /// The caller does not have the required permissions.</exception>
        /// <exception cref="NotSupportedException">The NotSupportedException is thrown due to the incorrect setting of the property.<br/>
        /// The property contains a colon (:) in the middle of the string.</exception>
        /// <exception cref="IOException">The IOException is thrown due to the incorrect setting of the property.<br/>
        /// The path in the property cannot be used to create a directory.</exception>
        public static string WorkingFolderPath
        {
            get
            {
                return workingFolderPath;
            }

            set
            {
                string newValue = value;
                try
                {
                    newValue = newValue.TrimEnd();
                    bool valid = IsPathValid(newValue);

                    if (valid)
                    {
                        workingFolderPath = newValue;
                        SortingReportsFolderPath = Path.Combine(WorkingFolderPath, ReportsFolderName);
                    }
                }
                catch (ArgumentNullException argumentNullException)
                {
                    throw new ArgumentNullException($"The ArgumentNullException was thrown due to the incorrect setting {nameof(WorkingFolderPath)}.", argumentNullException);
                }
                catch (ArgumentException argumentException)
                {
                    throw new ArgumentException($"The ArgumentException was thrown due to the incorrect setting {nameof(WorkingFolderPath)}.", argumentException);
                }
                catch (PathTooLongException pathTooLongException)
                {
                    throw new PathTooLongException($"The PathTooLongException was thrown due to the incorrect setting {nameof(WorkingFolderPath)}.", pathTooLongException);
                }
                catch (UnauthorizedAccessException unauthorizedAccessException)
                {
                    throw new UnauthorizedAccessException($"The ArgumentException was thrown due to the incorrect setting {nameof(WorkingFolderPath)}.", unauthorizedAccessException);
                }
                catch (SecurityException securityException)
                {
                    throw new SecurityException($"The SecurityException was thrown due to the incorrect setting {nameof(WorkingFolderPath)}.", securityException);
                }
                catch (NotSupportedException notSupportedException)
                {
                    throw new NotSupportedException($"The NotSupportedException was thrown due to the incorrect setting {nameof(WorkingFolderPath)}. " +
                        @"The string contains a colon ("":"") that is not part of a volume identifier (for example, ""c:\"").", notSupportedException);
                }
                catch (IOException ioException)
                {
                    throw new IOException($"The Exception was thrown due to the incorrect setting {nameof(WorkingFolderPath)}.", ioException);
                }
            }
        }

        private static string sortingReportsFolderPath = Path.Combine(WorkingFolderPath, ReportsFolderName);

        /// <summary>
        /// The path to the working folder (the folder for reports).
        /// The folder is included to this path.<br/>
        /// </summary>
        /// <exception cref="ArgumentNullException">The ArgumentNullException is thrown due to the incorrect setting of the property.<br/>
        /// The property is null.</exception>
        /// <exception cref="ArgumentException">The ArgumentException is thrown due to the incorrect setting of the property.<br/>
        /// The property is empty, contains only white spaces, or contains invalid characters.</exception>
        /// <exception cref="PathTooLongException">The PathTooLongException is thrown due to the incorrect setting of the property.<br/>
        /// The property exceed the system-defined maximum length.</exception>
        /// <exception cref="UnauthorizedAccessException">The UnauthorizedAccessException is thrown due to the incorrect setting of the property.<br/>
        /// Access to the path in the property is denied.</exception>
        /// <exception cref="SecurityException">The SecurityException is thrown due to the incorrect setting of the property.<br/>
        /// The caller does not have the required permissions.</exception>
        /// <exception cref="NotSupportedException">The NotSupportedException is thrown due to the incorrect setting of the property.<br/>
        /// The property contains a colon (:) in the middle of the string.</exception>
        /// <exception cref="IOException">The IOException is thrown due to the incorrect setting of the property.<br/>
        /// The path in the property cannot be used to create a directory.</exception>
        public static string SortingReportsFolderPath
        {
            get
            {
                return sortingReportsFolderPath;
            }

            set
            {
                string newValue = value;
                try
                {
                    newValue = newValue.TrimEnd();
                    bool valid = IsPathValid(newValue);

                    if (valid)
                    {
                        sortingReportsFolderPath = newValue;
                    }
                }
                catch (ArgumentNullException argumentNullException)
                {
                    throw new ArgumentNullException($"The ArgumentNullException was thrown due to the incorrect setting {nameof(SortingReportsFolderPath)}.", argumentNullException);
                }
                catch (ArgumentException argumentException)
                {
                    throw new ArgumentException($"The ArgumentException was thrown due to the incorrect setting {nameof(SortingReportsFolderPath)}.", argumentException);
                }
                catch (PathTooLongException pathTooLongException)
                {
                    throw new PathTooLongException($"The PathTooLongException was thrown due to the incorrect setting {nameof(SortingReportsFolderPath)}.", pathTooLongException);
                }
                catch (UnauthorizedAccessException unauthorizedAccessException)
                {
                    throw new UnauthorizedAccessException($"The ArgumentException was thrown due to the incorrect setting {nameof(SortingReportsFolderPath)}.", unauthorizedAccessException);
                }
                catch (SecurityException securityException)
                {
                    throw new SecurityException($"The SecurityException was thrown due to the incorrect setting {nameof(SortingReportsFolderPath)}.", securityException);
                }
                catch (NotSupportedException notSupportedException)
                {
                    throw new NotSupportedException($"The NotSupportedException was thrown due to the incorrect setting {nameof(SortingReportsFolderPath)}. " +
                        @"The string contains a colon ("":"") that is not part of a volume identifier (for example, ""c:\"").", notSupportedException);
                }
                catch (IOException ioException)
                {
                    throw new IOException($"The Exception was thrown due to the incorrect setting {nameof(SortingReportsFolderPath)}.", ioException);
                }
            }
        }

        private ArraysInfo arraysInfo;

        /// <summary>
        /// The arrays information for printing.
        /// </summary>
        /// <exception cref="ArgumentNullException">The ArgumentNullException is thrown due to the incorrect setting of the property.<br/>
        /// The property is null.</exception>
        public ArraysInfo ArraysInfo
        {
            get
            {
                return arraysInfo;
            }
            set
            {
                if (value == null)
                {
                    ArgumentNullException argumentNullException = new ArgumentNullException("The property is null.");
                    throw argumentNullException;
                }
                else
                {
                    arraysInfo = value;
                }
            }
        }

        private AlgorithmsInfo algorithmsInfo;

        /// <summary>
        /// The sorting algorithms information for printing.
        /// </summary>
        /// <exception cref="ArgumentNullException">The ArgumentNullException is thrown due to the incorrect setting of the property.<br/>
        /// The property is null.</exception>
        public AlgorithmsInfo AlgorithmsInfo
        {
            get
            {
                return algorithmsInfo;
            }
            set
            {
                if (value == null)
                {
                    ArgumentNullException argumentNullException = new ArgumentNullException("The property is null.");
                    throw argumentNullException;
                }
                else
                {
                    algorithmsInfo = value;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the Printer class.
        /// </summary>
        /// <param name="arrayInfo">Reference to the instance of the ArraysInfo class.</param>
        /// <param name="algorithmInfo">>Reference to the instance of the AlgorithmsInfo class.</param>
        /// <exception cref="ArgumentNullException">One of the strings of the properties WorkingFolderPath or
        /// ReportsFolderName is null.<br/>
        /// The parameter arraysInfo is null.<br/>
        /// The parameter algorithmsInfo is null.</exception>
        /// <exception cref="ArgumentException">The argument of GetFolderPath() is not a member of Environment.SpecialFolder or
        /// one of the strings of the properties WorkingFolderPath or ReportsFolderName
        /// contains one or more of the invalid characters defined in GetInvalidPathChars().</exception>
        /// <exception cref="PlatformNotSupportedException">The current platform is not supported.
        /// Choose another WorkingFolderPath.</exception>
        public Printer(ArraysInfo arrayInfo, AlgorithmsInfo algorithmInfo)
        {
            ReportsFolderName = Resource.ReportsFolderName;

            try
            {
                WorkingFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("The argument of GetFolderPath() is not a member of Environment.SpecialFolder");
            }
            catch (PlatformNotSupportedException)
            {
                throw new PlatformNotSupportedException($"The current platform is not supported. Choose another {nameof(WorkingFolderPath)}.");
            }

            try
            {
                SortingReportsFolderPath = Path.Combine(WorkingFolderPath, ReportsFolderName);
            }
            catch (ArgumentNullException)
            {
                ArgumentNullException argumentNullException = new ArgumentNullException($"One of the strings of the properties {nameof(WorkingFolderPath)} or {nameof(ReportsFolderName)} is null.");
                throw argumentNullException;
            }
            catch (ArgumentException)
            {
                ArgumentException argumentException = new ArgumentException($"One of the strings of the properties {nameof(WorkingFolderPath)} or {nameof(ReportsFolderName)}" +
                    $"contains one or more of the invalid characters defined in GetInvalidPathChars().");
                throw argumentException;
            }

            if (arrayInfo == null)
            {
                ArgumentNullException argumentNullException = new ArgumentNullException($"The parameter {nameof(arrayInfo)} is null.");
                throw argumentNullException;
            }
            else
            {
                ArraysInfo = arrayInfo;
                arraysInfo = arrayInfo;
            }

            if (algorithmInfo == null)
            {
                ArgumentNullException argumentNullException = new ArgumentNullException($"The parameter {nameof(algorithmInfo)} is null.");
                throw argumentNullException;
            }
            else
            {
                AlgorithmsInfo = algorithmInfo;
                algorithmsInfo = algorithmInfo;
            }
        }

        /// <summary>
        /// Initializes a new instance of the Printer class.
        /// </summary>
        /// <param name="folderPath">The path to the reports folder. The reports folder will be created automatically.</param>
        /// <param name="arrayInfo">Reference to the instance of the ArraysInfo class.</param>
        /// <param name="algorithmInfo">>Reference to the instance of the AlgorithmsInfo class.</param>
        /// <exception cref="ArgumentNullException">
        /// The parameter folderPath is null.<br/>
        /// The parameter arraysInfo is null.<br/>
        /// The parameter algorithmsInfo is null.</exception>
        /// <exception cref="ArgumentException">The folderPath is empty, contains only white spaces, or contains invalid characters.</exception>
        /// <exception cref="PathTooLongException">The folderPath exceed the system-defined maximum length.</exception>
        /// <exception cref="UnauthorizedAccessException">Access to the folderPath is denied.</exception>
        /// <exception cref="SecurityException">The caller does not have the required permissions for folderPath.</exception>
        /// <exception cref="NotSupportedException">The folderPath contains a colon (:) in the middle of the string.</exception>
        /// <exception cref="IOException">The folderPath in the property cannot be used to create a directory.</exception>
        public Printer(string folderPath, ArraysInfo arrayInfo, AlgorithmsInfo algorithmInfo)
        {
            WorkingFolderPath = folderPath;

            if (arrayInfo == null)
            {
                ArgumentNullException argumentNullException = new ArgumentNullException($"The parameter {nameof(arrayInfo)} is null.");
                throw argumentNullException;
            }
            else
            {
                ArraysInfo = arrayInfo;
                arraysInfo = arrayInfo;
            }

            if (algorithmInfo == null)
            {
                ArgumentNullException argumentNullException = new ArgumentNullException($"The parameter {nameof(algorithmInfo)} is null.");
                throw argumentNullException;
            }
            else
            {
                AlgorithmsInfo = algorithmInfo;
                algorithmsInfo = algorithmInfo;
            }
        }

        /// <summary>
        /// The method generates an .xlsx-file with the report.<br/>
        /// The file will be saved in SortingReportsFolderPath.
        /// </summary>
        /// <param name="startAfterPrint">If this parameter is true, the file will be opened in the end.</param>
        /// <param name="reportType">The report may contain the full arrays or it may also not contain these arrays.
        /// By default the report will be with arrays.</param>
        /// <param name="elementsPerRow">This parameter is created to control how many elements will be printed in a row.
        /// By default this parameter equals 8000 elements.</param>
        public void GetExcelReport(bool startAfterPrint = false, ReportType reportType = ReportType.WithArray, int elementsPerRow = 8000)
        {
            Print(Format.Excel, startAfterPrint, reportType, elementsPerRow);
        }

        /// <summary>
        /// The method generates an .txt-file with the report.<br/>
        /// The file will be saved in SortingReportsFolderPath.
        /// </summary>
        /// <param name="startAfterPrint">If this parameter is true, the file will be opened in the end.</param>
        /// <param name="reportType">The report may contain the full arrays or it may also not contain these arrays.
        /// By default the report will be with arrays.</param>
        public void GetTxtReport(bool startAfterPrint = false, ReportType reportType = ReportType.WithArray)
        {
            Print(Format.Txt, startAfterPrint, reportType);
        }

        /// <summary>
        /// The method generates a string with the report.
        /// </summary>
        /// <param name="reportType">The report may contain the full arrays or it may also not contain these arrays.
        /// By default the report will be with arrays.</param>
        /// <returns>The string with the report.</returns>
        public string GetStringReport(ReportType reportType)
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
                foreach (dynamic element in ArraysInfo.InputArray)
                {
                    tempBuilder.Append(element + " ");
                }
                tempBuilder.Length--;
                temp = tempBuilder.ToString();
                temp = Regex.Replace(temp, "(?<=\\G.{80})(?=.)", "\n");
                report.AppendLine(temp);
            }
            report.AppendLine(Resource.InputArrayLength + ": " + ArraysInfo.InputArraySize);
            report.AppendLine(Resource.IsArraySorted + ": " + (ArraysInfo.IsInputArraySorted ? Resource.True : Resource.False));
            report.AppendLine(Resource.ArrayType + ": " + GetArrayType(ArraysInfo.InputArrayType));

            // Output array information
            report.AppendLine("");
            report.AppendLine(Resource.OutputArray + ": ");
            temp = string.Empty;
            tempBuilder = new StringBuilder();
            if (reportType.Equals(ReportType.WithArray))
            {
                foreach (dynamic element in ArraysInfo.OutputArray)
                {
                    tempBuilder.Append(element + " ");
                }
                tempBuilder.Length--;
                temp = tempBuilder.ToString();
                temp = Regex.Replace(temp, "(?<=\\G.{80})(?=.)", "\n");
                report.AppendLine(temp);
            }
            report.AppendLine(Resource.OutputArrayLength + ": " + ArraysInfo.OutputArraySize);
            report.AppendLine(Resource.IsArraySorted + ": " + (ArraysInfo.IsOutputArraySorted ? Resource.True : Resource.False));
            report.AppendLine(Resource.ArrayType + ": " + GetArrayType(ArraysInfo.OutputArrayType));
            report.AppendLine(string.Concat(Enumerable.Repeat("*", 80)));

            // 1 Algorithm information
            report.AppendLine("");
            report.AppendLine(string.Concat(Enumerable.Repeat("*", 80)));
            if (AlgorithmsInfo.AlgorithmsList.Count == 1)
            {
                report.AppendLine(Resource.SortingAlgorithm + ": " + GetAlgorithmName(AlgorithmsInfo.AlgorithmsList[0].Name));
                report.AppendLine(Resource.Time + ": " + AlgorithmsInfo.AlgorithmsList[0].Time.ElapsedMilliseconds + " " + Resource.ms);
                report.AppendLine(Resource.IsTheAlgorithmStabil + ": " + (AlgorithmsInfo.AlgorithmsList[0].IsStabil ? Resource.True : Resource.False));
                report.AppendLine(Resource.Comparisons + ": " + AlgorithmsInfo.AlgorithmsList[0].Comparisons);
                report.AppendLine(Resource.Swaps + ": " + AlgorithmsInfo.AlgorithmsList[0].Swaps);
            }

            // 2 or more algorithms information
            if (AlgorithmsInfo.AlgorithmsList.Count > 1)
            {
                ConsoleTable table = new ConsoleTable(Resource.SortingAlgorithm,
                    Resource.Time, Resource.IsTheAlgorithmStabil, Resource.Comparisons, Resource.Swaps);
                foreach (dynamic algorithm in AlgorithmsInfo.AlgorithmsList)
                {
                    table.AddRow(GetAlgorithmName(algorithm.Name), algorithm.Time.ElapsedMilliseconds,
                        algorithm.IsStabil ? Resource.True : Resource.False, algorithm.Comparisons, algorithm.Swaps);
                }
                report.AppendLine(table.ToString());
            }
            return report.ToString();
        }

        private void Print(Format format, bool startAfterPrint = false, ReportType reportType = ReportType.WithArray, int elementsPerRow = 8000)
        {
            if (!Directory.Exists(SortingReportsFolderPath))
            {
                try
                {
                    Directory.CreateDirectory(SortingReportsFolderPath);
                }
                catch (UnauthorizedAccessException)
                {
                    UnauthorizedAccessException unauthorizedAccessException = new UnauthorizedAccessException("The caller does not have the required permission " +
                        $"to create a directory in {SortingReportsFolderPath}. Please, change the property {nameof(SortingReportsFolderPath)}.");
                    throw unauthorizedAccessException;
                }
                catch (ArgumentNullException)
                {
                    ArgumentNullException argumentNullException = new ArgumentNullException($"The {nameof(SortingReportsFolderPath)} is null.");
                    throw argumentNullException;
                }
                catch (ArgumentException)
                {
                    ArgumentException argumentException = new ArgumentException($"The {nameof(SortingReportsFolderPath)}  is a zero-length string," +
                        $"contains only white space, or contains one or more invalid characters." +
                        $"You can query for invalid characters by using the GetInvalidPathChars() method.");
                    throw argumentException;
                }
                catch (PathTooLongException)
                {
                    PathTooLongException pathTooLongException = new PathTooLongException($"The {nameof(SortingReportsFolderPath)} exceed the system-defined maximum length.");
                    throw pathTooLongException;
                }
                catch (DirectoryNotFoundException)
                {
                    DirectoryNotFoundException directoryNotFoundException = new DirectoryNotFoundException($"The {nameof(SortingReportsFolderPath)} is invalid " +
                        $"(for example, it is on an unmapped drive).");
                }
                catch (IOException)
                {
                    IOException iOException = new IOException($"The directory specified by path {nameof(SortingReportsFolderPath)} {SortingReportsFolderPath} is a file.");
                    throw iOException;
                }
                catch (NotSupportedException)
                {
                    NotSupportedException notSupportedException = new NotSupportedException($"The {nameof(SortingReportsFolderPath)} contains a colon character (:) " +
                        $"that is not part of a drive label (\"C:\\\").");
                    throw notSupportedException;
                }
            }
;
            string fileName = string.Empty;
            string filePath = string.Empty;

            for (int i = 0; i < 5; i++)
            {
                try
                {
                    fileName = GenerateFileName(format);
                    filePath = Path.Combine(SortingReportsFolderPath, fileName);
                }
                catch (ArgumentNullException)
                {
                    // Nothing
                }
                catch (ArgumentException)
                {
                    fileName = GenerateFileName(format, true);
                    filePath = Path.Combine(SortingReportsFolderPath, fileName);
                }

                if (File.Exists(filePath))
                {
                    continue;
                }
                else
                {
                    try
                    {
                        if (Printer.IsPathValid(filePath))
                        {
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    catch
                    {
                        // Nothing
                    }
                }                
            }

            if (format.Equals(Format.Txt))
            {
                GenerateTextReport(filePath, reportType);
            }
            else if (format.Equals(Format.Excel))
            {
                GenerateExcelReport(filePath, elementsPerRow, reportType);
            }

            if (startAfterPrint)
            {
                try
                {
                    OpenFile(filePath);
                }
                catch
                {
                    // Nothing
                }
            }
        }

        private void GenerateTextReport(string path, ReportType reportType)
        {
            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    byte[] buffer = Encoding.Default.GetBytes(GetStringReport(reportType));
                    fileStream.Write(buffer);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                // Nothing
            }
            catch (ArgumentNullException)
            {
                // Nothing
            }
            catch (EncoderFallbackException)
            {
                // Nothing
            }
            catch (ArgumentException)
            {
                // Nothing
            }
            catch (NotSupportedException)
            {
                // Nothing
            }
            catch (FileNotFoundException)
            {
                // Nothing
            }
            catch (DirectoryNotFoundException)
            {
                // Nothing
            }
            catch (PathTooLongException)
            {
                // Nothing
            }
            catch (IOException)
            {
                // Nothing
            }
            catch (SecurityException)
            {
                // Nothing
            }
            catch (UnauthorizedAccessException)
            {
                // Nothing
            }
        }

        private void GenerateExcelReport(string path, int elementsPerRow, ReportType excelReportType)
        {
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Error occurred here. Although it did not occurred past time.
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; // So far it works.

            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(Resource.Report);

                (int row, int column) lastCoordinates = AddExcelCommonInformation(worksheet, 1, 1);
                lastCoordinates = AddInputArrayInformation(worksheet, lastCoordinates.row + 2, 1, elementsPerRow, excelReportType);
                lastCoordinates = AddOutputArrayInformation(worksheet, lastCoordinates.row + 1, 1, elementsPerRow, excelReportType);

                if (AlgorithmsInfo.AlgorithmsList.Count == 1)
                {
                    lastCoordinates = AddAlgorithmInformation(worksheet, lastCoordinates.row + 1, 1);
                }
                else if (AlgorithmsInfo.AlgorithmsList.Count > 1)
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
            excelWorksheet.Cells[startRow + 1, startCol + 1].Value = ArraysInfo.InputArraySize;
            excelWorksheet.Cells[startRow + 2, startCol].Value = Resource.IsArraySorted + ":";
            excelWorksheet.Cells[startRow + 2, startCol + 1].Value = ArraysInfo.IsInputArraySorted ? Resource.True : Resource.False;
            excelWorksheet.Cells[startRow + 3, startCol].Value = Resource.ArrayType + ":";
            excelWorksheet.Cells[startRow + 3, startCol + 1].Value = GetArrayType(ArraysInfo.InputArrayType);
            excelWorksheet.Cells[startRow, startCol, startRow + 3, startCol].Style.Fill.PatternType = ExcelFillStyle.Solid;
            excelWorksheet.Cells[startRow, startCol, startRow + 3, startCol].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#BCD4E6"));
            BorderCell(excelWorksheet, startRow, startCol);
            BorderCell(excelWorksheet, startRow + 1, startCol, startRow + 3, startCol + 1);

            if (reportType.Equals(ReportType.WithArray))
            {
                for (int i = 0, j = startRow + 4, k = startCol; i < ArraysInfo.InputArray.Length; i++)
                {
                    excelWorksheet.Cells[j, k].Value = ArraysInfo.InputArray[i];
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
            excelWorksheet.Cells[startRow + 1, startCol + 1].Value = ArraysInfo.OutputArraySize;
            excelWorksheet.Cells[startRow + 2, startCol].Value = Resource.IsArraySorted + ":";
            excelWorksheet.Cells[startRow + 2, startCol + 1].Value = ArraysInfo.IsOutputArraySorted ? Resource.True : Resource.False;
            excelWorksheet.Cells[startRow + 3, startCol].Value = Resource.ArrayType + ":";
            excelWorksheet.Cells[startRow + 3, startCol + 1].Value = GetArrayType(ArraysInfo.OutputArrayType);
            excelWorksheet.Cells[startRow, startCol, startRow + 3, startCol].Style.Fill.PatternType = ExcelFillStyle.Solid;
            excelWorksheet.Cells[startRow, startCol, startRow + 3, startCol].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FAD2E1"));
            BorderCell(excelWorksheet, startRow, 1);
            BorderCell(excelWorksheet, startRow + 1, startCol, startRow + 3, startCol + 1);

            if (reportType.Equals(ReportType.WithArray))
            {
                for (int i = 0, j = startRow + 4, k = 1; i < ArraysInfo.OutputArray.Length; i++)
                {
                    excelWorksheet.Cells[j, k].Value = ArraysInfo.OutputArray[i];
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
            excelWorksheet.Cells[startRow, startCol + 1].Value = GetAlgorithmName(AlgorithmsInfo.AlgorithmsList[0].Name);
            excelWorksheet.Cells[startRow + 1, startCol].Value = Resource.Time + ":";
            excelWorksheet.Cells[startRow + 1, startCol + 1].Value = AlgorithmsInfo.AlgorithmsList[0].Time.ElapsedMilliseconds;
            excelWorksheet.Cells[startRow + 1, startCol + 2].Value = Resource.ms;
            excelWorksheet.Cells[startRow + 2, startCol].Value = Resource.IsTheAlgorithmStabil + ":";
            excelWorksheet.Cells[startRow + 2, startCol + 1].Value = AlgorithmsInfo.AlgorithmsList[0].IsStabil ? Resource.True : Resource.False;
            excelWorksheet.Cells[startRow + 3, startCol].Value = Resource.Comparisons + ":";
            excelWorksheet.Cells[startRow + 3, startCol + 1].Value = AlgorithmsInfo.AlgorithmsList[0].Comparisons;
            excelWorksheet.Cells[startRow + 4, startCol].Value = Resource.Swaps + ":";
            excelWorksheet.Cells[startRow + 4, startCol + 1].Value = AlgorithmsInfo.AlgorithmsList[0].Swaps;

            excelWorksheet.Cells[startRow + 5, startCol, startRow + 7, startCol].Merge = true;
            excelWorksheet.Cells[startRow + 5, startCol].Value = Resource.TimeComplexity + ":";
            excelWorksheet.Cells[startRow + 5, startCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            excelWorksheet.Cells[startRow + 5, startCol].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            excelWorksheet.Cells[startRow + 5, startCol].Style.WrapText = true;

            excelWorksheet.Cells[startRow + 5, startCol + 1].Value = Resource.BestCase + ":";
            excelWorksheet.Cells[startRow + 5, startCol + 2].Value = AlgorithmsInfo.AlgorithmsList[0].BestCase;
            excelWorksheet.Cells[startRow + 6, startCol + 1].Value = Resource.AverageCase + ":";
            excelWorksheet.Cells[startRow + 6, startCol + 2].Value = AlgorithmsInfo.AlgorithmsList[0].AverageCase;
            excelWorksheet.Cells[startRow + 7, startCol + 1].Value = Resource.WorstCase + ":";
            excelWorksheet.Cells[startRow + 7, startCol + 2].Value = AlgorithmsInfo.AlgorithmsList[0].WorstCase;

            excelWorksheet.Cells[startRow + 8, startCol].Value = Resource.SpaceComplexity + ":";
            excelWorksheet.Cells[startRow + 8, startCol + 1].Value = Resource.WorstCase + ":";
            excelWorksheet.Cells[startRow + 8, startCol + 2].Value = AlgorithmsInfo.AlgorithmsList[0].WorstCaseSpaceComplexity;

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

            foreach (dynamic algorithm in AlgorithmsInfo.AlgorithmsList)
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

        private static void BorderCell(ExcelWorksheet excelWorksheet, int Row, int Col)
        {
            excelWorksheet.Cells[Row, Col].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            excelWorksheet.Cells[Row, Col].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            excelWorksheet.Cells[Row, Col].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            excelWorksheet.Cells[Row, Col].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        }

        private static void BorderCell(ExcelWorksheet excelWorksheet, int FromRow, int FromCol, int ToRow, int ToCol)
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
                ArrayType.Other => Resource.Other,
                _ => Resource.Random
            };
            return s;
        }

        private static void OpenFile(string filePath)
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = filePath;
                processStartInfo.UseShellExecute = true;
                Process.Start(processStartInfo);
            }
            catch (ObjectDisposedException)
            {
                throw new ObjectDisposedException($"The process object {filePath} has already been disposed.");
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Win32Exception)
            {
                throw new Win32Exception($"An error occurred when opening the associated file {filePath}. OR" +
                    $"The file {filePath} specified in the processStartInfo parameter's FileName ({filePath}) property could not be found. OR" +
                    $"The sum of the length of the arguments and the length of the full path to the process exceeds 2080." +
                    $"The error message associated with this exception can be one of the following:" +
                    $"The data area passed to a system call is too small. or Access is denied.");
            }
            catch (PlatformNotSupportedException)
            {
                throw new PlatformNotSupportedException("Method not supported on operating systems without shell support such as Nano Server");
            }
        }

        private static string GenerateFileName(Format format, bool onlyNumber = false)
        {
            Random random = new Random();
            StringBuilder name = new StringBuilder();
            if (!onlyNumber)
            {
                try
                {
                    name.Append(GetUserName());
                    name.Append("-Report#");
                    name.Append(new Random().NextInt64());
                    name.Append(Printer.RandomString(random, 7));
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
                    name.Replace(@"\", @"-").Replace(" ", "").Replace(":", "").ToString();
                }
                catch (Exception ex)
                {
                    if (ex is ArgumentOutOfRangeException || ex is ArgumentNullException || ex is ArgumentException)
                    {
                        name.Clear();
                        name.Append(random.NextInt64());
                    }
                }
            }
            else
            {
                name.Clear();
                name.Append(new Random().NextInt64());
            }
            return name.ToString();
        }

        private static string RandomString(Random random, int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
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

        private static bool IsFolderFileNameValid(string name)
        {
            if (name == null)
            {
                ArgumentNullException argumentNullException = new ArgumentNullException($"The {nameof(name)} is null.");
                throw argumentNullException;
            }
            else if (name.Length == 0)
            {
                ArgumentException argumentException = new ArgumentException($"The {nameof(name)} is empty.");
                throw argumentException;
            }
            else if (name.Length > 255)
            {
                PathTooLongException pathTooLongException = new PathTooLongException($"The {nameof(name)} has more than 255 characters.");
                throw pathTooLongException;
            }
            else if (name.Equals("PRN") ||
                name.Equals("AUX") ||
                name.Equals("NUL") ||
                name.Equals("CON") ||
                name.Equals(".") ||
                name.Equals("..") ||
                name.Equals("CLOCK$") ||
                name.Equals("COM1") ||
                name.Equals("COM2") ||
                name.Equals("COM3") ||
                name.Equals("COM4") ||
                name.Equals("COM5") ||
                name.Equals("COM6") ||
                name.Equals("COM7") ||
                name.Equals("COM8") ||
                name.Equals("COM9") ||
                name.Equals("LPT1") ||
                name.Equals("LPT2") ||
                name.Equals("LPT3") ||
                name.Equals("LPT4") ||
                name.Equals("LPT5") ||
                name.Equals("LPT6") ||
                name.Equals("LPT7") ||
                name.Equals("LPT8") ||
                name.Equals("LPT9"))
            {
                ArgumentException argumentException = new ArgumentException("The following names are not allowed: " +
                    "LPT1, LPT2, LPT3, LPT4, LPT5, LPT6, LPT7, LPT8, LPT9, " +
                    "COM1, COM2, COM3, COM4, COM5, COM6, COM7, COM8, COM9, " +
                    "PRN, AUX, NUL, CON, CLOCK$, dot character (.), and two dot characters (..).");
                throw argumentException;
            }
            else if (HasSpecialChar(name))
            {
                ArgumentException argumentException = new ArgumentException("Do not use the following reserved characters:\r\n< > : \" / \\ | ? *");
                throw argumentException;
            }
            else if (Path.GetInvalidPathChars().Where(x => name.Contains(x)).Any())
            {
                ArgumentException argumentException = new ArgumentException("Do not use the invalid path name characters.");
                throw argumentException;
            }
            else if (Path.GetInvalidFileNameChars().Where(x => name.Contains(x)).Any())
            {
                ArgumentException argumentException = new ArgumentException("Do not use the invalid file name characters.");
                throw argumentException;
            }
            else
            {
                return true;
            }
        }

        private static bool IsPathValid(string path)
        {
            bool isValid = false;

            try
            {
                if (Path.GetInvalidPathChars().Where(x => path.Contains(x)).Any())
                {
                    throw new ArgumentException($"The {nameof(path)} is empty, contains only white spaces, or contains invalid characters.");
                }

                path = Path.GetFullPath(path);

                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                if (!directoryInfo.Exists)
                {
                    directoryInfo.Create();
                    directoryInfo.Delete(true);
                }
                isValid = true;
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException($"{nameof(path)} is null.");
            }
            catch (ArgumentException)
            {
                throw new ArgumentException($"The {nameof(path)} is empty, contains only white spaces, or contains invalid characters.");
            }
            catch (SecurityException)
            {
                throw new SecurityException("The caller does not have the required permissions.");
            }
            catch (UnauthorizedAccessException)
            {
                throw new UnauthorizedAccessException($"Access to {nameof(path)} is denied.");
            }
            catch (DirectoryNotFoundException)
            {
            }
            catch (PathTooLongException)
            {
                throw new PathTooLongException($"The {nameof(path)} exceed the system-defined maximum length.");
            }
            catch (NotSupportedException)
            {
                throw new NotSupportedException($"{nameof(path)} contains a colon (:) in the middle of the string.");
            }
            catch (IOException)
            {
                throw new IOException($"The path {nameof(path)} cannot be used to create a directory.");
            }
            return isValid;
        }

        private static bool HasSpecialChar(string input)
        {
            string specialChar = @"<>:""/\|?*";
            foreach (var item in specialChar)
            {
                if (input.Contains(item)) return true;
            }
            return false;
        }
    }
}