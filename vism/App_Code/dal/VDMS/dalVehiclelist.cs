using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalVehiclelist
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalVehiclelist()
    {

    }

    public DataTable AllpoolVehicleList(string Criteria)
    {
        return dm.ExecuteQuery(@"SELECT V.CHASSISNO, V.SEATCAPACITY, V.REGNO, D.STAFFID, E.EMPLOYEENAME , E.OFFICEMOBILE FROM VD_VEHICLE V 
        INNER JOIN VD_DRIVER D ON D.ID = V.BUSINESSCENTERID
        INNER JOIN EM_EMPLOYEE E ON E.STAFFID = D.STAFFID WHERE " + Criteria );
    }
}