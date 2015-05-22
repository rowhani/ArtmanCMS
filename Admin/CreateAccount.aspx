<%-- 
Author: Payman Rowhani & Artin Rezaie 
Copyright (C) Artman Systems Inc. 2004-2009
--%>

<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CreateAccount.aspx.cs" Inherits="Admin_CreateAccount" Title="Create Account" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:MultiView ID="AccountMultiView" runat="server" ActiveViewIndex="0">
        <asp:View ID="SignUpView" runat="server">
            <div style="background-color: #ffffcc">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="False" Font-Size="16pt"
                    ForeColor="#0000C0" Text="Fill in the user information" Width="432px" Font-Underline="False"></asp:Label>
                <br />
                &nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="Password"
                    ControlToValidate="ConfirmPassword" ErrorMessage="Password and Confirm Password do not match"
                    Font-Bold="True" Width="338px"></asp:CompareValidator><br />
                &nbsp;<asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="User Name is already existing"
                    Font-Bold="True" OnServerValidate="CustomValidator1_ServerValidate"></asp:CustomValidator><br />
                &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                    ControlToValidate="Email" ErrorMessage="RegularExpressionValidator" Font-Bold="True"
                    Style="left: 578px; top: 387px" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    Width="170px">Incorrect email format</asp:RegularExpressionValidator><br />
                <table border="0" style="width: 778px">
                    <tr>
                        <td>
                            <asp:Label ID="user" runat="server" Font-Bold="True" Text="User Name" Width="92px"></asp:Label>
                            <span style="color: red">*</span></td>
                        <td style="width: 219px">
                            <asp:TextBox ID="UserName" runat="server" CausesValidation="true" MaxLength="25"
                                Width="210px"></asp:TextBox></td>
                        <td style="width: 401px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserName"
                                ErrorMessage="RequiredFieldValidator" Font-Bold="True" Width="223px">User Name should be specified</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="pass" runat="server" Font-Bold="True" Text="Password" Width="92px"></asp:Label>
                            <span style="color: red">*</span></td>
                        <td style="width: 219px">
                            <asp:TextBox ID="Password" runat="server" CausesValidation="true" MaxLength="25"
                                TextMode="Password" Width="210px"></asp:TextBox></td>
                        <td style="width: 401px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Password"
                                ErrorMessage="RequiredFieldValidator" Font-Bold="True" Width="200px">Password should be specified</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="conf" runat="server" Font-Bold="True" Text="Confirm  Password" Width="126px"></asp:Label>
                            <span style="color: red">*</span></td>
                        <td style="width: 219px">
                            <asp:TextBox ID="ConfirmPassword" runat="server" CausesValidation="true" MaxLength="25"
                                TextMode="Password" Width="210px"></asp:TextBox></td>
                        <td style="width: 401px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ConfirmPassword"
                                ErrorMessage="RequiredFieldValidator" Font-Bold="True" Width="256px">Confirm Password should be specified</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="first" runat="server" Font-Bold="True" Text="First Name" Width="122px"></asp:Label>
                            <span style="color: red">*</span></td>
                        <td style="width: 219px">
                            <asp:TextBox ID="FirstName" runat="server" CausesValidation="true" MaxLength="25"
                                Width="210px"></asp:TextBox></td>
                        <td style="width: 401px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="FirstName"
                                ErrorMessage="RequiredFieldValidator" Font-Bold="True" Width="243px">First Name should be specified</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="last" runat="server" Font-Bold="True" Text="Last Name" Width="122px"></asp:Label>
                            <span style="color: red">*</span></td>
                        <td style="width: 219px">
                            <asp:TextBox ID="LastName" runat="server" CausesValidation="true" MaxLength="25"
                                Width="210px"></asp:TextBox></td>
                        <td style="width: 401px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="LastName"
                                ErrorMessage="RequiredFieldValidator" Font-Bold="True" Width="227px">Last Name should be specified</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="eml" runat="server" Font-Bold="True" Text="Email" Width="122px"></asp:Label>
                            <span style="color: red">*</span></td>
                        <td style="width: 219px">
                            <asp:TextBox ID="Email" runat="server" CausesValidation="true" MaxLength="25" Width="210px"></asp:TextBox></td>
                        <td style="width: 401px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="Email"
                                ErrorMessage="RequiredFieldValidator" Font-Bold="True">Email address should be specified </asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="right" colspan="2">
                            <asp:Button ID="SignUp" runat="server" Font-Bold="True" Text="Create User" OnClick="SignUp_Click"
                                CommandName="SignUp" /></td>
                        <td style="width: 401px">
                        </td>
                    </tr>
                </table>
            </div>
        </asp:View>
        <asp:View ID="SuccessView" runat="server">
            <div align="center">
                <br />
                <strong><span style="font-size: 24pt; color: deepskyblue">
                    <br />
                    User Created Successfully.</span></strong>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
