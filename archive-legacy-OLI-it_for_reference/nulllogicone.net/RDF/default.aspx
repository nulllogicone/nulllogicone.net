<%@ Page Language="c#" ValidateRequest="false" CodeBehind="default.aspx.cs" AutoEventWireup="True" Inherits="nulllogicone.net.RDF._default" %>

<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FussZeile" Src="../FussZeile.ascx" %>
<!DOCTYPE HTML>
<html>
<head>
    <title>nulllogicone.net/RDF/</title>
    <link href="../nulllogicone.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <uc1:HeaderControl ID="HeaderControl1" runat="server"></uc1:HeaderControl>
        <h1>RDF Input / Output</h1>

        <p>
            Die Daten von OLI-it können über das rdf Format ausgetauscht werden.<br>
            Um die Graphen zu visualisieren oder das RDF zu validieren, empfiehlt sich der <a href="http://www.w3.org/RDF/Validator/">W3C RDF Validation Service</a>.
        </p>
        <h2>SAPCT&nbsp;- Input</h2>
        <p>
            RDF Dokumente können geparsed, validiert und in der Datenbank gespeichert 
				werden. Dabei können die Dateien auch von verteilten Servern stammen. Diese 
				Seiten sind mit Hilfe des&nbsp;<a href="http://www.schemaweb.info/parser/Parser.aspx">VicSoft 
					RDF Parser</a>&nbsp;realisiert worden.
        </p>
        <p>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="StammInput.aspx">Stamm-Input</asp:HyperLink>&nbsp;<br>
            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="AnglerInput.aspx">Angler-Input</asp:HyperLink><br>
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="PostItInput.aspx">PostIt-Input</asp:HyperLink>&nbsp;<br>
            <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="CodeInput.aspx">Code-Input</asp:HyperLink><br>
            <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="TopLabInput.aspx">TopLab-Input</asp:HyperLink>
        </p>
        <h2>SAPCT - Output</h2>
        <p>
            Zu gegebenen GUIDs kann man eine RDF Datei zum Download erstellen. Die 
				angegebenen GUIDs sind Beispiele und sollten durch die gewünschten ersetzt 
				werden.
        </p>
        <p>
            <table id="Table1" cellspacing="1" cellpadding="1" width="300" border="0">
                <tr>
                    <td>StammGuid</td>
                    <td>
                        <asp:TextBox ID="StammGuidTextBox" runat="server" Width="256px">b4111e0e-48d9-42c4-a6f6-ec4991264947</asp:TextBox></td>
                    <td>
                        <asp:ImageButton ID="StammRdfImageButton" runat="server" ImageUrl="~/images/rdf_klein.jpg" OnClick="StammRdfImageButton_Click"></asp:ImageButton></td>
                </tr>
                <tr>
                    <td>AnglerGuid</td>
                    <td>
                        <asp:TextBox ID="AnglerGuidTextBox" runat="server" Width="256px">be279cca-b934-45e6-85fd-96b1a6b1e6ed</asp:TextBox></td>
                    <td>
                        <asp:ImageButton ID="AnglerRdfImageButton" runat="server" ImageUrl="~/images/rdf_klein.jpg" OnClick="AnglerRdfImageButton_Click"></asp:ImageButton></td>
                </tr>
                <tr>
                    <td>PostItGuid
                    </td>
                    <td>
                        <asp:TextBox ID="PostItGuidTextBox" runat="server" Width="256px">176bae4e-05d7-4256-97a5-c98bfcbb2869</asp:TextBox></td>
                    <td>
                        <asp:ImageButton ID="PostItRdfImageButton" runat="server" ImageUrl="~/images/rdf_klein.jpg" OnClick="PostItRdfImageButton_Click"></asp:ImageButton></td>
                </tr>
                <tr>
                    <td>CodeGuid</td>
                    <td>
                        <asp:TextBox ID="CodeGuidTextBox" runat="server" Width="256px">ae2dd96b-378b-44b6-b33b-c73a0142e9b7</asp:TextBox></td>
                    <td>
                        <asp:ImageButton ID="CodeRdfImageButton" runat="server" ImageUrl="~/images/rdf_klein.jpg" OnClick="CodeRdfImageButton_Click"></asp:ImageButton></td>
                </tr>
                <tr>
                    <td>TopLabGuid</td>
                    <td>
                        <asp:TextBox ID="TopLabGuidTextBox" runat="server" Width="256px">ab9a4a38-1058-4400-8414-2c2df683c9d1</asp:TextBox></td>
                    <td>
                        <asp:ImageButton ID="TopLabRdfImageButton" runat="server" ImageUrl="~/images/rdf_klein.jpg" OnClick="TopLabRdfImageButton_Click"></asp:ImageButton></td>
                </tr>
            </table>
        </p>
        <p>
            Man kann die RDF Dateien auch über einen Link lokalisieren. Der Link setzt sich 
				zusammen aus dem Basisnamespace, einem Slash, dem Klassennamen, noch einem 
				Slash, der entsprechenden GUID und der Endung .rdf. Beispiele:
        </p>
        <pre><A href="https://nulllogicone.net/PostIt/176bae4e-05d7-4256-97a5-c98bfcbb2869.rdf">https://nulllogicone.net/PostIt/176bae4e-05d7-4256-97a5-c98bfcbb2869.rdf</A> 
<A href="https://nulllogicone.net/Angler/be279cca-b934-45e6-85fd-96b1a6b1e6ed.rdf">https://nulllogicone.net/Angler/be279cca-b934-45e6-85fd-96b1a6b1e6ed.rdf</A></pre>
        <p></p>
        <h2>Wortraum [NKBZ] Output</h2>
        <p>
            Der gesamte Wortraum in einer RDF Datei
				<asp:ImageButton ID="NKBZImagebutton" runat="server" ImageUrl="~/images/rdf_klein.jpg" OnClick="NKBZImagebutton_Click"></asp:ImageButton>&nbsp;(~ 
				1 MB)<br>
            Der Wortraum über einen URL: <a href="https://nulllogicone.net/NKBZ/nkbz.rdf">https://nulllogicone.net/NKBZ/nkbz.rdf</a>
        </p>
        <p>
            <uc1:FussZeile ID="FussZeile1" runat="server"></uc1:FussZeile>
        </p>
    </form>
</body>
</html>
