<%@ Page Language="c#" CodeBehind="xml.aspx.cs" AutoEventWireup="false" Inherits="nulllogicone.net.NKBZ.xml" %>

<!DOCTYPE HTML>
<html>
<head>
    <title>nulllogicone.net/NKBZ/xml/</title>
    <link href="../nulllogicone.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <h1>XML</h1>
        <p>
            Der Wortraum setzt sich aus mehreren Tabellen zusammen. Sie können die Daten als 
				xml Text Dateien herunterladen und verwenden, kürzen und/oder ergänzen und 
				verwenden.
        </p>
        <p>
            NetzDataSet.xml
				<asp:LinkButton ID="LinkButton1" runat="server">NetzLinkButton</asp:LinkButton>
        </p>
        <p>NetzDataSet.xsd</p>
        <p>
            <asp:TextBox ID="TextBox1" runat="server" Width="528px" Height="320px" TextMode="MultiLine" Wrap="False"></asp:TextBox>
        </p>
    </form>
</body>
</html>
