<%@ Page Language="c#" CodeBehind="PostItTopLabSite.aspx.cs" MasterPageFile="~/Site.Master"
    AutoEventWireup="True" Inherits="OliWeb.Sites.PostItTopLabSite" Description="Antwoten auf diese Nachricht" %>

<%@ Register TagPrefix="uc1" TagName="PostItTopLabGrid" Src="~/Controls/Koerper/ViewGrids/PostItTopLabGrid.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PostItKoerper" Src="~/Controls/Koerper/PostItKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="~/Controls/Koerper/StammKoerper.ascx" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>OLI-it: Antworten auf Nachricht:
        <% = MyTitle %>
    </title>
    <meta name="keywords" content="toplab, feedback, antwort, lösung, ergebnis, " />
    <meta name="Description" content="Antworten, Feedback auf diese Nachricht" />
    <link rel="alternate" type="application/rdf+xml" title="RDF Version" href="<% = PostItRdfLink %>" />
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper>
    <uc1:PostItKoerper ID="PostItKoerper1" runat="server"></uc1:PostItKoerper>
    <uc1:PostItTopLabGrid ID="PostItTopLabGrid1" runat="server"></uc1:PostItTopLabGrid>
    <br />
    <br />
    <asp:Panel ID="HilfePanel" runat="server" CssClass="rechtsobenrund Hilfe">
        <span style="text-align: right"><a title="online dokumentation" href="http://doku.oli-it.com/Oberflaeche/Nachricht.htm#PostItTopLab"
            target="doku">
            <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                src="../images/icons/fragezeichen.gif" align="right" alt="weitere Hilfe">
        </a></span>&nbsp;&nbsp;<h4>
            <asp:HyperLink ID="XmlHyperLink" runat="server" ImageUrl="../images/xml.gif" NavigateUrl="http://xml.oli-it.com">
                        xml rss news feed</asp:HyperLink>&nbsp;
            Antworten auf diese Nachricht
        </h4>
        <p>
            Das sind die <strong>Feedback!</strong>
        </p>
        <p>
            Sie könne nach den höchst bezahlten und den tollsten Antworten sortieren.
        </p>
        <p>
            Jede Antwort ist von einem Urheber zum anklicken&nbsp;<br />
            und hat ein Bild und / oder einen weiterführenden Link.</p>
        <ul>
            <li>Wählen Sie eine Antwort aus</li>
            <li>bewerten Sie sie</li>
            <li>dann wird entsprechend abgerechnet und Lohn verteilt.</li>
            <li>Es können auch Antworten auf Antworten gegeben werden.</li>
        </ul>
        <p>
            <em>Wenn die Nachricht gekürzt angezeigt sein sollte, dann liegt es an dieser Unteransicht!<br />
                - </em><em>Entweder auf die Nachricht klicken
                    <br />
                    - </em><em>oder diese Detailansicht mit dem x rechts außen schließen</em></p>
    </asp:Panel>
</asp:Content>
