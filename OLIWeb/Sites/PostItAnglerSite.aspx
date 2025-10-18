<%@ Page Language="c#" CodeBehind="PostItAnglerSite.aspx.cs" MasterPageFile="~/Site.Master" EnableViewState="true"
    AutoEventWireup="True" Inherits="OliWeb.Sites.PostItAnglerSite" Description="Empfänger dieser Nachricht" %>

<%@ Register TagPrefix="uc1" TagName="PostItAnglerGrid" Src="~/Controls/Koerper/ViewGrids/PostItAnglerGrid.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PostItKoerper" Src="~/Controls/Koerper/PostItKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="~/Controls/Koerper/StammKoerper.ascx" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>OLI-it: Empfänger von Nachricht:
        <% = MyTitle %>
    </title>
    <meta name="keywords" content="empfänger, receiver, experte" />
    <meta name="Description" content="Empfänger dieser Nachricht" />
    <link rel="alternate" type="application/rdf+xml" title="RDF Version" href="<% = PostItRdfLink %>" />
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper>
    <uc1:PostItKoerper ID="PostItKoerper1" runat="server"></uc1:PostItKoerper>
    <uc1:PostItAnglerGrid ID="PostItAnglerGrid1" runat="server" EnableViewState="True"></uc1:PostItAnglerGrid>
    <%--Hilfe--%>
    <asp:Panel ID="HilfePanel" runat="server" CssClass="rechtsobenrund Hilfe">
        <span style="text-align: right"><a title="online dokumentation" href="http://doku.oli-it.com/Oberflaeche/Nachricht.htm#PostItAngler"
            target="doku">
            <img style="float:right; border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                alt="weitere Hilfe" src="../images/icons/fragezeichen.gif" width="42" height="42"  />
        </a></span>
        <h4><% = GetGlobalResourceObject("SAPCT","P_X")  %>      </h4>
        <p>
            (pls translate) Das sind die Filterprofile mit ihren Stämmen, die diese Nachricht erhalten!
        </p>
        <ul>
            <li>Wenn man sehen möchte, was sie sonst noch so erhalten -&gt;&nbsp;auf das Filterprofil
                klicken.</li>
            <li>Um sich ein Bild von dem Mensch zu machen, der dahinter steckt&nbsp;-&gt; auf Stamm
                klicken. </li>
        </ul>
        <p>
            <em><em>Wenn die Nachricht gekürzt angezeigt sein sollte, dann liegt es an dieser Unteransicht!<br />
                - </em><em>Entweder auf die Nachricht klicken
                    <br />
                    - </em><em>oder diese Detailansicht mit den "Pfeilen nach oben" - rechts außen schließen</em></em>
        </p>
    </asp:Panel>
</asp:Content>
