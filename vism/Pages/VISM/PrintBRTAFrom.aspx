<%@ Page Title="Print BRTA Application" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="PrintBRTAFrom.aspx.cs" Inherits="Pages_VISM_PrintBRTAFrom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .text-left {
            text-align: left;
        }

        .text-right {
            text-align: right;
        }

        .text-center {
            text-align: center;
        }

        .text-justify {
            text-align: justify;
        }

        .text-nowrap {
            white-space: nowrap;
        }

        .text-lowercase {
            text-transform: lowercase;
        }

        .text-uppercase {
            text-transform: uppercase;
        }

        .text-capitalize {
            text-transform: capitalize;
        }

        .img-thumbnail {
            display: inline-block;
            max-width: 100%;
            height: auto;
            padding: 4px;
            line-height: 1.42857143;
            background-color: #fff;
            border: 0;
            border-radius: 4px;
            -webkit-transition: all .2s ease-in-out;
            -o-transition: all .2s ease-in-out;
            transition: all .2s ease-in-out;
        }

        .panel-title {
            margin-top: 0;
            margin-bottom: 0;
            font-size: 11px;
            color: inherit;
        }

        .table {
            width: 100%;
            max-width: 100%;
            margin-bottom: 10px;
        }

        .table {
            border-collapse: collapse !important;
        }

            .table td, .table th {
                background-color: #fff !important;
            }

            .table th, .table td {
                border: 0;
            }

            .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
                padding: 4px;
                line-height: 1.12857143;
                vertical-align: top;
                border-top: 0;
                text-align: left;
                font-size: 11px;
            }

            .table > thead > tr > th {
                vertical-align: bottom;
                border-bottom: 0;
            }

            .table > caption + thead > tr:first-child > th, .table > colgroup + thead > tr:first-child > th, .table > thead:first-child > tr:first-child > th, .table > caption + thead > tr:first-child > td, .table > colgroup + thead > tr:first-child > td, .table > thead:first-child > tr:first-child > td {
                border-top: 0;
            }

            .table > tbody + tbody {
                border-top: 0;
            }

            .table .table {
                background-color: #fff;
            }

        .table {
            border: 0;
        }

            .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
                border: 0;
            }

            .table > thead > tr > th, .table > thead > tr > td {
                border-bottom-width: 0;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <button type="button" onclick="javascript:PrintPanel(pnlPage1)" class="btn btn-primary">Print Page 1</button>
    <button type="button" class="btn btn-primary" onclick="javascript:PrintPanel(pnlPage2)">Print Page 2</button>
    <button type="button" class="btn btn-primary" onclick="javascript:PrintPanel(pnlPage3)">Print Page 3</button>
    <button type="button" class="btn btn-primary" onclick="javascript:PrintPanel(pnlPage4)">Print Page 4</button>
    <button type="button" class="btn btn-primary" onclick="javascript:PrintPanel(pnlPage5)">Print Page 5</button>
    <hr />
    <asp:Panel ID="pnlPage1" runat="server" ClientIDMode="Static">
        <center><h4 style="margin:0; padding:0">FORM OF APPLICATION FOR THE REGISTRATION OF MOTOR VEHICLE</h4>
           <small>To be filled in by the office<br />            <b>Section-1</b></small> 
        </center>
        <table class="table">
            <tr>
                <td>Regn No : </td>
                <td></td>
                <td>Date : </td>
                <td></td>
                <td>Prev. Regn. No.(If any)</td>
                <td></td>
            </tr>
            <tr>
                <td>Issue No : </td>
                <td></td>
                <td>Date : </td>
                <td></td>
                <td>Issue by : </td>
                <td></td>
            </tr>
            <tr>
                <td>Diary No : </td>
                <td></td>
                <td>Date : </td>
                <td></td>
                <td>Received by : </td>
                <td></td>
            </tr>
            <tr>
                <td>Customer ID : </td>
                <td></td>
                <td>District : </td>
                <td></td>
                <td>Vehicle ID : </td>
                <td></td>
            </tr>
            <tr>
                <td>Veh. Description : </td>
                <td></td>
                <td></td>
                <td></td>
                <td>Call non date : </td>
                <td></td>
            </tr>
            <tr>
                <td>Refuse date : </td>
                <td></td>
                <td>Refusal Code : </td>
                <td></td>
                <td>Refused by : </td>
                <td></td>
            </tr>
            <tr>
                <td>P.O./Bank : </td>
                <td></td>
                <td></td>
                <td></td>
                <td>Index : </td>
                <td></td>
            </tr>
            <tr>
                <td>Remarks (if any) : </td>
                <td></td>
                <td></td>
                <td></td>
                <td>Index No : </td>
                <td></td>
            </tr>
        </table>
        <center><small>
            To be filled in by the Owner<br />
            <b>Section II</b><br />
            (Owner information)</small>
        </center>
        <table class="table">
            <tr>
                <td>1. Name of owner : </td>
                <td><asp:Label ID="lblPage1OwnerName" EnableViewState="false" runat="server"></asp:Label></td>
                <td>2. Date of birth : </td>
                <td><asp:Label ID="lblPage1OwnerDob" EnableViewState="false" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>3. Father/Husband : </td>
                <td><asp:Label ID="lblPage1Father" EnableViewState="false" runat="server"></asp:Label></td>
                <td>4. Nationality : </td>
                <td><asp:Label ID="lblPage1Nationality" EnableViewState="false" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>5. Sex : </td>
                <td><asp:Label ID="lblPage1Gender" EnableViewState="false" runat="server"></asp:Label></td>
                <td>6. Guardian's name : </td>
                <td><asp:Label ID="lblPage1Guardian" EnableViewState="false" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>7. Owner's Address(One only) : </td>
                <td colspan="3"><asp:Label ID="lblPage1Address" EnableViewState="false" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>8. Phone No.(if any) : </td>
                <td><asp:Label ID="lblPage1Phone" EnableViewState="false" runat="server"></asp:Label></td>
                <td>9. PO/Bank : </td>
                <td><asp:Label ID="lblPage1Bank" EnableViewState="false" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>10. Joint owner : </td>
                <td><asp:Label ID="lblPage1JointOwner" EnableViewState="false" runat="server"></asp:Label></td>
                <td>11. Owner type : </td>
                <td><asp:Label ID="lblPage1OwnerType" EnableViewState="false" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>12. Hire : </td>
                <td><asp:Label ID="lblPage1Hire" EnableViewState="false" runat="server"></asp:Label></td>
                <td>13. Hire purchase : </td>
                <td><asp:Label ID="lblPage1HirePurchase" EnableViewState="false" runat="server"></asp:Label></td>
            </tr>
        </table>
        <center>
           <small> To be filled in by the Owner<br />
            <b>Section III</b><br />
            (Owner information)</small>
        </center>
        <table class="table">
            <tr>
                <td>14. Vehicle or trailer : </td>
                <td><asp:Label ID="lblPage1Vehicle" EnableViewState="false" runat="server"></asp:Label></td>
                <td>15. Prev. Regn. No. (if any) : </td>
                <td><asp:Label ID="lblPage1RegNo" EnableViewState="false" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>14a. Class of vehicle : </td>
                <td><asp:Label ID="lblPage1VehicleType" EnableViewState="false" runat="server"></asp:Label></td>
                <td>15a. Maker’s name : </td>
                <td><asp:Label ID="lblPage1ManuFacturer" EnableViewState="false" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>16. Type of body : </td>
                <td><asp:Label ID="lblPage1TypeOfBody" EnableViewState="false" runat="server"></asp:Label></td>
                <td>17. Maker’s Country : </td>
                <td><asp:Label ID="lblPage1CountryofOrigin" EnableViewState="false" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>18. Color (cabin/body) : </td>
                <td><asp:Label ID="lblPage1ColorOfBody" EnableViewState="false" runat="server"></asp:Label></td>
                <td>19. Year of manufacture</td>
                <td><asp:Label ID="lblPage1YearofManuf" EnableViewState="false" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>20. Number of cylinders : </td>
                <td><asp:Label ID="lblPage1NoofCyl" EnableViewState="false" runat="server"></asp:Label></td>
                <td>21. Chassis number : </td>
                <td><asp:Label ID="lblPage1ChassisNo" EnableViewState="false" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>22. Engine number : </td>
                <td><asp:Label ID="lblPage1EngineNo" EnableViewState="false" runat="server"></asp:Label></td>
                <td>23. Fuel used : </td>
                <td><asp:Label ID="lblPage1FuelType" EnableViewState="false" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>24. Horse power : </td>
                <td><asp:Label ID="lblPage1HorsePower" EnableViewState="false" runat="server"></asp:Label></td>
                <td>25. RPM : </td>
                <td><asp:Label ID="lblPage1Rpm" EnableViewState="false" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>26. Cubic capacity : </td>
                <td><asp:Label ID="lblPage1EngineCapacity" EnableViewState="false" runat="server"></asp:Label></td>
                <td>27. Seats (incl. driver) : </td>
                <td><asp:Label ID="lblPage1Seats" EnableViewState="false" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>28. No. of Standee : </td>
                <td><asp:Label ID="lblPage1NoofStandee" EnableViewState="false" runat="server"></asp:Label></td>
                <td>29. Wheel base : </td>
                <td><asp:Label ID="lblPage1WheelBase" EnableViewState="false" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>30. Unladen weight (kg) : </td>
                <td><asp:Label ID="lblPage1UnladenWeight" EnableViewState="false" runat="server"></asp:Label></td>
                <td>31. Maximum laden/train weight (kg) : </td>
                <td><asp:Label ID="lblPage1LadenWeight" EnableViewState="false" runat="server"></asp:Label></td>
            </tr>
        </table>
        <center>
            <small>
            <b>Section III</b><br />
            (Additional informtion for transport vehicle)</small>
        </center>
        <table class="table">
            <tr>
                <td>32. No. of tyres : </td>
                <td><asp:Label ID="lblPage1NoofTyres" EnableViewState="false" runat="server"></asp:Label></td>
                <td>33. Tyres size : </td>
                <td><asp:Label ID="lblPage1TyreSize" EnableViewState="false" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>34. No. of axle : </td>
                <td><asp:Label ID="lblPage1NoofAxle" EnableViewState="false" runat="server"></asp:Label></td>
                <td>35. Maximum axle weight (kg) :
                    <br />
                    &nbsp;&nbsp;&nbsp;  a) Front axle (1)<br />
                    &nbsp;&nbsp;&nbsp;  b) Central axle(1)
                    <br />
                    &nbsp;&nbsp; &nbsp;  c) Rear axle (1) 
                </td>
                <td>
                    <br />
                    (2)<br />
                    (2) &nbsp;&nbsp;(3)<br />
                    (2)&nbsp;&nbsp; (3)
                </td>
            </tr>
            <tr>
                <td>36. Dimensions (mm)
                    <br />
                    &nbsp;&nbsp; &nbsp;  a) Overall length
                </td>
                <td>
                    <br />
                </td>
                <td>
                    <br />
                    b) Overall width </td>
                <td>
                    <br />
                    c) Overall height</td>
            </tr>
            <tr>
                <td>37. Overhangs (%) :
                    <br />
                    &nbsp;&nbsp; &nbsp; a) Front
                </td>
                <td>
                    <br />
                </td>
                <td>
                    <br />
                    b) Rear</td>
                <td>
                    <br />
                    c) Other</td>
            </tr>
            <tr>
                <td colspan="4">38. A copy of the drawing showing the vehicle dimension specifications of the body and of the seating
arrangements approved by <asp:Label ID="lblPage1DrawingApprover" EnableViewState="false" runat="server"></asp:Label> on <asp:Label ID="lblPage1DrawingApvDate" EnableViewState="false" runat="server"></asp:Label> is attached herewith.</td>
            </tr>
        </table>
    </asp:Panel>
    <hr />
    <asp:Panel ID="pnlPage2" runat="server" ClientIDMode="Static">
        <center>
            <small>
            <b>Section V</b></small>
        </center>
        <table class="table">
            <tr>
                <td colspan="4">39. Hire purchase/hypothecation information :<br />
                    &nbsp;&nbsp;&nbsp; &nbsp; The vehicle is subject to hire purchase/hypothecation with :
                </td>
            </tr>
            <tr>
                <td>&nbsp;&nbsp;&nbsp; &nbsp;a) Name :
                    <br />
                    &nbsp;&nbsp;&nbsp; &nbsp;Address : 
                </td>
                <td></td>
                <td>b) Date : </td>
            </tr>
            <tr>
                <td>40. Insurance information :
                    <br />
                    &nbsp;&nbsp;&nbsp; a) Policy no :
                </td>
                <td>
                    <br />
                    b) Type of policy :</td>
                <td>
                    <br />
                    c) Insurer’s name & address :</td>
                <td>
                    <br />
                    b) Date of expiry :</td>
            </tr>
            <tr>
                <td colspan="4">41. Joint owner information :
                </td>
            </tr>
            <tr>
                <td>a) Name :</td>
                <td><asp:Label ID="Page2JointOwner1Name" EnableViewState="false" runat="server"></asp:Label> </td>
                <td>b) Name :</td>
                <td><asp:Label ID="Page2JointOwner2Name" EnableViewState="false" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>Father/Husband :</td>
                <td><asp:Label ID="Page2JointOwner1Father" EnableViewState="false" runat="server"></asp:Label></td>
                <td>Father/Husband :</td>
                <td><asp:Label ID="Page2JointOwner2Father" EnableViewState="false" runat="server"></asp:Label></td>
            </tr>
        </table>
        <center>
            <small>
            <b>Section VI</b><br />
                (Declaration, Certificates and documents)
            </small>
        </center>
        <table class="table">
            <tr>
                <td colspan="2">42. Declaration by owner :</td>
            </tr>
            <tr>
                <td colspan="2" style="padding-left: 20px; text-align: justify;">a) I the undersigned do hereby declare that to the best of my knowledge and belief, the information given and the
documents enclosed (as per list attached) are true. I also declare that in case the papers/documents and information
furnished are found to be incorrect at any later stage, I shall be liable for legal action.
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <br />
                    Date :</td>
                <td style="width: 20%; text-align: center">
                    <br />
                    <br />
                    Signature of owner<br />
                    Seal</td>
            </tr>
            <tr>
                <td colspan="2">Encl : List of documents</td>
            </tr>
            <tr>
                <td colspan="2">43. Registered dealer’s certificate :
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding-left: 20px; text-align: justify;">I the undersigned do hereby certify that the vehicle in question has been sold by me/my firm and the ownership
documents attached with the application for registration are true. The information/specifications pertaining to the vehicle
are correct and the vehicle complies with all the requirements of the registration.</td>
            </tr>
            <tr>
                <td>
                    <br />
                    <br />
                    Date :</td>
                <td style="width: 20%; text-align: center">
                    <br />
                    <br />
                    Signature of Dealer<br />
                    Seal</td>
            </tr>
            <tr>
                <td colspan="2">Encl : List of documents</td>
            </tr>
            <tr>
                <td colspan="2">44. Certificate by the Inspector of Motor Vehicles :
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding-left: 20px; text-align: justify;">Certificate that the particulars pertaining to the owner and the vehicle (Chassis No. <asp:Label ID="Page2ChassisNo" EnableViewState="false" runat="server"></asp:Label> Engine
No. <asp:Label ID="Page2EngineNo" EnableViewState="false" runat="server"></asp:Label>) given in the application match with the ownership documents attached to this
application. It is further certified that the vehicle complies with the registration requirements specified in the MV Act
and the Rules and/or Regulations made thereunder and the vehicle is not mechanically defective. The necessary
documents/papers are available as per list enclosed.</td>
            </tr>
            <tr>
                <td>
                    <br />
                    <br />
                    Date :</td>
                <td style="width: 30%; text-align: center">
                    <br />
                    <br />
                    Signature of Inspector of Motor Vehicles<br />
                    Official Seal</td>
            </tr>
            <tr>
                <td colspan="2">Encl : List of documents</td>
            </tr>
            <tr>
                <td colspan="2">45. Registration Status :</td>
            </tr>
            <tr>
                <td style="padding-left: 20px;">Registration allowed/not allowed</td>
                <td style="width: 25%; text-align: center">Signature of Registering Authority<br />
                    Seal</td>
            </tr>
            <tr>
                <td colspan="2">46. Fees and Tax Accounts :</td>
            </tr>
            <tr>
                <td style="padding-left: 20px;" colspan="2">Necessary fees and taxes amounting to taka............................
                    .................................................................has been paid to
PO/Bank...................................................................vide vouchers and receipts enclosed.</td>
            </tr>
            <tr>
                <td>Signature of owner
                    <br />
                    or his representative</td>
                <td style="width: 25%; text-align: center">Signature of dealing assistant</td>
            </tr>
            <tr>
                <td style="text-align: center; padding-top: 40px;" colspan="2">Counter signature by the registering authority.
                </td>
            </tr>
        </table>
    </asp:Panel>
    <hr />
    <asp:Panel ID="pnlPage3" runat="server" ClientIDMode="Static">
        <center><h4 style="margin:0; padding:0">OWNER’S PARTICULARS/SPECIMEN SIGNATURE</h4>
        </center>
        <br />
        <table class="table">
            <tr>
                <td style="width: 20%"></td>
                <td></td>
                <td></td>
                <td>3 Copies
Photograph</td>
            </tr>
            <tr>
                <td>1. NAME</td>
                <td style="width: 10px; height: 30px;">:</td>
                <td><asp:Label ID="page3OwnerName" EnableViewState="false" runat="server"></asp:Label></td>
                <td style="text-align: center; vertical-align: middle; width: 100px; height: 100px" rowspan="3">
                    <asp:TextBox ID="photo" runat="server" Height="100px" Width="110px" Text="Stamp Size Color Pic"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>2. FATHER/HUSBAND</td>
                <td style="width: 10px; height: 30px;">:</td>
                <td><asp:Label ID="page3Father" EnableViewState="false" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>3. ADDRESS</td>
                <td style="width: 10px; height: 30px;">:</td>
                <td><asp:Label ID="page3Address" EnableViewState="false" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>4. SEX</td>
                <td style="width: 10px; height: 30px;">:</td>
                <td><asp:Label ID="page3Gender" EnableViewState="false" runat="server"></asp:Label></td>
                <td></td>
            </tr>
            <tr>
                <td>5. PHONE NO</td>
                <td style="width: 10px; height: 30px;">:</td>
                <td><asp:Label ID="page3Phone" EnableViewState="false" runat="server"></asp:Label></td>
                <td></td>
            </tr>
            <tr>
                <td>6. NATIONALITY</td>
                <td style="width: 10px; height: 30px;">:</td>
                <td><asp:Label ID="page3Nationality" EnableViewState="false" runat="server"></asp:Label></td>
                <td></td>
            </tr>
            <tr>
                <td>7. DATE OF BIRTH</td>
                <td style="width: 10px; height: 30px;">:</td>
                <td><asp:Label ID="page3dob" EnableViewState="false" runat="server"></asp:Label></td>
                <td></td>
            </tr>
            <tr>
                <td>8. GUARDIAN’S NAME</td>
                <td style="width: 10px; height: 30px;">:</td>
                <td><asp:Label ID="page3Guardian" EnableViewState="false" runat="server"></asp:Label></td>
                <td></td>
            </tr>
            <tr>
                <td>9. CHASSIS NO</td>
                <td style="width: 10px; height: 30px;">:</td>
                <td><asp:Label ID="page3ChassisNo" EnableViewState="false" runat="server"></asp:Label></td>
                <td></td>
            </tr>
            <tr>
                <td>10. ENGINE NO</td>
                <td style="width: 10px; height: 30px;">:</td>
                <td><asp:Label ID="page3EngineNo" EnableViewState="false" runat="server"></asp:Label></td>
                <td></td>
            </tr>
            <tr>
                <td>11. YEAR OF MFG</td>
                <td style="width: 10px; height: 30px;">:</td>
                <td><asp:Label ID="Page3YearofManuf" EnableViewState="false" runat="server"></asp:Label></td>
                <td></td>
            </tr>
            <tr>
                <td>12. PREV. REGN. NO. (IF ANY)</td>
                <td style="width: 10px; height: 30px;">:</td>
                <td><asp:Label ID="page3RegNo" EnableViewState="false" runat="server"></asp:Label></td>
                <td></td>
            </tr>
            <tr>
                <td>13. P.O./BANK</td>
                <td style="width: 10px; height: 30px;">:</td>
                <td><asp:Label ID="Page3Bank" EnableViewState="false" runat="server"></asp:Label></td>
                <td></td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                    <br />
                    <br />
                    <b><u>SPECIMEN SIGNATURE</u></b>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <table style="width: 100%; margin-top: 40px;">
                        <tr>
                            <td style="border: 0; padding-top: 30px;">1.
                                <asp:TextBox ID="tbxSignatr" runat="server" Width="220px" Height="40px"></asp:TextBox></td>
                            <td style="border: 0; padding-top: 30px;">2.
                                <asp:TextBox ID="TextBox1" runat="server" Width="220px" Height="40px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="border: 0; padding-top: 50px;">3.
                                <asp:TextBox ID="TextBox2" runat="server" Width="220px" Height="40px"></asp:TextBox></td>
                            <td style="border: 0; padding-top: 50px;">4.
                                <asp:TextBox ID="TextBox3" runat="server" Width="220px" Height="40px"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlPage4" runat="server" ClientIDMode="Static">
        <div style="padding: 100px 10px 70px 10px">
            <h4>To</h4>
            <h4>The Assistant Director</h4>
            <h4>Bangladesh Road Transport Authority</h4>
            <h4>Dhaka.</h4>
            <i>Subject: <u>Sales intimation</u></i>
            <br />
            <br />
            Dear Sir,<br />
            <p>
                We would like to inform you that we sold an FOTON vehicle to
                <asp:Label ID="page4CustomerDetails" runat="server" EnableViewState="false" ></asp:Label>
                <br />
                <br />
                Details are as under: 
            </p>
            <table class="table">
                <tr>
                    <td>Class of Vehicle</td>
                    <td>: <asp:Label ID="page4VehiclType" runat="server" EnableViewState="false"></asp:Label></td>
                </tr>
                <tr>
                    <td>Type of Body</td>
                    <td>: <asp:Label ID="page4TypeofBody" runat="server" EnableViewState="false"></asp:Label></td>
                </tr>
                <tr>
                    <td>Manufactuere Name</td>
                    <td>: <asp:Label ID="page4Manufacturer" runat="server" EnableViewState="false"></asp:Label></td>
                </tr>
                <tr>
                    <td>Country of Origin</td>
                    <td>: <asp:Label ID="page4CountryofOrigin" runat="server" EnableViewState="false"></asp:Label></td>
                </tr>
                <tr>
                    <td>Model</td>
                    <td>: <asp:Label ID="Page4Model" runat="server" EnableViewState="false"></asp:Label></td>
                </tr>
                <tr>
                    <td>Manufacturing Year</td>
                    <td>: <asp:Label ID="page4ManufacturingYear" runat="server" EnableViewState="false"></asp:Label></td>
                </tr>
                <tr>
                    <td>Chassis No.</td>
                    <td>: <asp:Label ID="page4ChassisNo" runat="server" EnableViewState="false"></asp:Label></td>
                </tr>
                <tr>
                    <td>Engine No.</td>
                    <td>: <asp:Label ID="Page4EngineNo" runat="server" EnableViewState="false"></asp:Label></td>
                </tr>
                <tr>
                    <td>No. of Cylinder</td>
                    <td>: <asp:Label ID="Page4NoofCyl" runat="server" EnableViewState="false"></asp:Label></td>
                </tr>
                <tr>
                    <td>Fuel</td>
                    <td>: <asp:Label ID="page4FuelType" runat="server" EnableViewState="false"></asp:Label></td>
                </tr>
                <tr>
                    <td>Displacement</td>
                    <td>: <asp:Label ID="Page4Displacement" runat="server" EnableViewState="false"></asp:Label></td>
                </tr>
                <tr>
                    <td>Max. Power</td>
                    <td>: <asp:Label ID="Page4MaxPower" runat="server" EnableViewState="false"></asp:Label></td>
                </tr>
                <tr>
                    <td>Wheel Base</td>
                    <td>: <asp:Label ID="Page4WheelBase" runat="server" EnableViewState="false"></asp:Label></td>
                </tr>
                <tr>
                    <td>Tyre Size</td>
                    <td>: <asp:Label ID="Page4TyreSize" runat="server" EnableViewState="false"></asp:Label></td>
                </tr>
                <tr>
                    <td>Laden Weight</td>
                    <td>: <asp:Label ID="Page4LadenWeight" runat="server" EnableViewState="false"></asp:Label></td>
                </tr>
                <tr>
                    <td>Unladen Weight</td>
                    <td>: <asp:Label ID="Page4unladenWeight" runat="server" EnableViewState="false"></asp:Label></td>
                </tr>
                <tr>
                    <td>Seating Capacity</td>
                    <td>: <asp:Label ID="Page4SeatingCapacity" runat="server" EnableViewState="false"></asp:Label></td>
                </tr>
                <tr>
                    <td>Selling Price</td>
                    <td>: <asp:Label ID="page4SellingPrice" runat="server" EnableViewState="false"></asp:Label></td>
                </tr>
            </table>
            <br />
            <br />
            Thanks<br />
            <h4>(Signature of the Registered Dealer/Importwe)</h4>
        </div>
    </asp:Panel>
    <hr />
    <asp:Panel ID="pnlPage5" runat="server" ClientIDMode="Static">
        <div style="padding:100px 10px 70px 10px">
            <center>
                <h2>SALES RECEIPT</h2>
            </center>
            <h4>CI N:</h4>
            <h4>D.C. No:</h4>
            <p>This is to certify that we have sold 01(One) unit FOTON <asp:Label ID="Page5VehicleDetails" runat="server"></asp:Label> cabin & Chassis</p>

            <br />
            <br />
            Chassis No: <asp:Label ID="page5ChassisNo" runat="server" EnableViewState="false"></asp:Label>
            <br />
            <br />
            Engine No: <asp:Label ID="page5EngineNo" runat="server" EnableViewState="false"></asp:Label>
            <br />
            <br />
            To <asp:Label ID="page5CustomerDetails" runat="server" EnableViewState="false"></asp:Label> <br />
            @ <asp:Label ID="Page5SellingPrice" runat="server" EnableViewState="false"></asp:Label>  per chassis/complete built up.
            <br />
            <br />
            <br />
            Thanks
            <br />
            <br />
            <br />
            For RANGPUR METAL INDUSTRIES LIMITED
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
    <script type="text/javascript">
        function PrintPanel(pnl) {
            var panel = pnl;
            var windowHeight = window.innerHeight;
            var windowWidth = window.innerWidth;
            var printWindow = window.open('', '', 'letf=0,top=0,height=' + windowHeight + ',width=' + windowWidth + ',toolbar=0,scrollbars=1,status=0');
            printWindow.document.write("<html><head><title>BRTA Registration Form</title>");
            printWindow.document.write('<style>.text-left {text-align: left;} .text-right {text-align: right;} .text-center {text-align: center;}.text-justify {text-align: justify;}.text-nowrap {white-space: nowrap;}.text-lowercase {text-transform: lowercase;}.text-uppercase {text-transform: uppercase;}.text-capitalize {text-transform: capitalize;}.img-thumbnail {display: inline-block;max-width: 100%;height: auto;padding: 4px;line-height: 1.22857143;background-color: #fff;border: 0;font-size:11px;border-radius: 4px;-webkit-transition: all .2s ease-in-out;-o-transition: all .2s ease-in-out;transition: all .2s ease-in-out;}.panel-title {margin-top: 0;margin-bottom: 0;font-size: 11px;color: inherit;}.table {width: 100%;max-width: 100%;margin-bottom: 10px;} .table {border-collapse: collapse !important;}.table td, .table th {background-color: #fff !important;}.table th, .table td {border: 0 !important;}.table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {padding: 4px;line-height: 1.22857143; vertical-align: top;border-top: 0; text-align:left;font-size:11px; }.table > thead > tr > th {vertical-align: bottom;border-bottom: 0;}.table > caption + thead > tr:first-child > th, .table > colgroup + thead > tr:first-child > th, .table > thead:first-child > tr:first-child > th, .table > caption + thead > tr:first-child > td, .table > colgroup + thead > tr:first-child > td, .table > thead:first-child > tr:first-child > td {border-top: 0;}.table > tbody + tbody {border-top: 0;}.table .table {background-color: #fff;}.table {border: 0;}.table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {border: 0;}.table > thead > tr > th, .table > thead > tr > td {border-bottom-width: 0;}</style></head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.focus();
            printWindow.print();
            printWindow.close();
            //panel.style.display = 'none';
        }
    </script>
</asp:Content>

