<%@ Register TagPrefix="uc1" TagName="FussZeile" Src="../FussZeile.ascx" %>

<%@ Page Language="c#" CodeBehind="default.aspx.cs" AutoEventWireup="True" Inherits="nulllogicone.net.Stamm._default" %>

<!DOCTYPE HTML>
<html>
<head>
    <title>nulllogicone.net/Stamm/</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../nulllogicone.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <h1>
            <p>
                <asp:Image ID="DateiImage" runat="server" Width="200px" ImageAlign="Right" DESIGNTIMEDRAGDROP="58"
                    Visible="False"></asp:Image>
            </p>
            Stamm</h1>
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
                    <td style="width: 119px" bgcolor="whitesmoke"><strong>Name</strong>
                    </td>
                    <td>
                        <asp:Label ID="StammLabel" runat="server" Font-Bold="True"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 119px" bgcolor="#f5f5f5">Beschreibung</td>
                    <td></td>
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
                    <td style="width: 119px" bgcolor="#f5f5f5"></td>
                    <td></td>
                </tr>
                <tr>
                    <td style="width: 119px" bgcolor="#f5f5f5">Anzahl <strong>Angler</strong>
                    </td>
                    <td align="right">
                        <p align="left">
                            <asp:LinkButton ID="AnzAnglerLinkButton" runat="server" OnClick="AnzAnglerLinkButton_Click">Anzahl Angler</asp:LinkButton>
                        </p>
                    </td>
                </tr>
                <tr>
                    <td style="width: 119px" bgcolor="#f5f5f5">Anzahl <strong>PostIt</strong></td>
                    <td>
                        <p align="left">
                            <asp:LinkButton ID="AnzPostItLinkButton" runat="server" OnClick="AnzPostItLinkButton_Click">Anzahl PostIt</asp:LinkButton>
                        </p>
                    </td>
                </tr>
                <tr>
                    <td style="width: 119px" bgcolor="#f5f5f5">Anzahl <strong>TopLab</strong></td>
                    <td>
                        <p align="left">
                            <asp:LinkButton ID="AnzTopLabLinkButton" runat="server" OnClick="AnzTopLabLinkButton_Click">Anzahl TopLab</asp:LinkButton>
                        </p>
                    </td>
                </tr>
                <tr>
                    <td style="width: 119px" bgcolor="#f5f5f5" valign="top"><strong>
                        <asp:Label ID="DetailLabel" runat="server"></asp:Label></strong></td>
                    <td>
                        <asp:Repeater ID="AnglerRepeater" runat="server">
                            <ItemTemplate>
                                <a style="font-size: smaller" href='<%# "https://nulllogicone.net/Angler/?" + DataBinder.Eval(Container.DataItem, "AnglerGuid") %>'>
                                    <%# "https://nulllogicone.net/Angler/?" + DataBinder.Eval(Container.DataItem, "AnglerGuid") %>
                                </a>
                                <br>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Repeater ID="PostItRepeater" runat="server">
                            <ItemTemplate>
                                <a style="font-size: smaller" href='<%# "https://nulllogicone.net/PostIt/?" + DataBinder.Eval(Container.DataItem, "PostItGuid") %>'>
                                    <%# "https://nulllogicone.net/PostIt/?" + DataBinder.Eval(Container.DataItem, "PostItGuid") %>
                                </a>
                                <br>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Repeater ID="TopLabRepeater" runat="server">
                            <ItemTemplate>
                                <a style="font-size: smaller" href='<%# "https://nulllogicone.net/TopLab/?" + DataBinder.Eval(Container.DataItem, "TopLabGuid") %>'>
                                    <%# "https://nulllogicone.net/TopLab/?" + DataBinder.Eval(Container.DataItem, "TopLabGuid") %>
                                </a>
                                <br>
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
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
            <br>
            <uc1:FussZeile ID="FussZeile1" runat="server"></uc1:FussZeile>
        </p>
        <p>&nbsp;</p>
    </form>
</body>
</html>
