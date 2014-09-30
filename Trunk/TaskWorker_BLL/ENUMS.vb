Public Enum twk_LogLevels
    Info_Success = 1
    Info_Detail = 2

    Warning = 20
    ProcessError = 30

    DebugInfo_L1 = 101
    DebugInfo_L2 = 102
    DebugInfo_L3 = 103
    DebugInfo_L4 = 104
End Enum
'1,2,20,30,101,102,103,104
Public Enum twk_LogTo
    only_to_sql = 1
    sql_and_file = 2
    only_to_file = 3
    only_to_console = 4
    sql_and_file_and_console = 5
End Enum

Public Enum twk_SQLResults
    Complete = 1
    SQLError = 2
    RecordAlreadyExists = 3
    NoRecordFound = 4
End Enum