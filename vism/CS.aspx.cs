using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


public partial class CS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.BindGrid();
        }
    }

    private void BindGrid()
    {
        DataTable dt = new DataTable();
        string query = "SELECT Name, Country FROM Customers";
        string constr = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                }
            }
        }

        if (dt.Rows.Count == 0)
        {
            //If no records then add a dummy row.
            dt.Rows.Add();
        }

        gvCustomers.DataSource = dt;
        gvCustomers.DataBind();
    }

    protected void Submit(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["name"]) && !string.IsNullOrEmpty(Request.Form["country"]))
        {
            //Fetch the Hidden Field values from the Request.Form collection.
            string[] names = Request.Form["name"].Split(',');
            string[] countries = Request.Form["country"].Split(',');

            //Loop through the values and insert into database table.
            for (int i = 0; i < names.Length; i++)
            {
                string constr = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Customers VALUES(@Name, @Country)"))
                    {
                        cmd.Parameters.AddWithValue("@Name", names[i]);
                        cmd.Parameters.AddWithValue("@Country", countries[i]);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }

            //Refresh the page to load GridView with records from database table.
            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }
}