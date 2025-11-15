<%@ Page Language="c#" CodeBehind="StammExtras.aspx.cs" AutoEventWireup="True" Inherits="OliWeb.Sites.Edit.StammExtras"
    MasterPageFile="~/Site.Master" %>

<%@ Register TagPrefix="uc1" TagName="UserEinstellungen" Src="UserEinstellungen.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="~/Controls/Koerper/StammKoerper.ascx" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>StammExtras</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper>
    <uc1:UserEinstellungen ID="UserEinstellungen1" runat="server"></uc1:UserEinstellungen>
</asp:Content>
