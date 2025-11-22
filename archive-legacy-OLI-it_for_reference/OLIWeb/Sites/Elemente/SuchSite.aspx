<%@ Page Language="c#" CodeBehind="SuchSite.aspx.cs" MasterPageFile="~/Site.Master"
    AutoEventWireup="True" Inherits="OliWeb.Sites.Elemente.SuchList" %>

<%@ Register TagPrefix="uc1" TagName="PostItListsControl" Src="../../Controls/Floor/Suche/PostItListsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammListsControl" Src="../../Controls/Floor/Suche/StammListsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopLabListsControl" Src="../../Controls/Floor/Suche/TopLabListsControl.ascx" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>SuchList</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="SuchSiteRumpfTable" cellspacing="1" cellpadding="1" width="100%" border="1">
        <tr>
            <td align="left" colspan="2">
                <p>
                    Suche nach der Zeichenkette:
                    <asp:Label ID="SuchLabel" runat="server" Font-Bold="True">Label</asp:Label></p>
                <p>
                    <font size="2">Es wird in den Stämmen, den Nachrichten und Antworten mit ihren Titeln
                                    nach der enthaltenen Zeichenfolge gesucht. </font>
                </p>
                <p>
                    <font size="2">Das ist keine Wortverknüpfte Suche wie bei Google sondern prüft nur auf
                                    ein striktes Enthaltensein in den Tabellen. Man muß mindestenst drei Buchstaben
                                    angeben.</font>
                </p>
                <p>
                    <font size="2">Bei einer
                                    <asp:LinkButton ID="GoogleLinkButton" runat="server" Font-Bold="True" OnClick="GoogleLinkButton_Click">Googlesuche</asp:LinkButton>&nbsp;kann
                                    man mehrere Begriffe verbinden.</font>
                </p>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <uc1:StammListsControl ID="StammListsControl1" runat="server"></uc1:StammListsControl>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left" width="50%">
                <uc1:PostItListsControl ID="PostItListsControl1" runat="server"></uc1:PostItListsControl>
            </td>
            <td valign="top" align="right" width="50%">
                <uc1:TopLabListsControl ID="TopLabListsControl1" runat="server"></uc1:TopLabListsControl>
            </td>
        </tr>
    </table>
</asp:Content>
