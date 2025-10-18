<%@ Register TagPrefix="uc1" TagName="FussZeile" Src="../FussZeile.ascx" %>

<%@ Page Language="c#" CodeBehind="default.aspx.cs" AutoEventWireup="True" Inherits="nulllogicone.net.Zweig._default" %>

<!DOCTYPE HTML>
<html>
<head>
    <title>nulllogicone.net/Zweig/</title>
    <link href="../nulllogicone.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <h1>Zweig</h1>
        <table id="Table1" cellspacing="1" cellpadding="3" border="0">
            <tr>
                <td style="width: 119px" bgcolor="whitesmoke"><strong>URIref</strong>
                </td>
                <td>
                    <asp:HyperLink ID="UriHyperLink" runat="server" Font-Size="Smaller" Font-Bold="True"></asp:HyperLink></td>
            </tr>
            <tr>
                <td style="width: 119px" bgcolor="whitesmoke"><strong>Name</strong></td>
                <td>
                    <asp:Label ID="ZweigLabel" runat="server" Font-Size="120%" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 119px" bgcolor="#f5f5f5"><strong>Beschreibung</strong></td>
                <td>
                    <asp:Label ID="BeschreibungLabel" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 119px" bgcolor="#f5f5f5">
                    <p>gehört zu <strong>Baum</strong></p>
                </td>
                <td>
                    <asp:HyperLink ID="BaumHyperLink" runat="server" Font-Size="Smaller"></asp:HyperLink><strong></strong></td>
            </tr>
            <tr>
                <td style="width: 119px" bgcolor="#f5f5f5">weiter<strong> Netz</strong></td>
                <td>
                    <asp:HyperLink ID="WeiterNetzHyperLink" runat="server" Font-Size="Smaller"></asp:HyperLink><strong></strong></td>
            </tr>
            <tr>
                <td style="width: 119px" bgcolor="#f5f5f5">weiter<strong> Baum</strong></td>
                <td>
                    <asp:HyperLink ID="WeiterBaumHyperLink" runat="server" Font-Size="Smaller"></asp:HyperLink></td>
            </tr>
            <tr>
                <td style="width: 119px" bgcolor="whitesmoke">
                    <p>
                        <strong>Datum</strong>
                    </p>
                </td>
                <td>
                    <asp:Label ID="DatumLabel" runat="server"></asp:Label></td>
            </tr>
        </table>
    </form>
    <p>&nbsp;</p>
    <p>
        <uc1:FussZeile ID="FussZeile1" runat="server"></uc1:FussZeile>
    </p>
</body>
</html>
