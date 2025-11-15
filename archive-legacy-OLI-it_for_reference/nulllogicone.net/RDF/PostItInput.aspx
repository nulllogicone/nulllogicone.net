<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../HeaderControl.ascx" %>

<%@ Page ValidateRequest="false" Language="c#" CodeBehind="PostItInput.aspx.cs" AutoEventWireup="True" Inherits="nulllogicone.net.RDF.PostItInput" SmartNavigation="True" %>

<%@ Register TagPrefix="uc1" TagName="FussZeile" Src="../FussZeile.ascx" %>
<!DOCTYPE HTML>
<html>
<head>
    <title>PostItInput</title>
    <link href="../nulllogicone.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <p>
            <uc1:HeaderControl ID="HeaderControl1" runat="server"></uc1:HeaderControl>
        </p>
        <h1>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="default.aspx">IO.</asp:HyperLink>PostIt 
				Input</h1>
        <h2>RDF</h2>
        <p>
            URL<br>
            <asp:TextBox ID="UrlTextBox" runat="server" Width="528px">http://nulllogic.de/kathrinweb/postit.rdf</asp:TextBox><asp:Button ID="GetRdfButton" runat="server" Text="get RDF" OnClick="GetRdfButton_Click"></asp:Button>
        </p>
        <p>
            PostIt RDF<br>
            <asp:TextBox ID="PostItRdfTextBox" runat="server" Width="600px" Height="200px" TextMode="MultiLine"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="ParseButton" runat="server" Text="parse" OnClick="ParseButton_Click"></asp:Button></p>
        <h2>PostIt
        </h2>
        <p>
            <asp:DataGrid ID="PostItDataGrid" runat="server"></asp:DataGrid></p>
        <p>
            <strong>URIref:</strong>
            <asp:HyperLink ID="URIHyperLink" runat="server"></asp:HyperLink>
        </p>
        <h2>Urheber</h2>
        <p>
            <asp:DataGrid ID="PostItStammDataGrid" runat="server" BorderColor="#DEDFDE" BorderStyle="None"
                BorderWidth="1px" BackColor="White" CellPadding="4" GridLines="Vertical" ForeColor="Black"
                DataKeyField="Stamm">
                <FooterStyle BackColor="#CCCC99"></FooterStyle>
                <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#CE5D5A"></SelectedItemStyle>
                <AlternatingItemStyle BackColor="White"></AlternatingItemStyle>
                <ItemStyle BackColor="#F7F7DE"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#6B696B"></HeaderStyle>
                <Columns>
                    <asp:ButtonColumn Text="Ausw&#228;hlen" CommandName="Select"></asp:ButtonColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Right" ForeColor="Black" BackColor="#F7F7DE" Mode="NumericPages"></PagerStyle>
            </asp:DataGrid>
        </p>
        <h2>Anmelden</h2>
        <p>
            <asp:Label ID="StammLabel" runat="server" CssClass="stamm"></asp:Label>&nbsp;<br>
            Kennwort:
				<asp:TextBox ID="KennwortTextBox" runat="server"></asp:TextBox>&nbsp;
				<asp:Button ID="LoginButton" runat="server" Text="anmelden" OnClick="LoginButton_Click"></asp:Button>
        </p>
        <p>
            <asp:Label ID="StammServiceLabel" runat="server"></asp:Label>
        </p>
        <h2>Speichern</h2>
        <p>
            <asp:Button ID="Button1" runat="server" Text="Button"></asp:Button>
        </p>
        <p>&nbsp;</p>
        <uc1:FussZeile ID="FussZeile1" runat="server"></uc1:FussZeile>
    </form>
</body>
</html>
