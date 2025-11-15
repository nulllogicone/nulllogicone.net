<%@ Page Language="c#" CodeBehind="PostItCodeSite.aspx.cs" AutoEventWireup="True"
    MasterPageFile="~/Site.Master" Inherits="OliWeb.Sites.PostItCodeSite" SmartNavigation="False"
    Description="Markierung der Nachricht" %>

<%@ Register TagPrefix="uc1" TagName="PostItCodeGrid" Src="../Controls/Koerper/ViewGrids/PostItCodeGrid.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="~/Controls/Koerper/StammKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PostItKoerper" Src="~/Controls/Koerper/PostItKoerper.ascx" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>OLI-it: Code - Markierung</title>
    <meta name="keywords" content="code, nkbz, markierung, beschreibung, adresse, filter" />
    <meta name="Description" content="die Markierung einer Nachricht" />
    <meta name="Copyright" content="Frederic Luchting">
    <meta name="Robots" content="INDEX,FOLLOW">
    <link href="../OliWeb.css" type="text/css" rel="stylesheet">
    <link rel="alternate" type="application/rdf+xml" title="RDF Version" href="<% = PostItRdfLink %>">
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper>
    <uc1:PostItKoerper ID="PostItKoerper1" runat="server"></uc1:PostItKoerper>
    <uc1:PostItCodeGrid ID="PostItCodeGrid1" runat="server"></uc1:PostItCodeGrid>
    <asp:Panel ID="HilfePanel" runat="server" CssClass="rechtsobenrund Hilfe">
        <h4>
            <span style="text-align: right"><a title="online dokumentation" href="http://doku.oli-it.com/Oberflaeche/Nachricht.htm#PostItCode"
                target="doku">
                <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                    alt="weitere Hilfe" src="../images/icons/fragezeichen.gif" align="right">
            </a></span>Code</h4>
        <p>
            Die Markierungen einer Nachricht.</p>
        <p>
            Der Urheber beschreibt sich (freiwillig) und markiert die Kategorien und Inhalte
            der Nachricht.
            <br />
            Dann kann er die gewünschten Eigenschaften der Empfänger seiner Nachricht beschreiben.</p>
    </asp:Panel>
</asp:Content>
