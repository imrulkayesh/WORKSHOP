using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;

/// <summary>
/// Summary description for dalPerson
/// </summary>
public class dalPerson
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalPerson()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int Insert(string nameEn, string nameBd, string fatherNameEn, string fatherNameBd, string motherNameEn, string motherNameBd, int genderId, int religionId, string nationality, DateTime dob, string birthCertificate, string phone
        , string mobile, string phnHome, string mobileHome, string email, string fax, int bloodGroup, string fatherNID, string motherNID, bool IsFreeDom, bool isTribal, bool isPhyDef, int fatherIncome,
        int motherIncome, string fatherPhn, string motherPhn, string fatherImage, string motherImage, int fatherEducation, int fatherProfession, int motherEducation, int motherProfession, string personImage, string userName)
    {
        dm.AddParameteres("@NameEng", nameEn);
        dm.AddParameteres("@NameBan", nameBd);
        dm.AddParameteres("@FatherNameEng", fatherNameEn);
        dm.AddParameteres("@FatherNameBan", fatherNameBd);
        dm.AddParameteres("@MotherNameEng", motherNameEn);
        dm.AddParameteres("@MotherNameBan", motherNameBd);
        dm.AddParameteres("@GenderId", genderId);
        dm.AddParameteres("@ReligionId", religionId);
        dm.AddParameteres("@Nationality", nationality);
        dm.AddParameteres("@DateofBirth", dob);
        dm.AddParameteres("@BirthCertificate", birthCertificate);
        dm.AddParameteres("@PhoneNo", phone);
        dm.AddParameteres("@Mobile", mobile);
        dm.AddParameteres("@PhoneHome", phnHome);
        dm.AddParameteres("@MobileHome", mobileHome);
        dm.AddParameteres("@Email", email);
        dm.AddParameteres("@Fax", fax);
        dm.AddParameteres("@BloodGroup", bloodGroup);
        dm.AddParameteres("@FatherNId", fatherNID);
        dm.AddParameteres("@MotherNId", motherNID);
        dm.AddParameteres("@IsFreedomFighter", IsFreeDom);
        dm.AddParameteres("@IsTribal", isTribal);
        dm.AddParameteres("@IsPhysicallyDefect", isPhyDef);
        dm.AddParameteres("@FatherIncome", fatherIncome);
        dm.AddParameteres("@MotherIncome", motherIncome);
        dm.AddParameteres("@FatherPhone", fatherPhn);
        dm.AddParameteres("@MotherPhone", motherPhn);
        dm.AddParameteres("@FatherImage", fatherImage);
        dm.AddParameteres("@MotherImage", motherImage);
        dm.AddParameteres("@FatherQualificationId", fatherEducation);
        dm.AddParameteres("@FatherProfessionId", fatherProfession);
        dm.AddParameteres("@MotherQualificationId", motherEducation);
        dm.AddParameteres("@MotherProfessionId", motherProfession);
        dm.AddParameteres("@PersonImage", personImage);
        dm.AddParameteres("@UserName", userName);
        DataTable dt = dm.ExecuteQuery("USP_Person_Insert");
        return Convert.ToInt32(dt.Rows[0][0].ToString());
    }


    public int PersonalInfoInsert(string nameEn, string nameBd, int genderId, int religionId, string nationality, DateTime dob, string birthCertificate, string phone
        , string mobile, string phnHome, string mobileHome, string email, string fax, int bloodGroup, string personImage, int userId)
    {
        dm.AddParameteres("@NameEng", nameEn);
        dm.AddParameteres("@NameBan", nameBd);
        dm.AddParameteres("@GenderId", genderId);
        dm.AddParameteres("@ReligionId", religionId);
        dm.AddParameteres("@Nationality", nationality);
        dm.AddParameteres("@DateofBirth", dob);
        dm.AddParameteres("@BirthCertificate", birthCertificate);
        dm.AddParameteres("@PhoneNo", phone);
        dm.AddParameteres("@Mobile", mobile);
        dm.AddParameteres("@PhoneHome", phnHome);
        dm.AddParameteres("@MobileHome", mobileHome);
        dm.AddParameteres("@Email", email);
        dm.AddParameteres("@Fax", fax);
        dm.AddParameteres("@BloodGroup", bloodGroup);
        dm.AddParameteres("@PersonImage", personImage);
        dm.AddParameteres("@UserId", userId);
        DataTable dt = dm.ExecuteQuery("USP_Person_PersonalInfoInsert");
        return Convert.ToInt32(dt.Rows[0][0].ToString());
    }


    public int Update(int Id, string nameEn, string nameBd, int gender, int religionId, string nationality, DateTime dob, string birthCertificate, string phone
        , string mobile, string phnHome, string mobileHome, string email, string fax, int bloodGroup)
    {
        dm.AddParameteres("@Id", Id);
        dm.AddParameteres("@NameEng", nameEn);
        dm.AddParameteres("@NameBan", nameBd);
        dm.AddParameteres("@Gender", gender);
        dm.AddParameteres("@ReligionId", religionId);
        dm.AddParameteres("@Nationality", nationality);
        dm.AddParameteres("@DateofBirth", dob);
        dm.AddParameteres("@BirthCertificate", birthCertificate);
        dm.AddParameteres("@PhoneNo", phone);
        dm.AddParameteres("@Mobile", mobile);
        dm.AddParameteres("@PhoneHome", phnHome);
        dm.AddParameteres("@MobileHome", mobileHome);
        dm.AddParameteres("@Email", email);
        dm.AddParameteres("@Fax", fax);
        dm.AddParameteres("@BloodGroup", bloodGroup);
        return dm.ExecuteNonQuery("USP_Person_Update");
    }
    public int UserInsert(string userName, string email)
    {
        dm.AddParameteres("@UserName", userName);
        dm.AddParameteres("@Email", email);
        return dm.ExecuteNonQuery("USP_User_Insert");
    }

    public int UpdateFatherInfo(int personId, string fatherNameEn, string fatherNameBd, string fatherNID, int fatherIncome, string fatherPhn, int fatherEducation, int fatherProfession)
    {
        dm.AddParameteres("@Id", personId);
        dm.AddParameteres("@FatherNameEng", fatherNameEn);
        dm.AddParameteres("@FatherNameBan", fatherNameBd);
        dm.AddParameteres("@FatherNId", fatherNID);
        dm.AddParameteres("@FatherIncome", fatherIncome);
        dm.AddParameteres("@FatherPhone", fatherPhn);
        dm.AddParameteres("@FatherQualificationId", fatherEducation);
        dm.AddParameteres("@FatherProfessionId", fatherProfession);
        return dm.ExecuteNonQuery("USP_FatherInformation_Update");
    }
    public int UpdateMotherInfo(int personId, string motherNameEn, string motherNameBd, string motherNID, int motherIncome, string motherPhn, int motherEducation, int motherProfession)
    {
        dm.AddParameteres("@Id", personId);
        dm.AddParameteres("@MotherNameEng", motherNameEn);
        dm.AddParameteres("@MotherNameBan", motherNameBd);
        dm.AddParameteres("@MotherNId", motherNID);
        dm.AddParameteres("@MotherIncome", motherIncome);
        dm.AddParameteres("@MotherPhone", motherPhn);
        dm.AddParameteres("@MotherQualificationId", motherEducation);
        dm.AddParameteres("@MotherProfessionId", motherProfession);
        return dm.ExecuteNonQuery("USP_MotherInformation_Update");
    }
    public int UpdateOtherInfo(int personId, bool IsFreeDom, bool isTribal, bool isPhyDef)
    {
        dm.AddParameteres("@Id", personId);
        dm.AddParameteres("@IsFreedomFighter", IsFreeDom);
        dm.AddParameteres("@IsTribal", isTribal);
        dm.AddParameteres("@IsPhysicallyDefect", isPhyDef);
        return dm.ExecuteNonQuery("USP_OtherInfo_Update");
    }

    public int UpdateAllImage(int personId, string personImage, string fatherImage, string motherImage)
    {
        dm.AddParameteres("@PersonId", personId);
        dm.AddParameteres("@PersonImage", personImage);
        dm.AddParameteres("@FatherImage", fatherImage);
        dm.AddParameteres("@MotherImage", motherImage);
        return dm.ExecuteNonQuery("USP_Person_AllImageUpdate");
    }

    public int PresentAddressInsert(int PersonId, int divisionId, int districtId, int thanaId, string postOffice, string postCode, string address)
    {
        dm.AddParameteres("@PersonId", PersonId);
        dm.AddParameteres("@DivisionId", divisionId);
        dm.AddParameteres("@DistrictId", districtId);
        dm.AddParameteres("@ThanaId", thanaId);
        dm.AddParameteres("@PostOffice", postOffice);
        dm.AddParameteres("@PostCode", postCode);
        dm.AddParameteres("@Address", address);
        return dm.ExecuteNonQuery("USP_Person_PresentAddressInsert");
    }
    public int PresentAddressUpdate(int PersonId, int divisionId, int districtId, int thanaId, string postOffice, string postCode, string address)
    {
        dm.AddParameteres("@PersonId", PersonId);
        dm.AddParameteres("@DivisionId", divisionId);
        dm.AddParameteres("@DistrictId", districtId);
        dm.AddParameteres("@ThanaId", thanaId);
        dm.AddParameteres("@PostOffice", postOffice);
        dm.AddParameteres("@PostCode", postCode);
        dm.AddParameteres("@Address", address);
        return dm.ExecuteNonQuery("USP_Person_PresentAddressUpdate");
    }
    public int PermanentAddressInsert(int PersonId, int divisionId, int districtId, int thanaId, string postOffice, string postCode, string address)
    {
        dm.AddParameteres("@PersonId", PersonId);
        dm.AddParameteres("@DivisionId", divisionId);
        dm.AddParameteres("@DistrictId", districtId);
        dm.AddParameteres("@ThanaId", thanaId);
        dm.AddParameteres("@PostOffice", postOffice);
        dm.AddParameteres("@PostCode", postCode);
        dm.AddParameteres("@Address", address);
        return dm.ExecuteNonQuery("USP_Person_PermanentAddressInsert");
    }

    public int PermanentAddressUpdate(int PersonId, int divisionId, int districtId, int thanaId, string postOffice, string postCode, string address)
    {
        dm.AddParameteres("@PersonId", PersonId);
        dm.AddParameteres("@DivisionId", divisionId);
        dm.AddParameteres("@DistrictId", districtId);
        dm.AddParameteres("@ThanaId", thanaId);
        dm.AddParameteres("@PostOffice", postOffice);
        dm.AddParameteres("@PostCode", postCode);
        dm.AddParameteres("@Address", address);
        return dm.ExecuteNonQuery("USP_Person_PermanentAddressUpdate");
    }

    public DataTable GetByUserName(string userName)
    {
        dm.AddParameteres("@UserName", userName);
        return dm.ExecuteQuery("USP_Person_GetByUserName");
    }

    public DataTable GetByPersonId(int personId)
    {
        dm.AddParameteres("@PersonId", personId);
        return dm.ExecuteQuery("USP_Person_GetByPersonId");
    }

    public int PersonImageUpdate(int personId, string img)
    {
        dm.AddParameteres("@PersonId", personId);
        dm.AddParameteres("@Photo", img);
        return dm.ExecuteNonQuery("USP_Person_ImageUpdate");
    }
    public int FatherImageUpdate(int personId, string img)
    {
        dm.AddParameteres("@PersonId", personId);
        dm.AddParameteres("@Photo", img);
        return dm.ExecuteNonQuery("USP_Person_FatherImageUpdate");
    }
    public int MotherImageUpdate(int personId, string img)
    {
        dm.AddParameteres("@PersonId", personId);
        dm.AddParameteres("@Photo", img);
        return dm.ExecuteNonQuery("USP_Person_MotherImageUpdate");
    }
}