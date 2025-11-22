<%@ Register TagPrefix="uc1" TagName="FussZeile" Src="../FussZeile.ascx" %>

<%@ Page Language="c#" CodeBehind="default.aspx.cs" AutoEventWireup="True" Inherits="nulllogicone.net.TopLab._default" %>

<!DOCTYPE HTML>
<html>
<head>
    <title>nulllogicone.net/TopLab/</title>    
    <link href="../nulllogicone.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <h1>
            <p>
                <asp:Image ID="DateiImage" runat="server" Width="200px" ImageAlign="Right" DESIGNTIMEDRAGDROP="58"
                    Visible="False"></asp:Image>
            </p>
            TopLab</h1>
        <p>&nbsp;</p>
        <p>
            <table id="Table1" cellspacing="1" cellpadding="3" border="0">
                <tr>
                    <td style="width: 119px" bgcolor="whitesmoke"><strong>URIref</strong>
                    </td>
                    <td>
                        <asp:HyperLink ID="UriHyperLink" runat="server" Font-Size="Smaller" Font-Bold="True"></asp:HyperLink></td>
                </tr>
                <tr>
                    <td style="width: 119px" bgcolor="#f5f5f5"><strong>Titel</strong></td>
                    <td>
                        <asp:Label ID="TitelLabel" runat="server" Font-Bold="True"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 119px" bgcolor="whitesmoke"><strong>Text</strong></td>
                    <td>
                        <asp:Label ID="TopLabLabel" runat="server"></asp:Label></td>
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
                <tr>
                    <td style="width: 119px" bgcolor="#f5f5f5"><strong>Link</strong></td>
                    <td>
                        <asp:HyperLink ID="UrlHyperLink" runat="server"></asp:HyperLink></td>
                </tr>
                <tr>
                    <td style="width: 119px" bgcolor="#f5f5f5">von <strong>Stamm</strong></td>
                    <td>
                        <asp:HyperLink ID="StammHyperLink" runat="server" Font-Size="Smaller"></asp:HyperLink></td>
                </tr>
                <tr>
                    <td style="width: 119px" bgcolor="#f5f5f5">auf <strong>PostIt</strong></td>
                    <td>
                        <asp:HyperLink ID="PostItHyperLink" runat="server" Font-Size="Smaller"></asp:HyperLink></td>
                </tr>
            </table>
        </p>
        <hr width="100%" size="1">
        <p>
            <table id="LinkTable" cellspacing="1" cellpadding="1" border="0">
                <tr>
                    <td>
                        <asp:ImageButton ID="RdfImageButton" runat="server" ImageUrl="~/images/rdf_meta.jpg" ToolTip="RDF Metadaten für diese Resource anzeigen" OnClick="RdfImageButton_Click"></asp:ImageButton></td>
                    <td>
                        <asp:HyperLink ID="RdfHyperLink" runat="server" Font-Size="Smaller"></asp:HyperLink></td>
                </tr>
                <tr>
                    <td>
                        <asp:HyperLink ID="OLIitHyperLink" runat="server" ToolTip="diese Antwort bei OLI-it.com anzeigen"
                            ImageUrl="~/images/oli-it_128.jpg"></asp:HyperLink></td>
                    <td>
                        <p>
                            <asp:HyperLink ID="OLIHyperLink" runat="server" Font-Size="Smaller"></asp:HyperLink>
                        </p>
                    </td>
                </tr>
            </table>
        </p>
        <p>
            <uc1:FussZeile ID="FussZeile1" runat="server"></uc1:FussZeile>
        </p>
    </form>
</body>
</html>
