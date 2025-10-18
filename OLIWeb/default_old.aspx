<%@ Page Language="c#" CodeBehind="default_old.aspx.cs" AutoEventWireup="True" Inherits="OliWeb._default_old"
    Description="Homepage von OLI-it mit kurzer Einführung und weiterführenden Links" %>

<%@ Register TagPrefix="uc1" TagName="Kopf" Src="~/Controls/Koerper/Kopf.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AnonymIntro" Src="~/Controls/Floor/AnonymIntro.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AnonymMenu" Src="~/Controls/Floor/AnonymMenu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>OLI-it: Nachrichtenvermittlung im Semantischen Web</title>
    <meta content="OLI-it" name="Title" />
    <meta content="Frederic Luchting" name="Author" />
    <meta content="Frederic Luchting" name="Publisher" />
    <meta content="frederic@luchting.de" name="Copyright" />
    <meta content="Frage, Antwort, Kommunikation, Infrastruktur" name="Keywords" />
    <meta content="OLI-it, Nachrichtenvermittlung im Semantischen Web, Kommunikationsinfrastruktur, Nachrichtenvermittlungssystem, "
        name="Description" />
    <meta name="Robots" content="INDEX,FOLLOW" />
    <link href="OliWeb.css" type="text/css" rel="stylesheet" />
    <link rel="alternate" type="application/rss+xml" title="OLI-it Journal" href="http://xml.oli-it.com/RSS/Journal.aspx" />
</head>
<body>
    <form id="_default" method="post" runat="server">
    <uc1:Kopf ID="Kopf1" runat="server"></uc1:Kopf>
    <table id="DefaultTable" cellspacing="1" cellpadding="1" border="0">
        <tr>
            <td valign="top" width="150px">
                <uc1:AnonymMenu ID="AnonymMenu1" runat="server"></uc1:AnonymMenu>
            </td>
            <td valign="top">
                <uc1:AnonymIntro ID="AnonymIntro1" runat="server"></uc1:AnonymIntro>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
