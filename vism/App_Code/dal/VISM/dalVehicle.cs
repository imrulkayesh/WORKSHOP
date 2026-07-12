using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalVehicle
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalVehicle()
    {
    }
    //int OriginId, int CountryId,
    public int Insert(int CustId, string ChassisNo, string RegNo, string EngineNo, int VehicleTypeId, int EningeModelId, int ManufYear, int ManufacturerId, string vin, decimal EngineCapacity, int BrandId, int ModelId, int BodyTypeId, int OriginId, int CountryId, int FuelTypeId, int NoofCylinder, decimal CylinderCapacity, string WheelBase, int SeatingCapacity, string ladenWeight, string UnladenWeight, string TyreSize, int NoofTyres, int NoofAxles, int TypeofUseId, decimal CostPrice, int CostCurrency, decimal SellingPrice, int SellingCurrency, int CompanyId, int BusinessCenterId, string AxleWeight, decimal HorsePower, decimal rpm, string ImportDate, string drApprover, string DrawingFile, string drApvdate)
    {
        int vehicleId = 0;
        dm.ExecuteNonQuery(@"Insert into RVS_VEHICLE(ID,CUSTOMERID,CHASSISNO,REGNO,ENGINENO,VEHICLETYPEID,ENGINEMODELID,MANUFACTURINGYEAR,MANUFACTURERID,VIN,ENGINECAPACITY,BRANDID,MODELID,BODYTYPEID,COUNTRYOFORIGINID,COUNTRYID,FUELYTYPEID,NOOFCYLINDER,CYLINDERCAPACITY,WHEELBASE,SEATINGCAPACITY,LADENWEIGHT,UNLADENWEIGHT,TYRESIZE,NOOFTYRES,NOOFAXLE,TYPEOFUSEID,CREATEDBY,COSTPRICE,COSTCURRENCYID,SELLINGPRICE,SELLINGCURRENCYID,COMPANYID,BUSINESSCENTERID,AXLEWEIGHT,HORSEPOWER,RPM,IMPORTDATE,DRAWINGFILE,DRAPPROVER,DRDATE)
        VALUES(SQ_RVS_VEHICLE.NEXTVAL,'" + CustId + "','" + ChassisNo + "','" + RegNo + "','" + EngineNo + "','" + VehicleTypeId + "','" + EningeModelId + "','" + ManufYear + "','" + ManufacturerId + "','" + vin + "','" + EngineCapacity + "','" + BrandId + "','" + ModelId + "','" + BodyTypeId + "','" + OriginId + "','"+CountryId+"','" + FuelTypeId + "','" + NoofCylinder + "','" + CylinderCapacity + "','" + WheelBase + "','" + SeatingCapacity + "','" + ladenWeight + "','" + UnladenWeight + "','" + TyreSize + "','" + NoofTyres + "','" + NoofAxles + "','" + TypeofUseId + "','" + SessionManager.SessionName.StaffId + "','" + CostPrice + "','" + CostCurrency + "','" + SellingPrice + "','" + SellingCurrency + "','" + CompanyId + "','" + BusinessCenterId + "','" + AxleWeight + "','" + HorsePower + "','" + rpm + "', TO_DATE('" + ImportDate + "','DD/MM/YYYY'),'" + DrawingFile + "','" + drApprover + "', TO_DATE('" + drApvdate + "','DD/MM/YYYY'))");
        DataTable dt = dm.ExecuteQuery("SELECT ID FROM RVS_VEHICLE WHERE CHASSISNO='" + ChassisNo + "'");
        if (dt.Rows.Count > 0)
        {
            vehicleId = Convert.ToInt32(dt.Rows[0][0]);
        }
        return vehicleId;
    }

    public DataTable GetOwnerDetails(int id)
    {
        return dm.ExecuteQuery(@"SELECT * FROM RVS_SALES S INNER JOIN RVS_OWNERS O ON O.SALESID=S.ID
        INNER JOIN RVS_CUSTOMER C ON C.ID=O.CUSTOMERID
        INNER JOIN BS_COUNTRY CN ON CN.ID=C.COUNTRYID
        INNER JOIN BS_DIVISION DV ON DV.ID=C.DIVISIONID
        INNER JOIN BS_DISTRICT DS ON DS.ID=C.DISTRICTID
        INNER JOIN BS_THANA TH ON TH.ID=C.THANAID
        INNER JOIN BS_GENDER G ON G.ID=C.GENDERID
        INNER JOIN ASSET_CURRENCY CR ON CR.ID=S.CURRENCYID
        INNER JOIN BS_RELIGION RG ON RG.ID=C.RELIGIONID
        where S.VEHICLEID=" + id);
    }

    public DataTable getVehicleDetails(int id)
    {
        return dm.ExecuteQuery(@"SELECT * FROM RVS_VEHICLE V INNER JOIN RVS_BRAND B ON B.ID=V.BRANDID
        INNER JOIN RVS_MODEL MD ON MD.ID=V.MODELID INNER JOIN RVS_MANUFACTURER M ON M.ID=V.MANUFACTURERID
        INNER JOIN RVS_USETYPE UT ON UT.ID=V.TYPEOFUSEID
        INNER JOIN RVS_BODYTYPE BT ON BT.ID=V.BODYTYPEID
        INNER JOIN RVS_ENGINEMODEL EM ON EM.ID=V.ENGINEMODELID
        INNER JOIN RVS_FUELTYPE FT ON FT.ID=V.FUELYTYPEID
        INNER JOIN RVS_VEHICLETYPE VT ON VT.ID=V.VEHICLETYPEID
        INNER JOIN BS_COUNTRY C ON C.ID=V.COUNTRYOFORIGINID
        INNER JOIN ASSET_CURRENCY CR ON CR.ID=V.SELLINGCURRENCYID WHERE V.ID=" + id);
    }

    public DataTable VehicleDetailReport(int id)
    {
        return dm.ExecuteQuery(@"SELECT V.*, M.MODELNAME, C.CUSTOMERNAME, VT.VEHICLETYPENAME FROM RVS_VEHICLE V
            JOIN RVS_MODEL M ON M.ID = V.MODELID 
            JOIN RVS_CUSTOMER C ON C.ID = V.CUSTOMERID
            JOIN RVS_VEHICLETYPE VT ON VT.ID = V.VEHICLETYPEID
            WHERE V.ID =" + id);
    }
    //int OriginId, int CountryId,
    public int Update(int id, int CustId, string ChassisNo, string RegNo, string EngineNo, int VehicleTypeId, int EningeModelId, int ManufYear, int ManufacturerId, string vin, decimal EngineCapacity, int BrandId, int ModelId, int BodyTypeId, int OriginId, int CountryId, int FuelTypeId, int NoofCylinder, decimal CylinderCapacity, string WheelBase, int SeatingCapacity, string ladenWeight, string UnladenWeight, string TyreSize, int NoofTyres, int NoofAxles, int TypeofUseId, decimal CostPrice, int CostCurrency, decimal SellingPrice, int SellingCurrency, int CompanyId, int BusinessCenterId, string AxleWeight, decimal HorsePower, decimal rpm, string ImportDate, string drApprover, string DrawingFile, string drApvdate)
    {
        return dm.ExecuteNonQuery("update RVS_VEHICLE set CUSTOMERID='" + CustId + "', CHASSISNO='" + ChassisNo + "',REGNO='" + RegNo + "',ENGINENO='" + EngineNo + "',VEHICLETYPEID='" + VehicleTypeId + "',ENGINEMODELID='" + EningeModelId + "',MANUFACTURINGYEAR='" + ManufYear + "',MANUFACTURERID='" + ManufacturerId + "',VIN='" + vin + "',ENGINECAPACITY='" + EngineCapacity + "',BRANDID='" + BrandId + "',MODELID='" + ModelId + "',BODYTYPEID='" + BodyTypeId + "',COUNTRYOFORIGINID='" + OriginId + "',COUNTRYID='"+ CountryId + "',FUELYTYPEID='" + FuelTypeId + "',NOOFCYLINDER='" + NoofCylinder + "',CYLINDERCAPACITY='" + CylinderCapacity + "',WHEELBASE='" + WheelBase + "',SEATINGCAPACITY='" + SeatingCapacity + "',LADENWEIGHT='" + ladenWeight + "',UNLADENWEIGHT='" + UnladenWeight + "',TYRESIZE='" + TyreSize + "',NOOFTYRES='" + NoofTyres + "',NOOFAXLE='" + NoofAxles + "',TYPEOFUSEID='" + TypeofUseId + "',DRAWINGFILE='" + DrawingFile + "',DRAPPROVER='" + drApprover + "',DRDATE=TO_DATE('" + drApvdate + "','DD/MM/YYYY'),UPDATEDBY='" + SessionManager.SessionName.StaffId + "',IMPORTDATE=TO_DATE('" + ImportDate + "','DD/MM/YYYY'),UPDATEDATE=SYSDATE,COSTPRICE='" + CostPrice + "',COSTCURRENCYID='" + CostCurrency + "',SELLINGPRICE='" + SellingPrice + "',SELLINGCURRENCYID='" + SellingCurrency + "',COMPANYID='" + CompanyId + "',BUSINESSCENTERID='" + BusinessCenterId + "',AXLEWEIGHT='" + AxleWeight + "',HORSEPOWER='" + HorsePower + "',RPM='" + rpm + "' WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery(@"Select vh.id,vh.REGNO,vh.CHASSISNO,vh.ENGINENO,mo.MODELNAME,mn.MANUFACTURERNAME,
TO_DATE(vh.MANUFACTURINGYEAR,'DD/MM/YY') MANUFACTURINGYEAR,vt.VEHICLETYPENAME,ft.FUELTYPENAME,bc.CENTERNAME,
co.COMPANYNAME,cn.COUNTRYNAME,vh.DRIVERNAME,vh.DRIVERMOBILE,vh.DRIVERID, NVL(VH.CNGCYLINDERREDDATE,NULL) CNGCYLINDERREDDATE,cu.CUSTOMERNAME,cu.ADDRESS,cu.MOBILE,cu.EMAIL,cu.NID,cu.CC1,cu.CC2,vh.CUSTOMERID
from rvs_Vehicle vh
left join rvs_model mo on vh.MODELID=mo.ID
left join RVS_MANUFACTURER mn on vh.MANUFACTURERID=mn.ID
left join RVS_VEHICLETYPE vt on vh.VEHICLETYPEID=vt.ID
left join RVS_FUELTYPE ft on vh.FUELYTYPEID=ft.ID
left join bs_businesscenter bc on vh.BUSINESSCENTERID=bc.ID
left join RVS_CUSTOMER cu on vh.CUSTOMERID=cu.ID
left join bs_company co on vh.COMPANYID=co.id
left join bs_country cn on vh.COUNTRYID=cn.ID WHERE vh.id='" + id + "' and vh.ISACTIVE='1'");
        //return dm.ExecuteQuery("Select * from RVS_VEHICLE WHERE ID=" + id);
    }
    public DataTable GetByJobId(int id)
    {
        return dm.ExecuteQuery("Select * from RVS_JOBCARD WHERE ID=" + id);
    }
    //public DataTable GetOutPendingCheckByVehicleId(int id)
    //{
    //    return dm.ExecuteQuery("select * from RVS_GATEINOUT WHERE ISOUT=0 AND VEHICLEID=" + id);
    //}
    public DataTable VehicleInfo(string crt )
    {
        //return dm.ExecuteQuery(@"SELECT V.*, M.MODELNAME FROM RVS_VEHICLE V 
        //INNER JOIN RVS_MODEL M ON M.ID = V.MODELID");
        return dm.ExecuteQuery(@"SELECT V.*, M.MODELNAME FROM RVS_VEHICLE V 
      INNER JOIN RVS_MODEL M ON M.ID = V.MODELID"+crt);

    }
    public DataTable VehicleInfo()
    {
        //return dm.ExecuteQuery(@"SELECT V.*, M.MODELNAME FROM RVS_VEHICLE V 
        //INNER JOIN RVS_MODEL M ON M.ID = V.MODELID");
        return dm.ExecuteQuery(@"SELECT V.*, M.MODELNAME FROM RVS_VEHICLE V 
      INNER JOIN RVS_MODEL M ON M.ID = V.MODELID");

    }
    public DataTable GetByChassisNo(string ChassisNo)
    {
        return dm.ExecuteQuery(@"Select * from RVS_VEHICLE V INNER JOIN RVS_BRAND B ON B.ID=V.BRANDID INNER JOIN RVS_MODEL M ON M.ID=V.MODELID
        INNER JOIN RVS_MANUFACTURER MC ON MC.ID=V.MANUFACTURERID WHERE V.CHASSISNO='" + ChassisNo + "'");
    }
    public bool CheckDuplicate(string ChassisNo)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RVS_VEHICLE WHERE CHASSISNO='" + ChassisNo + "'");
        if (dt.Rows.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}