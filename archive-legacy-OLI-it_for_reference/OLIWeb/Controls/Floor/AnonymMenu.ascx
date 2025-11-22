<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="AnonymMenu.ascx.cs"
    Inherits="OliWeb.Controls.Floor.AnonymMenu" %>
<%--Register new Stamm --%>
<div id="DivNewStamm" runat="server">
    <asp:HyperLink ID="NeuAnmeldenHyperLink" ToolTip="erstellen Sie einen neuen Stamm"
        NavigateUrl="../../Sites/Elemente/NeuAnmelden.aspx" runat="server" Font-Bold="True"
        meta:resourcekey="NeuAnmeldenHyperLinkResource1" Text="neu anmelden"></asp:HyperLink><br />
    <asp:Literal ID="LiteralFree" Text="kostenlos und unverbindlich" runat="server" meta:resourcekey="LiteralFreeResource1" /></div>
<%--Journal--%>
<div id="DivJournal" runat="server">
    <asp:HyperLink ID="JournalHyperLink" ToolTip="die neuesten Stämme, Filterprofile, Nachrichten und Antworten in der Datenbank"
        NavigateUrl="~/Sites/Elemente/Journal.aspx" runat="server" Font-Bold="True" meta:resourcekey="JournalHyperLinkResource1"
        Text="Journal"></asp:HyperLink><br />
    <asp:Literal ID="LiteralNews" Text="die neuesten Einträge" runat="server" meta:resourcekey="LiteralNewsResource1" /></div>
<%--Chart--%>
<div id="DivChart" runat="server">
    <asp:HyperLink ID="ChartHyperLink" ToolTip="Die teuersten, reichsten, besten und schlechtesten Nachrichten und Stämme und Antworten"
        NavigateUrl="~/Sites/Elemente/ChartPage.aspx" runat="server" Font-Bold="True"
        meta:resourcekey="ChartHyperLinkResource1" Text="Chart"></asp:HyperLink><br />
    <asp:Literal ID="LiteralStatistic" Text="statistische Übersicht" runat="server" meta:resourcekey="LiteralStatisticResource1" /></div>
<%--search --%>
<div id="DivSearch" runat="server">
    <asp:Literal ID="LiteralSearch" Text="<strong>Suchen   </strong> <br />
                        nach Urhebern, Nachrichten und Antworten" runat="server" meta:resourcekey="LiteralSearchResource1" />
    <asp:TextBox ID="SuchTextBox" Style="width: 100%;" ToolTip="Bitte eine gewünschte, enthaltene Zeichenfolge eingeben"
        runat="server" meta:resourcekey="SuchTextBoxResource1"></asp:TextBox>
    <asp:Button ID="SuchButton" ToolTip="durchsucht die angegebenen Zeichen in den Datenbanktabellen von Urheber, Nachricht und Antworten"
        runat="server" Text="los" OnClick="SuchButton_Click" meta:resourcekey="SuchButtonResource1">
    </asp:Button>
    <asp:LinkButton ID="GoogleLinkButton" runat="server" Font-Bold="True" Font-Size="X-Small"
        OnClick="GoogleLinkButton_Click" meta:resourcekey="GoogleLinkButtonResource1"
        Text=" > mit google"></asp:LinkButton>
</div>
<%-- RSS --%>
<div>
    <a href="<% = RssLink %>">
        <asp:Image ID="Image2" runat="server" ImageUrl="~/images/xml.gif" meta:resourcekey="Image2Resource1">
        </asp:Image>
        RSS</a>
</div>
<%-- more links --%>
<div>
    <asp:HyperLink ID="HyperLinkLinks" runat="server" NavigateUrl="~/Sites/Elemente/Links.aspx"
        meta:resourcekey="HyperLinkLinks">Links</asp:HyperLink>
</div>
<%-- TOC --%>
<div id="DivImprint" runat="server">
    <asp:HyperLink ID="NutzungHyperLink" NavigateUrl="~/Nutzungsbedingungen.aspx" runat="server"
        meta:resourcekey="NutzungHyperLinkResource1" Text="Nutzungsbedingung"></asp:HyperLink>
</div>
<%-- Imprint  --%>
<div>
    <asp:HyperLink ID="ImpressumHyperLink" NavigateUrl="~/Impressum.aspx" runat="server"
        meta:resourcekey="ImpressumHyperLinkResource1" Text="Impressum"></asp:HyperLink></div>

