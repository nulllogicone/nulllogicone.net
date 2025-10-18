<%@ Page Language="c#" CodeBehind="default.aspx.cs" AutoEventWireup="false" Inherits="nulllogicone.net.client._default" %>

<%@ Register TagPrefix="uc1" TagName="FussZeile" Src="../FussZeile.ascx" %>
<!DOCTYPE HTML>
<html>
<head>
    <title>default</title>
    <link href="../nulllogicone.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <h2>Client</h2>
        <p>für die nulllogicone.net Netztechniker und OLI-it Anwender</p>
        <p>OliWin</p>
        <p>OliXWin</p>
        <p>&nbsp;</p>
        <p>
            <uc1:FussZeile ID="FussZeile1" runat="server"></uc1:FussZeile>
        </p>
    </form>
</body>
</html>
