<%@ Register TagPrefix="uc1" TagName="AjaxWortraumControl" Src="AjaxWortraumControl.ascx" %>

<%@ Page Language="c#" CodeBehind="AjaxWortraumTest.aspx.cs" AutoEventWireup="True" Inherits="nulllogicone.net.Controls.AjaxWortraum.AjaxWortraumTest" EnableViewState="False" %>

<%@ Register TagPrefix="uc1" TagName="AjaxWortraumControlFlip" Src="AjaxWortraumControlFlip.ascx" %>
<!DOCTYPE HTML>
<html>
<head>
    <title>AjaxWortraumControlTest</title>
    <link href="../../nulllogicone.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <p>
            <table id="Table1" cellspacing="0" cellpadding="0" bgcolor="darkgray" border="1">
                <tr>
                    <td colspan="2" nowrap>
                        <asp:CheckBox ID="VgbCheckBox" runat="server" Text="Vorgabewerte"></asp:CheckBox>
                        <asp:CheckBox ID="EditCheckBox" runat="server" Text="Editierbar"></asp:CheckBox>
                        <asp:CheckBox ID="WerbefreiCheckBox" runat="server" Text="Werbefrei"></asp:CheckBox></td>
                    <td>
                        <asp:Button ID="ShowButton" runat="server" Text="show" OnClick="ShowButton_Click"></asp:Button></td>
                </tr>
                <tr>
                    <td>Code&nbsp;&nbsp;</td>
                    <td>
                        <asp:TextBox ID="CodeGuidTextBox" runat="server" Width="250px">ae2dd96b-378b-44b6-b33b-c73a0142e9b7</asp:TextBox></td>
                    <td>
                        <asp:Button ID="CodeButton" runat="server" Text="show" OnClick="CodeButton_Click"></asp:Button></td>
                </tr>
                <tr>
                    <td>Angler
                    </td>
                    <td>
                        <asp:TextBox ID="AnglerGuidTextBox" runat="server" Width="250px">be279cca-b934-45e6-85fd-96b1a6b1e6ed</asp:TextBox></td>
                    <td>
                        <asp:Button ID="AnglerButton" runat="server" Text="show" OnClick="AnglerButton_Click"></asp:Button></td>
                </tr>
            </table>
        </p>
        <p>
            <table id="Table2" cellspacing="1" cellpadding="1" width="100%" border="1">
                <tr>
                    <td valign="top" width="50%">
                        <p>
                            <uc1:AjaxWortraumControl ID="AjaxWortraumControl1" runat="server" Visible="False"></uc1:AjaxWortraumControl>
                        </p>
                    </td>
                    <td valign="top" width="50%">
                        <p align="right">
                            <uc1:AjaxWortraumControlFlip ID="AjaxWortraumControlFlip1" runat="server"></uc1:AjaxWortraumControlFlip>
                        </p>
                    </td>
                </tr>
            </table>
        </p>
        <asp:DataGrid ID="DataGrid1" runat="server"></asp:DataGrid>
    </form>
</body>
</html>
