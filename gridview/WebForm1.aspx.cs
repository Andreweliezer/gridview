using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace gridview
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private void fillData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
            string str = string.Format("select * from employee");
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack==false)
            {
                fillData();
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int eid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
            string str = String.Format("delete from employee where eid={0}", eid);
            SqlCommand cmd = new SqlCommand(str, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            fillData();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)GridView1.FooterRow;
            TextBox t1 = (TextBox)row.FindControl("eid");
            TextBox t2 = (TextBox)row.FindControl("ename");
            TextBox t3 = (TextBox)row.FindControl("sal");
            TextBox t4 = (TextBox)row.FindControl("dno");
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
            string str = String.Format("insert into employee values({0},'{1}',{2},{3})", t1.Text, t2.Text, t3.Text, t4.Text);
            SqlCommand cmd = new SqlCommand(str, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            fillData();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            fillData();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            fillData();
        }
    }
}