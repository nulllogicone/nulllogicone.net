<%@ Page Language="c#" CodeBehind="AnglerTSite.aspx.cs" MasterPageFile="~/Site.Master"
    AutoEventWireup="True" Inherits="OliWeb.Sites.AnglerTSite" Description="Antworten auf diese Treffer" %>

<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="~/Controls/Koerper/StammKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AnglerKoerper" Src="~/Controls/Koerper/AnglerKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PostItKoerper" Src="~/Controls/Koerper/PostItKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopLabKoerper" Src="~/Controls/Koerper/TopLabKoerper.ascx" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <meta name="Description" content="ein Treffer mit einer ausgewählten Antwort">
    <meta name="keywords" content="toplab, antwort, angler, nachricht, ">
    <title>OLI-it: Filterprofil mit Nachricht und Antwort</title>
    <meta name="Robots" content="INDEX,FOLLOW">
    <link href="../OliWeb.css" type="text/css" rel="stylesheet">
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper>
    <uc1:AnglerKoerper ID="AnglerKoerper1" runat="server"></uc1:AnglerKoerper>
    <uc1:PostItKoerper ID="PostItKoerper1" runat="server"></uc1:PostItKoerper>
    <uc1:TopLabKoerper ID="TopLabKoerper1" runat="server"></uc1:TopLabKoerper>
</asp:Content>
