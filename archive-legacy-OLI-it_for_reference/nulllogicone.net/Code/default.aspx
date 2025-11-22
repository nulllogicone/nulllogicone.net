<%@ Register TagPrefix="uc1" TagName="FussZeile" Src="../FussZeile.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AjaxWortraumControl" Src="../Controls/AjaxWortraum/AjaxWortraumControl.ascx" %>

<%@ Page Language="c#" CodeBehind="default.aspx.cs" AutoEventWireup="True" Inherits="nulllogicone.net.Code._default" %>

<!DOCTYPE HTML>
<html>
<head>
    <title>nulllogicone.net/Code/</title>
    <link href="../nulllogicone.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <h1>Code
        </h1>
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
                    <td style="width: 119px" bgcolor="whitesmoke"><strong>Code</strong></td>
                    <td>
                        <asp:Label ID="CodeLabel" runat="server" Font-Bold="True"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 119px" bgcolor="#f5f5f5"><strong>Version</strong></td>
                    <td>
                        <asp:Label ID="VersionLabel" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 119px" bgcolor="#f5f5f5">von <strong>Stamm</strong></td>
                    <td>
                        <asp:HyperLink ID="StammHyperLink" runat="server" Font-Size="Smaller"></asp:HyperLink></td>
                </tr>
                <tr>
                    <td style="width: 119px" bgcolor="#f5f5f5">für <strong>PostIt</strong></td>
                    <td>
                        <asp:HyperLink ID="PostItHyperLink" runat="server" Font-Size="Smaller"></asp:HyperLink></td>
                </tr>
                <tr>
                    <td style="width: 119px" bgcolor="#f5f5f5">Anzahl <strong>Angler</strong></td>
                    <td>
                        <p align="left">
                            <asp:LinkButton ID="AnzAnglerLinkButton" runat="server" OnClick="AnzAnglerLinkButton_Click">Anzahl Empfänger</asp:LinkButton>
                        </p>
                    </td>
                </tr>
                <tr>
                    <td style="width: 119px" bgcolor="#f5f5f5"></td>
                    <td>
                        <asp:Repeater ID="AnglerRepeater" runat="server">
                            <ItemTemplate>
                                <a style="font-size: smaller" href='<%# "https://nulllogicone.net/Angler/?" + DataBinder.Eval(Container.DataItem, "AnglerGuid") %>'>
                                    <%# "https://nulllogicone.net/Angler/?" + DataBinder.Eval(Container.DataItem, "AnglerGuid") %>
                                </a>
                                <br>
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
            </table>
        </p>
        <p>
            <uc1:AjaxWortraumControl ID="AjaxWortraumControl1" runat="server"></uc1:AjaxWortraumControl>
        </p>
        <hr width="100%" size="1">
        <p>
            <asp:ImageButton ID="RdfImageButton" runat="server" ImageUrl="~/images/rdf_meta.jpg" OnClick="RdfImageButton_Click"></asp:ImageButton><br>
            <asp:HyperLink ID="RdfHyperLink" runat="server" Font-Size="Smaller"></asp:HyperLink>
        </p>
        <p>
            <uc1:FussZeile ID="FussZeile1" runat="server"></uc1:FussZeile>
        </p>
    </form>
</body>
</html>
