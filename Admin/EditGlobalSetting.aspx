<%-- 
Author: Payman Rowhani & Artin Rezaie 
Copyright (C) Artman Systems Inc. 2004-2009
--%>

<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="EditGlobalSetting.aspx.cs" Inherits="EditGlobalSetting" Title="Edit Global Setting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:MultiView ID="SettingMultiView" runat="server" ActiveViewIndex="0">
        <asp:View ID="SettingView" runat="server">
            <table style="width: 596px" border="10" bordercolor="white">
                <tr>
                    <th colspan="2" style="height: 68px">
                        <h2>
                            <span style="color: #cc00ff"></span>&nbsp;</h2>
                        <h2>
                            <span style="color: #cc00ff">Directory Setting</span></h2>
                    </th>
                </tr>
                <tr>
                    <td style="height: 21px; width: 202px;">
                        <strong><span style="font-size: 14pt; color: #009999">CSS Directory</span></strong></td>
                    <td style="height: 21px; width: 1px;">
                        <asp:TextBox ID="CssFilePath" runat="server" Width="362px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 202px">
                        <strong><span style="font-size: 14pt; color: #009999">JS Directory</span></strong></td>
                    <td style="width: 1px">
                        <asp:TextBox ID="JsFilePath" runat="server" Width="362px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 202px">
                        <strong><span style="font-size: 14pt; color: #009999">Image Directory</span></strong></td>
                    <td style="width: 1px">
                        <asp:TextBox ID="ImageFilePath" runat="server" Width="362px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 202px">
                        <strong><span style="font-size: 14pt; color: #009999">Static HTML Directory</span></strong></td>
                    <td style="width: 1px">
                        <asp:TextBox ID="StaticHtmlFilePath" runat="server" Width="362px"></asp:TextBox></td>
                </tr>
                <tr>
                    <th colspan="2">
                        <h2>
                            <span style="color: #cc00ff"></span>&nbsp;</h2>
                        <h2>
                            <span style="color: #cc00ff">Database Setting</span></h2>
                    </th>
                </tr>
                <tr>
                    <td style="width: 202px">
                        <strong><span style="font-size: 14pt; color: #009999">Data Source</span></strong></td>
                    <td style="width: 1px">
                        <asp:TextBox ID="Data_Source" runat="server" Width="362px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 202px">
                        <strong><span style="font-size: 14pt; color: #009999">Database File</span></strong></td>
                    <td style="width: 1px">
                        <asp:TextBox ID="AttachDbFilename" runat="server" Width="362px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 202px">
                        <strong><span style="font-size: 14pt; color: #009999">Database User ID</span></strong></td>
                    <td style="width: 1px">
                        <asp:TextBox ID="User_Id" runat="server" Width="362px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 202px">
                        <strong><span style="font-size: 14pt; color: #009999">Database Password</span></strong></td>
                    <td style="width: 1px">
                        <asp:TextBox ID="Password" runat="server" Width="362px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2" align="right">
                        <br />
                        <asp:Button ID="SubmitChanges" runat="server" Text="Submit Changes" Font-Bold="True"
                            OnClick="SubmitChanges_Click" /></td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="SuccessView" runat="server">
            <div align="center">
                <h1>
                    <span style="color: darkorange"></span>&nbsp;</h1>
                <h1>
                    &nbsp;</h1>
                <h1>
                    <span style="color: darkorange">Settings submitted successfully.</span></h1>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
