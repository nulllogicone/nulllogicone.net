<%@ Page Language="c#" CodeBehind="default.aspx.cs" AutoEventWireup="True" Inherits="nulllogicone.net.Baum._default" %>

<%@ Register TagPrefix="uc1" TagName="FussZeile" Src="../FussZeile.ascx" %>
<!DOCTYPE HTML>
<html>
<head>
    <title>nulllogicone.net/Baum/</title>
    <link href="../nulllogicone.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <h1>
            <asp:Image ID="DateiImage" runat="server" Width="200px" ImageAlign="Right" DESIGNTIMEDRAGDROP="58"
                Visible="False"></asp:Image>Baum</h1>
        <table id="Table1" cellspacing="1" cellpadding="3" border="0">
            <tr>
                <td style="width: 119px" bgcolor="whitesmoke"><strong>URIref</strong>
                </td>
                <td>
                    <asp:HyperLink ID="UriHyperLink" runat="server" Font-Size="Smaller" Font-Bold="True"></asp:HyperLink></td>
            </tr>
            <tr>
                <td style="width: 119px" bgcolor="whitesmoke"><strong>Name</strong>
                </td>
                <td>
                    <asp:Label ID="BaumLabel" runat="server" Font-Size="120%" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 119px" bgcolor="#f5f5f5"><strong>Beschreibung</strong></td>
                <td>
                    <asp:Label ID="BeschreibungLabel" runat="server"></asp:Label></td>
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
        <p>
            <a href="https://nulllogicone.net/Baum/?6aeead08-b05f-4dea-b34b-7d52013a3c55"><font size="1">
					</font></a>
        </p>
        <h2>Zweig</h2>
        <table id="Table2" width="600">
            <asp:Repeater ID="ZweigRepeater" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><strong><%# DataBinder.Eval(Container.DataItem, "Zweig") %></strong></td>
                        <td><font size="1"><a href='<%# MakeZweigURIRef(DataBinder.Eval(Container.DataItem, "ZweigGuid").ToString()) %>'><%# MakeZweigURIRef(DataBinder.Eval(Container.DataItem, "ZweigGuid").ToString()) %></a></font></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <p>&nbsp;</p>
        <p>
            <uc1:FussZeile ID="FussZeile1" runat="server"></uc1:FussZeile>
        </p>
        <p>&nbsp;</p>
    </form>
</body>
</html>
