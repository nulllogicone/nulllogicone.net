<%@ Page Language="c#" CodeBehind="default.aspx.cs" AutoEventWireup="True" Inherits="nulllogicone.net.Angler._default" %>

<%@ Register TagPrefix="uc1" TagName="AjaxWortraumControl" Src="../Controls/AjaxWortraum/AjaxWortraumControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AjaxWortraumControlFlip" Src="../Controls/AjaxWortraum/AjaxWortraumControlFlip.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FussZeile" Src="../FussZeile.ascx" %>
<!DOCTYPE HTML>
<html>
<head>
    <title>nulllogicone.net/Angler</title>
    <link href="../nulllogicone.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <h1></h1>
        <h1>Angler</h1>
        <p>
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
                        <asp:Label ID="AnglerLabel" runat="server" Font-Bold="True"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 119px" bgcolor="#f5f5f5"><strong>Beschreibung</strong></td>
                    <td>
                        <asp:Label ID="BeschreibungLabel" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 119px" bgcolor="#f5f5f5"><strong>Version</strong></td>
                    <td>
                        <asp:Label ID="VersionLabel" runat="server"></asp:Label></td>
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
                    <td style="width: 119px" bgcolor="#f5f5f5">gehört zu <strong>Stamm</strong></td>
                    <td>
                        <asp:HyperLink ID="StammUriRefHyperLink" runat="server" Font-Size="Smaller"></asp:HyperLink></td>
                </tr>
                <tr>
                    <td style="width: 119px" bgcolor="#f5f5f5">Anzahl <strong>Nachrichten</strong></td>
                    <td>
                        <p align="left">
                            <asp:LinkButton ID="AnzPLinkButton" runat="server" OnClick="AnzPLinkButton_Click">Anzahl Nachrichten</asp:LinkButton>
                        </p>
                    </td>
                </tr>
                <tr>
                    <td style="width: 119px" bgcolor="#f5f5f5" valign="top"><strong>
                        <asp:Label ID="DetailLabel" runat="server" Visible="False">alle PostIt</asp:Label></strong></td>
                    <td>
                        <asp:Repeater ID="PostItRepeater" runat="server">
                            <ItemTemplate>
                                <a style="font-size: smaller" href='<%# "https://nulllogicone.net/PostIt/?" + DataBinder.Eval(Container.DataItem, "PostItGuid") %>'>
                                    <%# "https://nulllogicone.net/PostIt/?" + DataBinder.Eval(Container.DataItem, "PostItGuid") %>
                                </a>
                                <br>
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
            </table>
        </p>
        <p>
            <uc1:AjaxWortraumControlFlip ID="AjaxWortraumControlFlip1" runat="server"></uc1:AjaxWortraumControlFlip>
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
            <br>
            <uc1:FussZeile ID="FussZeile1" runat="server"></uc1:FussZeile>
        </p>
    </form>
</body>
</html>
