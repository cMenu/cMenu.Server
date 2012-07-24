using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cMenu.Common
{
    public enum EnRegistryPathType
    {
        ELocalMachine = 0,
        ECurrentUser = 1
    }
    public enum EnRegistryLocalPathType
    {
        EnModulesPath = 0,
        EnApplicationConfig = 1,
        EnRootPath = 2
    }
    public enum EnFsSerializerType
    {
        EBin = 0,
        EXML = 1,
        EJSON = 2
    }
    public enum EnLogType
    {
        EnDebug = 0,
        EnTrace = 1,
        EnError = 2,
        EnFatal = 3,
        EnInfo = 4
    }
    public enum EnLogTarget
    {
        EnEventLog = 0,
        EnFileLog = 1,
        EnMemoryLog = 2
    }
    public enum EnServerState
    {
        EWorking = 0,
        EStopped = 1,
        ENoConnection = 2
    }        
    public enum EnServerDB
    {
        EMsSQL = 0,
        EMySQL = 1,
        EOralce = 2,
        ESQLLite = 3,
        EUnknown = 4
    }
    public enum EnUserInfoType
    {
        EAllSimple = 0,
        EAllComplex = 1,
        EOneSimple = 2,
        EOneComplex = 3
    }
    public enum EnJournalInfoType
    {
        EAllSimple = 0,
        EAllComplex = 1,
        EOneSimple = 2,
        EOneComplex = 3
    }
    public enum EnJournalStatType
    {
        EILOperation = 0
    }
    public enum EnUserGroupOperation
    {
        EAdd = 0,
        EUpdate = 1,
        EDelete = 2
    }
    public enum EnUserGroupGetOperation
    {
        EAll = 0,
        EOne = 1
    }
    public enum EnErrorType
    {
        EError = 0,
        EInformation = 1,
        EExclamation = 2,
        EQuestion = 3,
        EWarning = 4
    }
    public enum EnPolicyValue
    {
        ETrue = 0,
        EFalse = 1,
        EUser = 2
    }

}
