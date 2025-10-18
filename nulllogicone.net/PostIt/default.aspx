<%@ Page Language="c#" CodeBehind="default.aspx.cs" AutoEventWireup="True" Inherits="nulllogicone.net.PostIt._default" %>

<%@ Register TagPrefix="uc1" TagName="FussZeile" Src="../FussZeile.ascx" %>
<!DOCTYPE HTML>
<html>
<head>
    <title>nulllogicone.net/PostIt/?</title>
    <link href="../nulllogicone.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <h1>
            <p>
                <asp:Image ID="DateiImage" runat="server" Visible="False" DESIGNTIMEDRAGDROP="58" ImageAlign="Right"
                    Width="200px"></asp:Image>
            </p>
            PostIt</h1>
        <strong>
            <p>&nbsp;</p>
            <p>
                <table id="Table1" cellspacing="1" cellpadding="3" border="0">
                    <tr>
                        <td style="width: 142px" nowrap bgcolor="whitesmoke"><strong>URIref</strong>
                        </td>
                        <td>
                            <asp:HyperLink ID="UriHyperLink" runat="server" Font-Size="Smaller" Font-Bold="True"></asp:HyperLink></td>
                    </tr>
                    <tr>
                        <td style="width: 142px" nowrap bgcolor="#f5f5f5"><strong>Titel</strong></td>
                        <td>
                            <asp:Label ID="TitelLabel" runat="server" Font-Bold="True"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 142px" nowrap bgcolor="whitesmoke"><strong>Text</strong></td>
                        <td>
                            <asp:Label ID="PostItLabel" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 142px" nowrap bgcolor="whitesmoke">
                            <p>
                                <strong>Datum</strong>
                            </p>
                        </td>
                        <td>
                            <asp:Label ID="DatumLabel" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 142px" nowrap bgcolor="#f5f5f5"><strong>Link</strong></td>
                        <td>
                            <asp:HyperLink ID="UrlHyperLink" runat="server" Font-Size="Smaller"></asp:HyperLink></td>
                    </tr>
                    <tr>
                        <td style="width: 142px" nowrap bgcolor="#f5f5f5">Anzahl <strong>Urheber</strong></td>
                        <td>
                            <p align="left">
                                <asp:LinkButton ID="AnzUrheberLinkButton" runat="server" OnClick="AnzUrheberLinkButton_Click">Anzahl Urheber</asp:LinkButton></p>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 142px" nowrap bgcolor="#f5f5f5">Anzahl<strong> Code</strong></td>
                        <td>
                            <asp:LinkButton ID="AnzCodeLinkButton" runat="server" OnClick="AnzCodeLinkButton_Click">Anzahl Code</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td style="width: 142px" nowrap bgcolor="#f5f5f5">Anzahl <strong>Empfänger</strong></td>
                        <td>
                            <p align="left">
                                <asp:LinkButton ID="AnzEmpfLinkButton" runat="server" OnClick="AnzEmpfLinkButton_Click">Anzahl Empfänger</asp:LinkButton></p>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 142px" nowrap bgcolor="#f5f5f5">Anzahl <strong>TopLab</strong></td>
                        <td>
                            <p align="left">
                                <asp:LinkButton ID="AnzTopLabLinkButton" runat="server" OnClick="AnzTopLabLinkButton_Click">Anzahl TopLab</asp:LinkButton></p>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 142px" valign="top" nowrap bgcolor="#f5f5f5"><strong></strong></td>
                        <td>
                            <p>&nbsp;</p>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 142px" valign="top" nowrap bgcolor="#f5f5f5">
                            <asp:Label ID="DetailLabel" runat="server" Font-Bold="True"></asp:Label></td>
                        <td>
                            <p>
                                <asp:Repeater ID="CodeRepeater" runat="server">
                                    <ItemTemplate>
                                        <a style="font-size: smaller" href='<%# "https://nulllogicone.net/Code/?" + DataBinder.Eval(Container.DataItem, "CodeGuid") %>'>
                                            <%# "https://nulllogicone.net/Code/?" + DataBinder.Eval(Container.DataItem, "CodeGuid") %>
                                        </a>
                                        <br>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </p>
                            <asp:Repeater ID="StammRepeater" runat="server">
                                <ItemTemplate>
                                    <a style="font-size: smaller" href='<%# "https://nulllogicone.net/Stamm/?" + DataBinder.Eval(Container.DataItem, "StammGuid") %>'>
                                        <%# "https://nulllogicone.net/Stamm/?" + DataBinder.Eval(Container.DataItem, "StammGuid") %>
                                    </a>
                                    <br>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Repeater ID="AnglerRepeater" runat="server">
                                <ItemTemplate>
                                    <a style="font-size: smaller" href='<%# "https://nulllogicone.net/Angler/?" + DataBinder.Eval(Container.DataItem, "AnglerGuid") %>'>
                                        <%# "https://nulllogicone.net/Angler/?" + DataBinder.Eval(Container.DataItem, "AnglerGuid") %>
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
        </strong>
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
