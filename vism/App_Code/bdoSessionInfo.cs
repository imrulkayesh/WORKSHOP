using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for SessionInfo
/// </summary>
public class bdoSessionInfo
{

    int _departmentid;
    int _designationid;
    int _businesscenterid;
    int _companyid;
    string _userId;
    string _userName;
    int _roleId;
    int _personId;
    int _rolePriority;
    long _DispatchId;
    long _GateEntryId;
    long _RequestId;
    string _StaffId;
    int _ModuleId;
    int _MRoomId;
    int _MyTaskId;
    int _MeetingId;
    int _VehicleId;
    int _PicnicId;
    int _ShipmentId;
    int _FreightId;
    int _BudgetId;
    string _UserFullName;
    int _RecvLoc;
    int _RecvWh;
    int _CompanyVehicleTypeId;
    //DataTable _TaskList;
    //DataTable _Modules;
    public bdoSessionInfo()
    {
    }

    #region Common Members

    public string SessionID
    {
        get { return HttpContext.Current.Session.SessionID; }
    }
        #endregion

    public int RecvLoc
    {
        get
        {
            return _RecvLoc;
        }
        set
        {
            _RecvLoc = value;
        }
    }

    public int RecvWh
    {
        get
        {
            return _RecvWh;
        }
        set
        {
            _RecvWh = value;
        }
    }
    
    public int ShipmentId
    {
        get
        {
            return _ShipmentId;
        }
        set
        {
            _ShipmentId = value;
        }
    }
    public int FreightId
    {
        get
        {
            return _FreightId;
        }
        set
        {
            _FreightId = value;
        }
    }
    public int BudgetId
    {
        get
        {
            return _BudgetId;
        }
        set
        {
            _BudgetId = value;
        }
    }
    
    #region Users
    public string UserId
    {
        get
        {
            return _userId;
        }
        set
        {
            _userId = value;
        }
    }

    public string UserName
    {
        get
        {
            return _userName;
        }
        set
        {
            _userName = value;
        }
    }

    public int RoleId
    {
        get
        {
            return _roleId;
        }
        set
        {
            _roleId = value;
        }
    }

    public int RolePriority
    {
        get
        {
            return _rolePriority;
        }
        set
        {
            _rolePriority = value;
        }
    }

    public int PersonId
    {
        get
        {
            return _personId;
        }
        set
        {
            _personId = value;
        }
    }
    public int DepartmentId
    {
        get
        {
            return _departmentid;
        }
        set
        {
            _departmentid = value;
        }
    }
    public int DesignationId
    {
        get
        {
            return _designationid;
        }
        set
        {
            _designationid = value;
        }
    }
    public int BusinessCentetid
    {
        get
        {
            return _businesscenterid;
        }
        set
        {
            _businesscenterid = value;
        }
    }
    public int CompanyId
    {
        get
        {
            return _companyid;
        }
        set
        {
            _companyid = value;
        }
    }
    public long DispatchId
    {
        get
        {
            return _DispatchId;
        }
        set
        {
            _DispatchId = value;
        }
    }
    public long GateEntryId
    {
        get
        {
            return _GateEntryId;
        }
        set
        {
            _GateEntryId = value;
        }
    }
    public long RequestId
    {
        get
        {
            return _RequestId;
        }
        set
        {
            _RequestId = value;
        }
    }
    public string StaffId
    {
        get
        {
            return _StaffId;
        }
        set
        {
            _StaffId = value;
        }
    }
    #endregion
    public int ModuleId
    {
        get
        {
            return _ModuleId;
        }
        set
        {
            _ModuleId = value;
        }
    }
    public int VehicleId
    {
        get
        {
            return _VehicleId;
        }
        set
        {
            _VehicleId = value;
        }
    }
    public int MyTaskId
    {
        get
        {
            return _MyTaskId;
        }
        set
        {
            _MyTaskId = value;
        }
    }
    public int PicnicId
    {
        get
        {
            return _PicnicId;
        }
        set
        {
            _PicnicId = value;
        }
    }
    public int MRoomId
    {
        get
        {
            return _MRoomId;
        }
        set
        {
            _MRoomId = value;
        }
    }
    public int MeetingId
    {
        get
        {
            return _MeetingId;
        }
        set
        {
            _MeetingId = value;
        }
    }
    public string UserFullName
    {
        get
        {
            return _UserFullName;
        }
        set
        {
            _UserFullName = value;
        }
    }

    public int CompanyVehicleTypeId
    {
        get
        {
            return _CompanyVehicleTypeId;
        }
        set
        {
            _CompanyVehicleTypeId = value;
        }
    }

    //public DataTable TaskList
    //{
    //    get
    //    {
    //        return _TaskList;
    //    }
    //    set
    //    {
    //        _TaskList = value;
    //    }
    //}
    //public DataTable ModuleList
    //{
    //    get
    //    {
    //        return _Modules;
    //    }
    //    set
    //    {
    //        _Modules = value;
    //    }
    //}
}
