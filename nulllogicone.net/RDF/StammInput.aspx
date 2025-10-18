<%@ Register TagPrefix="uc1" TagName="FussZeile" Src="../FussZeile.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../HeaderControl.ascx" %>

<%@ Page ValidateRequest="false" Language="c#" CodeBehind="StammInput.aspx.cs" AutoEventWireup="True" Inherits="nulllogicone.net.RDF.StammInput" %>

<!DOCTYPE HTML>
<html>
<head>
    <title>StammInput</title>
    <link href="../nulllogicone.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form2" method="post" runat="server">
        <p>
            <uc1:HeaderControl ID="HeaderControl1" runat="server"></uc1:HeaderControl>
        </p>
        <h1>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="default.aspx">IO.</asp:HyperLink>Stamm 
				Input</h1>
        <p>
            URL<br>
            <asp:TextBox ID="UrlTextBox" runat="server" Width="528px">http://nulllogic.de/rdf/stamm_oli.rdf</asp:TextBox>
            <asp:Button ID="GetRdfButton" runat="server" Text="get RDF" OnClick="GetRdfButton_Click"></asp:Button>
        </p>
        <p>
            PostIt RDF<br>
            <asp:TextBox ID="StammRdfTextBox" runat="server" Height="200px" Width="600px" TextMode="MultiLine"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="ParseButton" runat="server" Text="parse" OnClick="ParseButton_Click"></asp:Button>
        </p>
        <p>
            URIref:
				<br>
            <asp:HyperLink ID="URIHyperLink" runat="server"></asp:HyperLink>
        </p>
        <p>
            <asp:DataGrid ID="DataGrid1" runat="server"></asp:DataGrid>
        </p>
        <p>&nbsp;</p>
    </form>
    <p>&nbsp;</p>
    <p>
        <uc1:FussZeile ID="FussZeile1" runat="server"></uc1:FussZeile>
    </p>
</body>
</html>
