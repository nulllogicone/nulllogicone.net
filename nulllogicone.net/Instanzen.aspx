<%@ Register TagPrefix="uc1" TagName="FussZeile" Src="FussZeile.ascx" %>

<%@ Page Language="c#" CodeBehind="Instanzen.aspx.cs" AutoEventWireup="True" Inherits="nulllogicone.net.Instanzen" %>

<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="HeaderControl.ascx" %>
<!DOCTYPE HTML>
<html>
<head>
    <title>nulllogicone.net/Instanzen#</title>
    <link href="nulllogicone.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <uc1:HeaderControl ID="HeaderControl1" runat="server"></uc1:HeaderControl>
        <h1>Instanzen</h1>
        <p>
            Beispiele für <strong>nulllogicone.net</strong> Instanzen.
        </p>
        <p>Die Links sind völlig zufällig ermittelt und absolut zusammenhangsfrei:</p>
        <p>
            <table id="Table1" cellspacing="1" cellpadding="1" width="100%" border="1">
                <tr>
                    <th bgcolor="#f1f1f1">Klasse</th>
                    <th>Beschreibung</th>
                    <th>Zufalls-Link</th>
                </tr>
                <tr>
                    <td bgcolor="#f1f1f1">Stamm</td>
                    <td>Urheber von Nachrichten und Antworten.</td>
                    <td>
                        <asp:HyperLink ID="StammHyperLink" runat="server" Font-Size="Smaller"></asp:HyperLink></td>
                </tr>
                <tr>
                    <td bgcolor="#f1f1f1">Angler</td>
                    <td>Filterprofil</td>
                    <td>
                        <asp:HyperLink ID="AnglerHyperLink" runat="server" Font-Size="Smaller"></asp:HyperLink></td>
                </tr>
                <tr>
                    <td bgcolor="#f1f1f1">PostIt</td>
                    <td>Nachricht</td>
                    <td>
                        <asp:HyperLink ID="PostItHyperLink" runat="server" Font-Size="Smaller"></asp:HyperLink></td>
                </tr>
                <tr>
                    <td bgcolor="#f1f1f1">Code</td>
                    <td>Markierung</td>
                    <td>
                        <asp:HyperLink ID="CodeHyperLink" runat="server" Font-Size="Smaller"></asp:HyperLink></td>
                </tr>
                <tr>
                    <td bgcolor="#f1f1f1">TopLab</td>
                    <td>Antwort</td>
                    <td>
                        <asp:HyperLink ID="TopLabHyperLink" runat="server" Font-Size="Smaller"></asp:HyperLink></td>
                </tr>
                <tr>
                    <td bgcolor="#f1f1f1">-</td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td bgcolor="#f1f1f1">Netz</td>
                    <td colspan="2"><strong>Root:</strong> <a href="https://nulllogicone.net/Netz/?76035f19-f4ae-4d58-a388-4bbc72c51cef">
                        <font size="1">https://nulllogicone.net/Netz/?76035f19-f4ae-4d58-a388-4bbc72c51cef</font></a></td>
                </tr>
                <tr>
                    <td bgcolor="#f1f1f1">Knoten</td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td bgcolor="#f1f1f1">Baum</td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td bgcolor="#f1f1f1">Zweig</td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </p>
        <p>&nbsp;</p>
        <p>
            <uc1:FussZeile ID="FussZeile1" runat="server"></uc1:FussZeile>
        </p>
    </form>
</body>
</html>
