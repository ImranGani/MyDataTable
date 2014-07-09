using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class MyDataTable : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            myDataTable();
        // First Line
        // Bug Fixed
        // Dev1 Testing Line
        // Test 1
        // Test 2
        // Demo 1
    }
    private void myDataTable()
    {
        Label lblCatch = null;
        DataTable dtGrid = null;
        try
        {
            TableList(ref dtGrid); // Get DataTable from DataBase.
            dynTableList(ref dtGrid); // Convert the DataTable value into the New DataTable.
            GridView1.DataSource = dtGrid; // Display the DataTable with the help of GridView.
            DataBind();
        }
        catch (Exception er)
        {
            lblCatch = new Label();
            lblCatch.Text = "<br/><span id=\"lbl1\" style=\"color:Red;\"/>Catch err : " + er;
            phOutputHere.Controls.Add(lblCatch);
        }
        finally
        {
            if (lblCatch != null)
                lblCatch.Dispose();
            if (dtGrid != null)
                dtGrid.Dispose();
        }
    }
    public DataTable TableList(ref DataTable dtGrid)
    {
        Label lblCatch = null;
        SqlConnection sqlconnObj = null;
        SqlCommand sqlcmdObj = null;
        SqlDataAdapter daAllDetails = null;
        string sqlConnString = "", sqlSelectQuery = "";
        try
        {
            sqlConnString = "server=CSNSYS068\\SQLEXPRESS;Database=ImranGani;User ID=sa;Password=cellar123;";
            sqlconnObj = new SqlConnection(sqlConnString);
            sqlSelectQuery = "select * from DetailsTable_1;";
            sqlcmdObj = new SqlCommand(sqlSelectQuery, sqlconnObj);
            daAllDetails = new SqlDataAdapter(sqlSelectQuery, sqlconnObj);
            dtGrid = new DataTable();
            daAllDetails.Fill(dtGrid);
            return dtGrid;
        }
        catch (Exception er)
        {
            lblCatch = new Label();
            lblCatch.Text = "<br/><span id=\"lbl1\" style=\"color:Red;\"/>Catch err : " + er;
            phOutputHere.Controls.Add(lblCatch);
            return dtGrid;
        }
        finally
        {
            if (sqlconnObj != null)
            {
                if (sqlconnObj.State.Equals(ConnectionState.Open))
                    sqlconnObj.Close();
                sqlconnObj.Dispose();
            }
            if (sqlcmdObj != null)
                sqlcmdObj.Dispose();
            sqlConnString = null;
            sqlSelectQuery = null;
            if (daAllDetails != null)
                daAllDetails.Dispose();
            if (lblCatch != null)
                lblCatch.Dispose();
        }
    }

    public DataTable dynTableList(ref DataTable dtGrid)
     {
        Label lblCatch = null;
        DataRow[] dynRow = null;
        try
        {
            dynRow = dtGrid.Select("[MarriageStatus]='Single'");
            dtGrid = dtGrid.Clone();
            foreach (DataRow dr in dynRow)
            {
                dtGrid.ImportRow(dr);
            }
            return dtGrid;
        }
        catch (Exception er)
        {
            lblCatch = new Label();
            lblCatch.Text = "<br/><span id=\"lbl1\" style=\"color:Red;\"/>Catch err : " + er;
            phOutputHere.Controls.Add(lblCatch);
            return dtGrid;
        }
        finally
        {
            if (lblCatch != null)
                lblCatch.Dispose();
        }
    }
}
