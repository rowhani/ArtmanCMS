///Author: Payman Rowhani & Artin Rezaie 
///Copyright (C) Artman Systems Inc. 2004-2009

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

public partial class Admin_PublishPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Form.DefaultButton = Publish.UniqueID;
    }

    protected void Publish_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in PageTable.Rows)
        {
            CheckBox ck = (CheckBox)(row.FindControl("SelectCheckBox"));
            if (ck.Checked)
            {
                string page = row.Cells[1].Text;
                Toolkit.Publish(page);
                ck.Checked = false;
                PageTable.DataBind();
            }
        }
    }

    protected void Unpublish_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in PageTable.Rows)
        {
            CheckBox ck = (CheckBox)(row.FindControl("SelectCheckBox"));
            if (ck.Checked)
            {
                string page = row.Cells[1].Text;
                Toolkit.Unpublish(page);
                ck.Checked = false;
                PageTable.DataBind();
            }
        }
    }

    protected void PageTable_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chk = (CheckBox)(e.Row.FindControl("SelectCheckBox"));
            Page.ClientScript.RegisterStartupScript(this.GetType(),
                "MyScript",
                " function HighlightSelected(colorcheckboxselected, RowState) { if (colorcheckboxselected.checked) colorcheckboxselected.parentElement.parentElement.style.backgroundColor='#00FF99'; else { if (RowState=='0') colorcheckboxselected.parentElement.parentElement.style.backgroundColor='white'; else colorcheckboxselected.parentElement.parentElement.style.backgroundColor='#D6E3F7'; } }", true);
            chk.Attributes.Add("onclick",
                "HighlightSelected(this,'" + Convert.ToString(e.Row.RowState) + "' );");
        }
    }
}
