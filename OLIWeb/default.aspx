<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"
    CodeBehind="default.aspx.cs" Inherits="OliWeb._default" %>

<%@ Register Src="~/Controls/Floor/AnonymIntro.ascx" TagName="AnonymIntro" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:AnonymIntro ID="AnonymIntro1" runat="server" />
</asp:Content>
