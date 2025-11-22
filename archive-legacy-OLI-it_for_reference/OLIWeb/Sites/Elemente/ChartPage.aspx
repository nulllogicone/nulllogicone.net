<%@ Page MasterPageFile="~/Site.Master" Title="OLI-it: Chart" Language="c#" CodeBehind="ChartPage.aspx.cs"
    AutoEventWireup="True" Inherits="OliWeb.Sites.Elemente.ChartPage" %>

<%@ Register TagPrefix="uc1" TagName="ChartControl" Src="~/Controls/Floor/Chart/ChartControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        Chart</h1>
    <uc1:ChartControl ID="ChartControl1" runat="server"></uc1:ChartControl>
</asp:Content>
