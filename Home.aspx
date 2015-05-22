<%-- 
Author: Payman Rowhani & Artin Rezaie 
Copyright (C) Artman Systems Inc. 2004-2009
--%>

<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Home.aspx.cs" Inherits="Home" Title="Artman CMS Home" ValidateRequest="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:MultiView ID="MultiViewPanels" runat="server" ActiveViewIndex="0">
        <asp:View ID="LoginView" runat="server">
            <br />
            <div align="center" style="width: 792px; height: 124px">
                <br />
                <br />
                <br />
                <br />
                <strong><span style="color: #cc33cc">Please login to enable the site navigation.<br />
                    <asp:CustomValidator ID="AuthenticateValidator" runat="server" ErrorMessage="Invalid Login ID or Password. Try again."
                        Font-Bold="True" OnServerValidate="AuthenticateValidator_ServerValidate"></asp:CustomValidator><br />
                </span></strong>
                <table border="0" style="width: 360px">
                    <tr>
                        <td style="width: 102px">
                            <strong>User Name <span style="color: red">*</span></strong></td>
                        <td style="width: 312px">
                            <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserName"
                                ErrorMessage="Required" SetFocusOnError="True" Font-Bold="True"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            <strong>Password <span style="color: red">* </span></strong>
                        </td>
                        <td style="width: 312px">
                            <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="148px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Password"
                                ErrorMessage="Required" SetFocusOnError="True" Font-Bold="True"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right">
                            <asp:CheckBox ID="Remember" runat="server" Font-Bold="True" ForeColor="#FF0099" Text="Remember me on this computer" />&nbsp;
                            &nbsp; &nbsp; &nbsp;&nbsp;
                            <asp:Button ID="Login" runat="server" BackColor="#000099" Font-Bold="True" ForeColor="#FFFFFF"
                                Height="25px" OnClick="Login_Click" Text="Login" Width="83px" />
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
                <br />
                    <hr />
                    <br />
                <span style="color: lime"><span style="color: royalblue"><span style="color: mediumblue">
                                        Admin User Name:</span> </span><strong>admin</strong> &nbsp; &nbsp; <span style="color: mediumblue">
                        Admin Password: </span><strong>admin<br />
                            <br />
                        </strong><span style="color: #0000cd">User User Name:</span><span style="color: #4169e1">
                        </span><strong>user</strong> &nbsp; &nbsp; <span style="color: mediumblue">User Password:
                        </span><strong>user</strong></span></div>
            <br />            
        </asp:View>
        <asp:View ID="AdminView" runat="server">
            &nbsp;&nbsp;<br />
            &nbsp; From the menu on the left, you can perform tasks on Accounts, Settings and
            Pages.
            <br />
            <em>
                <br />
                <br />
                &nbsp; If you are not <b><i><font color="#33ff33">
                    <%= Request["AUTH_USER"]%>
                </font></i></b>, click </em>
            <asp:LinkButton ID="Signout" runat="server" OnClick="Signout_Click">here</asp:LinkButton><em>
                to sign as a different user. </em>
        </asp:View>
        <asp:View ID="UserView" runat="server">
            <br />
            <table align="center">
                <tr align="center">
                    <th style="height: 24px">
                        <span style="font-size: 14pt; color: darkorchid">List of New Templates</span></th>
                    <th style="height: 24px">
                        <span style="font-size: 14pt; color: darkorchid">List of New Pages</span></th>
                </tr>
                <tr align="center">
                    <td>
                        <asp:Panel ID="TemplatePanel" runat="server" Height="300px" Width="200px" HorizontalAlign="Center"
                            ScrollBars="Auto">
                        </asp:Panel>
                    </td>
                    <td>
                        <asp:Panel ID="PagePanel" runat="server" BorderWidth="0px" Height="300px" Width="200px"
                            HorizontalAlign="Center" ScrollBars="Auto">
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <br />
            <div align="center">
            
            <asp:HyperLink ID="HyperLink1" runat="server" Font-Size="14pt" NavigateUrl="~/User/ShowPage.aspx?PageID=4" Target="_blank">Sample Created Page</asp:HyperLink>
            </div>
            <br />
            <strong><span style="font-size: 14pt">&nbsp;&nbsp; </span></strong><em>If you are not
                <b><i><font color="#33ff33">
                    <%= Request["AUTH_USER"]%>
                </font></i></b>, click </em>
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="Signout_Click">here</asp:LinkButton><em>
                to sign as a different user. </em>
        </asp:View>
    </asp:MultiView>
</asp:Content>
