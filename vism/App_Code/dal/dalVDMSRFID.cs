using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalVDMSRFID
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalVDMSRFID()
    {
    }
    public DataTable VehicleINOUT()
    {
       return dm.ExecuteQuery(@"SELECT * FROM VD_RFIDRECORDS RR INNER JOIN VD_RFIDREADERS RL ON RL.ID=RR.CARDLOCATIONID
                INNER JOIN BS_BUSINESSCENTER B ON B.ID=RL.BUSINESSCENTERID INNER JOIN VD_RFIDCARDS RV ON RV.ID=RR.CARDID");
    }

    public DataTable VehicleSearch(string Criteria)
    {
        return dm.ExecuteQuery(@"SELECT RR.*, RL.READERLOCATION,B.CENTERNAME,RV.CARDTAGID,RV.CHASSISNO FROM VD_RFIDRECORDS RR 
            INNER JOIN VD_RFIDREADERS RL ON RL.ID=RR.CARDLOCATIONID
            INNER JOIN BS_BUSINESSCENTER B ON B.ID=RL.BUSINESSCENTERID 
            INNER JOIN VD_RFIDCARDS RV ON RV.ID=RR.CARDID WHERE B.ID>0" + Criteria);
    }
}