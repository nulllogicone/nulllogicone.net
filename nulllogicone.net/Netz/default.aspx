<%@ Register TagPrefix="uc1" TagName="FussZeile" Src="../FussZeile.ascx" %>

<%@ Page Language="c#" CodeBehind="default.aspx.cs" AutoEventWireup="True" Inherits="nulllogicone.net.Netz._default" %>

<!DOCTYPE HTML>
<html>
<head>
    <title>nulllogicone.net/Netz/?</title>
    <link href="../nulllogicone.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <h1>
            <asp:Image ID="DateiImage" runat="server" Width="200px" ImageAlign="Right" DESIGNTIMEDRAGDROP="58"
                Visible="False"></asp:Image>
            Netz</h1>
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
                    <asp:Label ID="NetzLabel" runat="server" Font-Bold="True" Font-Size="120%"></asp:Label></td>
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
            <a href="https://nulllogicone.net/Netz/?76035f19-f4ae-4d58-a388-4bbc72c51cef"><font size="1">
					</font></a>
        </p>
        <p>
            <a href="https://nulllogicone.net/Netz/?76035f19-f4ae-4d58-a388-4bbc72c51cef"><font size="1">
					</font></a>
        </p>
        <h2>Knoten</h2>
        <table width="600">
            <asp:Repeater ID="KnotenRepeater" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><strong><%# DataBinder.Eval(Container.DataItem, "Knoten") %></strong></td>
                        <td><font size="1"><a href='<%# MakeKnotenURIRef(DataBinder.Eval(Container.DataItem, "KnotenGuid").ToString()) %>'><%# MakeKnotenURIRef(DataBinder.Eval(Container.DataItem, "KnotenGuid").ToString()) %></a></font></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <p>&nbsp;</p>
        <p>
        <p>
            <a href="https://nulllogicone.net/Netz/?76035f19-f4ae-4d58-a388-4bbc72c51cef"><font size="1">
					</font></a>
        </p>
        <uc1:FussZeile ID="FussZeile1" runat="server"></uc1:FussZeile>
        <p></p>
        <p>&nbsp;</p>
    </form>
</body>
</html>
