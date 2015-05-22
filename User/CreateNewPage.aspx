<%-- 
Author: Payman Rowhani & Artin Rezaie 
Copyright (C) Artman Systems Inc. 2004-2009
--%>

<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CreateNewPage.aspx.cs" Inherits="User_CreateNewPage" Title="Create New Page"
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:MultiView ID="PageMultiView" runat="server" ActiveViewIndex="0">
        <asp:View ID="PageView" runat="server">
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CmsDB %>"
                SelectCommand="SELECT * FROM [Template]"></asp:SqlDataSource>
            <div style="background-color: #cccccc">
                <table border="0">
                    <tr>
                        <td style="width: 934px">
                            <strong><span style="color: #ff33cc"><span style="color: #ffff00; font-size: 14pt;">
                                &nbsp;Page Name</span> <span style="color: #ff0066">*</span></span></strong></td>
                        <td style="width: 483px">
                            <asp:TextBox ID="PageName" runat="server" Width="430px"></asp:TextBox></td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Name cannot be empty"
                                Width="141px" ControlToValidate="PageName"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="width: 934px; height: 24px;">
                            <strong><span style="color: #ff33cc"><span style="color: #ffff00; font-size: 14pt;">
                                &nbsp;Template Name</span> </span></strong>
                        </td>
                        <td style="height: 24px">
                            <asp:DropDownList ID="TemplateName" runat="server" AutoPostBack="True" Width="218px"
                                DataSourceID="SqlDataSource1" DataTextField="TemplateName" DataValueField="TemplateName" OnSelectedIndexChanged="TemplateName_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="height: 24px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 934px" colspan="3" align="center">
                            <span style="font-size: 32pt; color: #66ff33"><strong>Page Content </strong></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 934px" colspan="3">
                            <br />
                            <asp:PlaceHolder ID="PageContentPlaceHolder" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right">
                            <asp:Button ID="CreatePage" runat="server" Font-Bold="True" OnClick="CreatePage_Click"
                                Text="Create Page" /></td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:View>
        <asp:View ID="SuccessView" runat="server">
            <div align="center">
                <br />
                <br />
                <strong><span style="font-size: 24pt; color: mediumturquoise">Page created successfully</span></strong></div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
