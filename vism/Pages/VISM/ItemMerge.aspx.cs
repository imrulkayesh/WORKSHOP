using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_ItemMerge : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ItemMerge_OnClick(object sender, EventArgs e)
    {
        DatabaseManagerORCL dm=new DatabaseManagerORCL();
        string qry = @"SELECT count(IM.AMIM_TEXT) FROM CM.T_AMIM@scm_link IM
INNER JOIN
 (
SELECT DISTINCT WI.AMWI_AMIM FROM CM.T_AMWI@scm_link WI
WHERE WI.AMWI_AMSP IN('AMSPxxxxxx4005245800xxxxxxxxxx052458xxxx',
'AMSPxxxxxx4005245900xxxxxxxxxx052459xxxx',
'AMSPxxxxxx4005245200xxxxxxxxxx052452xxxx',
'AMSPxxxxxx4005326700xxxxxxxxxx053267xxxx',
'AMSPxxxxxx4005348400xxxxxxxxxx053484xxxx',
'AMSPxxxxxx4005385400xxxxxxxxxx053854xxxx',
'AMSPxxxxxx4005245100xxxxxxxxxx052451xxxx',
'AMSPxxxxxx4005556700xxxxxxxxxx055567xxxx',
'AMSPxxxxxx4005362100xxxxxxxxxx053621xxxx',
'AMSPxxxxxx5005150200xxxxxxxxxx051502xxxx',
'AMSPxxxxxx4005362200xxxxxxxxxx053622xxxx',
'AMSPxxxxxx4005274700xxxxxxxxxx052747xxxx',
'AMSPxxxxxx4005245000xxxxxxxxxx052450xxxx',
'AMSPxxxxxx4005245400xxxxxxxxxx052454xxxx',
'AMSPxxxxxx4005245500xxxxxxxxxx052455xxxx',
'AMSPxxxxxx4005245600xxxxxxxxxx052456xxxx',
'AMSPxxxxxx4005245700xxxxxxxxxx052457xxxx',
'AMSPxxxxxx4006412000xxxxxxxxxx064120xxxx',
'AMSPxxxxxx4006412100xxxxxxxxxx064121xxxx',
'AMSPxxxxxx4006412200xxxxxxxxxx064122xxxx',
'AMSPxxxxxx4006412300xxxxxxxxxx064123xxxx',
'AMSPxxxxxx4006412400xxxxxxxxxx064124xxxx',
'AMSPxxxxxx4006412500xxxxxxxxxx064125xxxx',
'AMSPxxxxxx4006412600xxxxxxxxxx064126xxxx',
'AMSPxxxxxx4006412700xxxxxxxxxx064127xxxx',
'AMSPxxxxxx4006412800xxxxxxxxxx064128xxxx',
'AMSPxxxxxx4005999700xxxxxxxxxx059997xxxx')
) II ON IM.OID = II.AMWI_AMIM
INNER JOIN CM.T_AMIG @scm_link GP
 ON IM.AMIM_AMIG = GP.OID
INNER JOIN CM.T_AMSU @scm_link UT ON UT.OID = IM.AMIM_AMSU
left join rvs_itemmaster vim on vim.ITEMCODE = IM.AMIM_TEXT
where vim.ITEMCODE is null";
        string a = dm.ExecuteScalar(qry);
        qry = @"MERGE INTO RVS_ITEMMASTER D
   USING (
   SELECT IM.AMIM_TEXT,IM.AMIM_NAME, GP.AMIG_TEXT,UT.AMSU_TEXT FROM CM.T_AMIM@SCM_LINK IM
INNER JOIN 
 (
SELECT DISTINCT WI.AMWI_AMIM FROM CM.T_AMWI@SCM_LINK WI
WHERE WI.AMWI_AMSP IN ('AMSPxxxxxx4005245800xxxxxxxxxx052458xxxx',
'AMSPxxxxxx4005245900xxxxxxxxxx052459xxxx',
'AMSPxxxxxx4005245200xxxxxxxxxx052452xxxx',
'AMSPxxxxxx4005326700xxxxxxxxxx053267xxxx',
'AMSPxxxxxx4005348400xxxxxxxxxx053484xxxx',
'AMSPxxxxxx4005385400xxxxxxxxxx053854xxxx',
'AMSPxxxxxx4005245100xxxxxxxxxx052451xxxx',
'AMSPxxxxxx4005556700xxxxxxxxxx055567xxxx',
'AMSPxxxxxx4005362100xxxxxxxxxx053621xxxx',
'AMSPxxxxxx5005150200xxxxxxxxxx051502xxxx',
'AMSPxxxxxx4005362200xxxxxxxxxx053622xxxx',
'AMSPxxxxxx4005274700xxxxxxxxxx052747xxxx',
'AMSPxxxxxx4005245000xxxxxxxxxx052450xxxx',
'AMSPxxxxxx4005245400xxxxxxxxxx052454xxxx',
'AMSPxxxxxx4005245500xxxxxxxxxx052455xxxx',
'AMSPxxxxxx4005245600xxxxxxxxxx052456xxxx',
'AMSPxxxxxx4005245700xxxxxxxxxx052457xxxx',
'AMSPxxxxxx4006412000xxxxxxxxxx064120xxxx',
'AMSPxxxxxx4006412100xxxxxxxxxx064121xxxx',
'AMSPxxxxxx4006412200xxxxxxxxxx064122xxxx',
'AMSPxxxxxx4006412300xxxxxxxxxx064123xxxx',
'AMSPxxxxxx4006412400xxxxxxxxxx064124xxxx',
'AMSPxxxxxx4006412500xxxxxxxxxx064125xxxx',
'AMSPxxxxxx4006412600xxxxxxxxxx064126xxxx',
'AMSPxxxxxx4006412700xxxxxxxxxx064127xxxx',
'AMSPxxxxxx4006412800xxxxxxxxxx064128xxxx',
'AMSPxxxxxx4005999700xxxxxxxxxx059997xxxx')
) II ON IM.OID=II.AMWI_AMIM
INNER JOIN CM.T_AMIG@SCM_LINK GP
ON IM.AMIM_AMIG=GP.OID
INNER JOIN CM.T_AMSU@SCM_LINK UT ON UT.OID=IM.AMIM_AMSU) S
   ON (D.ITEMCODE = S.AMIM_TEXT)
   WHEN NOT MATCHED THEN INSERT (D.ITEMCODE, D.ITEMNAME,D.ITEMCATEGORYCODE,D.SCMUNITCODE)
     VALUES (S.AMIM_TEXT,S.AMIM_NAME,S.AMIG_TEXT,S.AMSU_TEXT)";
        dm.ExecuteNonQuery(qry);
        MessageBox(a+" item opened successfully.");

    }
}