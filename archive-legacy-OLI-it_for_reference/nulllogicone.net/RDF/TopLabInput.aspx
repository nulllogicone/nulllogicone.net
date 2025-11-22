<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../HeaderControl.ascx" %>

<%@ Page Language="c#" CodeBehind="TopLabInput.aspx.cs" AutoEventWireup="True" Inherits="nulllogicone.net.RDF.TopLabInput" ValidateRequest="false" %>

<%@ Register TagPrefix="uc1" TagName="FussZeile" Src="../FussZeile.ascx" %>
<!DOCTYPE HTML>
<html>
<head>
    <title>TopLabInput</title>
    <link href="../nulllogicone.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <p>
            <uc1:HeaderControl ID="HeaderControl1" runat="server"></uc1:HeaderControl>
        </p>
        <h1>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="default.aspx">IO.</asp:HyperLink>TopLab 
				Input</h1>
        <p>
            URL<br>
            <asp:TextBox ID="UrlTextBox" runat="server" Width="528px">http://nulllogic.de/kathrinweb/toplab.rdf</asp:TextBox>
            <asp:Button ID="GetRdfButton" runat="server" Text="get RDF" OnClick="GetRdfButton_Click"></asp:Button>
        </p>
        <p>
            TopLab RDF<br>
            <asp:TextBox ID="TopLabRdfTextBox" runat="server" Width="600px" Height="200px" TextMode="MultiLine"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="ParseButton" runat="server" Text="parse" OnClick="ParseButton_Click"></asp:Button>
        </p>
        <p>
            <asp:DataGrid ID="DataGrid1" runat="server"></asp:DataGrid>
        </p>
        <p>
            <uc1:FussZeile ID="FussZeile1" runat="server"></uc1:FussZeile>
        </p>
    </form>
</body>
</html>
