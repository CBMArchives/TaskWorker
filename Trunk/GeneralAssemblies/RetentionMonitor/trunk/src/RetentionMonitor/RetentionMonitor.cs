using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using TaskWorker_BLL;

namespace RetentionMonitor
{
    [Serializable]
    public class FileMonitor : TaskWorker_BLL.actionGeneral, IDisposable
    {
        #region Properties

        private string _directoryPath;
        private string _moveToPath;
        private string _ageInterval;
        private string _ageValue;
        private string _dateType;
        private DateTime _retentionDate;
        private DirectoryInfo _baseDirectory;
        private DirectoryInfo _moveDirectory;
        private bool _includeSubDirectories = true;
        private Operations _opType = Operations.Scan;
        private string _omitFileExtentsions;
        private bool _runAsThread = false;
        private bool disposed = false;
        

        #endregion

        public FileMonitor()
        {
            base.LogToFile = true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (_directoryPath != null) _directoryPath = null;
                    if (_moveToPath != null) _moveToPath = null;
                    if (_ageInterval != null) _ageInterval = null;
                    if (_ageValue != null) _ageValue = null;
                    if (_omitFileExtentsions != null) _omitFileExtentsions = null;
                    if (base.ActionXML != null) base.ActionXML = null;
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public override void Start()
        {
            LoadParameters();

            if (!(String.IsNullOrWhiteSpace(_directoryPath) || String.IsNullOrWhiteSpace(_ageInterval) || String.IsNullOrWhiteSpace(_ageValue)))
            {
                _retentionDate = ParseRetentionDate(_ageInterval, _ageValue);
                _baseDirectory = new DirectoryInfo(_directoryPath);

                base.LogItem("RetentionMonitor will parse all files prior to \"" + _retentionDate.ToString() + "\"", twk_LogLevels.Info_Detail, System.Reflection.MethodBase.GetCurrentMethod());

                if (_opType == Operations.Move && !String.IsNullOrWhiteSpace(_moveToPath))
                {
                    _moveDirectory = new DirectoryInfo(_moveToPath);
                }

                if (_baseDirectory.Exists)
                {
                    ParseRetentionDirectory(_baseDirectory);
                    base.LogItem("RetentionMonitor completed successfully!", twk_LogLevels.Info_Success, System.Reflection.MethodBase.GetCurrentMethod());
                }
                else
                    base.LogItem("RetentionMonitor unable to start due to missing monitor location directory!", twk_LogLevels.ProcessError, System.Reflection.MethodBase.GetCurrentMethod());
            }
            else
            {
                base.LogItem("RetentionMonitor unable to start due to missing configuration parameters!", twk_LogLevels.ProcessError, System.Reflection.MethodBase.GetCurrentMethod());
            }
        }

        private void ParseRetentionDirectory(DirectoryInfo _baseDirectory)
        {
            base.LogItem("Parsing directory for " + _opType.ToString() + " action: " + _baseDirectory.FullName, twk_LogLevels.DebugInfo_L4, System.Reflection.MethodBase.GetCurrentMethod());
            ParseRetentionFiles(_baseDirectory);

            if (_includeSubDirectories)
            {
                foreach (DirectoryInfo _dir in _baseDirectory.GetDirectories())
                {
                    ParseRetentionDirectory(_dir);
                }
            }
        }

        private DateTime ParseRetentionDate(string retentionPeriodType, string retentionPeriodValue)
        {
            Double subtract = 0;
            DateTime retentionDate = DateTime.Now;

            if (Double.TryParse(retentionPeriodValue, out subtract))
            {
                switch (retentionPeriodType.Trim().ToLower())
                {
                    case "millisecond":
                        retentionDate = retentionDate.AddMilliseconds((subtract - (subtract * 2)));
                        break;
                    case "second":
                        retentionDate = retentionDate.AddSeconds((subtract - (subtract * 2)));
                        break;
                    case "minute":
                        retentionDate = retentionDate.AddMinutes((subtract - (subtract * 2)));
                        break;
                    case "hour":
                        retentionDate = retentionDate.AddHours((subtract - (subtract * 2)));
                        break;
                    case "day":
                        retentionDate = retentionDate.AddDays((subtract - (subtract * 2)));
                        break;
                    case "month":
                        Int32 months;
                        if (Int32.TryParse((subtract - (subtract * 2)).ToString(), out months))
                            retentionDate = retentionDate.AddMonths(months);
                        break;
                    case "year":
                        Int32 years;
                        if (Int32.TryParse((subtract - (subtract * 2)).ToString(), out years))
                            retentionDate = retentionDate.AddYears(years);
                        break;
                    default:
                        base.LogItem("RetentionMonitor was unable to calculate retention date!", twk_LogLevels.ProcessError, System.Reflection.MethodBase.GetCurrentMethod());
                        return retentionDate;
                }
            }

            return retentionDate;
        }

        private void ParseRetentionFiles(DirectoryInfo MonitorDirectory)
        {
            bool exitLoop = false;

            if (MonitorDirectory.Exists)
            {
                foreach (FileInfo _file in MonitorDirectory.GetFiles())
                {
                    DateTime fileDate = _file.CreationTime;
                    
                    if (!String.IsNullOrWhiteSpace(_dateType))
                    {
                        switch (_dateType.Trim().ToLower())
	                    {
                            case "creationtime":
                                fileDate = _file.CreationTime;
                                break;
                            case "lastwritetime":
                                fileDate = _file.LastWriteTime;
                                break;
		                    default:
                                fileDate = _file.CreationTime;
                                break;
	                    }
                    }

                    if (_file.Exists && fileDate <= _retentionDate)
                    {
                        try
                        {
                            switch (_opType)
                            {
                                case Operations.Delete:   
                                    try 
	                                {	        
		                                _file.Delete();
                                        base.LogItem("FILE: \"" + _file.FullName + "\" was successfully deleted.", twk_LogLevels.Info_Detail, System.Reflection.MethodBase.GetCurrentMethod());
	                                }
	                                catch (Exception ex)
	                                {
                                        base.LogItem("ERROR DELETING FILE: \"" + _file.FullName + "\"" + ex.Message, twk_LogLevels.ProcessError, System.Reflection.MethodBase.GetCurrentMethod());
	                                }
                                    break;
                                case Operations.Move:
                                    if (_moveDirectory != null)
                                    {
                                        if (!_moveDirectory.Exists)
                                        {
                                            _moveDirectory.Create();
                                            base.LogItem("DIRECTORY: \"" + _moveDirectory.FullName + "\" was successfully moved.", twk_LogLevels.Info_Detail, System.Reflection.MethodBase.GetCurrentMethod());
                                        }

                                        try
                                        {
                                            _file.MoveTo(_moveDirectory.FullName + _file.Name);
                                            base.LogItem("FILE: \"" + _file.FullName + "\" was successfully moved.", twk_LogLevels.Info_Detail, System.Reflection.MethodBase.GetCurrentMethod());
                                        }
                                        catch (Exception ex)
                                        {
                                            base.LogItem("Directory error! Processing halted:" + ex.Message, twk_LogLevels.ProcessError, System.Reflection.MethodBase.GetCurrentMethod());
                                            exitLoop = true;
                                        }
                                    }
                                    else
                                    {
                                        base.LogItem("Missing parameter \"movetopath\"", twk_LogLevels.ProcessError, System.Reflection.MethodBase.GetCurrentMethod());
                                    }
                                    break;
                                case Operations.Rename:
                                    // TODO: Not implemented yet
                                    break;
                                case Operations.Email:
                                    // TODO: Not implemented yet
                                    break;
                                case Operations.Scan:
                                    base.LogItem("FILE: \"" + _file.FullName + "\" found.", twk_LogLevels.Info_Detail, System.Reflection.MethodBase.GetCurrentMethod());
                                    break;
                            }

                            if (exitLoop) break;
                        }
                        catch (Exception ex) { base.LogItem("Retention monitor was unable to " + _opType.ToString().ToLower() + " file: " + _file.FullName + " ERROR:" + ex.Message, twk_LogLevels.ProcessError, System.Reflection.MethodBase.GetCurrentMethod()); }
                    }
                }
            }
        }

        private void LoadParameters()
        {
            foreach (XElement xparam in base.ActionXML.Element("parameters").Elements("parameter"))
            {
                if (!String.IsNullOrWhiteSpace(xparam.Attribute("name").Value))
                {
                    String param = xparam.Attribute("name").Value.Trim().ToLower();

                    switch (param)
                    {
                        case "ageinterval":
                            _ageInterval = xparam.Attribute("value").Value;
                            break;
                        case "agevalue":
                            _ageValue = xparam.Attribute("value").Value;
                            break;
                        case "directorypath":
                            _directoryPath = xparam.Attribute("value").Value;
                            break;
                        case "includesubdirectories":
                            bool.TryParse(xparam.Attribute("value").Value, out _includeSubDirectories);
                            break;
                        case "omitfileextensions":
                            _omitFileExtentsions = xparam.Attribute("value").Value;
                            break;
                        case "runasthread":
                            bool.TryParse(xparam.Attribute("value").Value, out _runAsThread);
                            break;
                        case "movetopath":
                            _moveToPath = xparam.Attribute("value").Value;
                            break;
                        case "operation":
                            switch (xparam.Attribute("value").Value.ToLower().Trim())
                            {
                                case "delete_files":
                                    _opType = Operations.Delete;
                                    break;
                                case "move_files":
                                    _opType = Operations.Move;
                                    break;
                                case "rename_files":
                                    _opType = Operations.Rename;
                                    break;
                                case "email_files":
                                    _opType = Operations.Email;
                                    break;
                                case "scan":
                                    _opType = Operations.Scan;
                                    break;
                                default:
                                    _opType = Operations.Scan;
                                    break;
                            }
                            break;
                        case "datetype":
                            _dateType = xparam.Attribute("value").Value;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}