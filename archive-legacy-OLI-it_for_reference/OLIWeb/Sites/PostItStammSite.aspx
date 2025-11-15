<%@ Page Language="c#" CodeBehind="PostItStammSite.aspx.cs" MasterPageFile="~/Site.Master"
    AutoEventWireup="True" Inherits="OliWeb.Sites.PostItStammSite" Description="Urheber dieser Nachricht" %>

<%@ Register TagPrefix="uc1" TagName="CloseHyperLink" Src="../Controls/Command/GetCommand/SpecialCommand/CloseHyperLink.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="~/Controls/Koerper/StammKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PostItKoerper" Src="~/Controls/Koerper/PostItKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PostItStammGrid" Src="~/Controls/Koerper/ViewGrids/PostItStammGrid.ascx" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>OLI-it: Urheber der Nachricht:
        <% = MyTitle %>
    </title>
    <meta name="keywords" content="urheber, stamm, nachricht, editor, verantwortlicher, " />
    <meta name="Description" content="Die Urheber dieser Nachricht" />
    <link rel="alternate" type="application/rdf+xml" title="RDF Version" href="<% = PostItRdfLink %>" />
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper>
    <uc1:PostItKoerper ID="PostItKoerper1" runat="server"></uc1:PostItKoerper>
    <uc1:PostItStammGrid ID="PostItStammGrid1" runat="server"></uc1:PostItStammGrid>
    <br />
    <br />
    <asp:Panel ID="HilfePanel" runat="server" CssClass="rechtsobenrund Hilfe">
        <span style="text-align: right"><a title="online dokumentation" href="http://doku.oli-it.com/Oberflaeche/Nachricht.htm#PostItStamm"
            target="doku">
            <img runat="server" style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                alt="weitere Hilfe" src="~/images/icons/fragezeichen.gif" align="right" />
        </a></span>
        <h4><asp:Localize meta:resourcekey="PostItStammHeader" Text="abc" runat="server"></asp:Localize>
        </h4>
        <asp:Localize meta:resourcekey="PostItStammDescription" Text="cde" runat="server"></asp:Localize>
        <uc1:CloseHyperLink ID="CloseHyperLink1" runat="server" ToolTip="Urheber schließen"
            NavigateUrl="~/Sites/PostItSite.aspx"></uc1:CloseHyperLink>
    </asp:Panel> 
</asp:Content>
