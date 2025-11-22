<%@ Page Language="c#" CodeBehind="StammPostItSite.aspx.cs" AutoEventWireup="True"
    Inherits="OliWeb.Sites.StammPostItSite" EnableViewState="True" MasterPageFile="~/Site.Master"
    Description="meine Nachrichten. PostIt, die dieser Stamm geschrieben hat." %>

<%@ Register TagPrefix="uc1" TagName="StammPostItGrid" Src="~/Controls/Koerper/ViewGrids/StammPostItGrid.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="~/Controls/Koerper/StammKoerper.ascx" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>OLI-it: Nachrichten von
        <% = MyTitle %>
    </title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper>
    <uc1:StammPostItGrid ID="StammPostItGrid1" runat="server"></uc1:StammPostItGrid>
    <asp:Panel ID="HilfePanel" runat="server" CssClass="rechtsobenrund Hilfe">
        <div>
            <h4>
                <span style="text-align: right">
                    <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                         alt="weitere Hilfe" src="../images/icons/fragezeichen.gif" align="right" /></span>
               
                Meine Nachrichten,
            </h4> <asp:HyperLink ID="XmlHyperLink" runat="server" NavigateUrl="http://xml.oli-it.com"
                    ImageUrl="../images/xml.gif"></asp:HyperLink>&nbsp;
            <p>
                die
                <asp:Label ID="StammLabel" runat="server" CssClass="Stamm" Style="background-color: antiquewhite"></asp:Label>
                geschrieben hat oder an die&nbsp;er/sie <font style="background-color: #f1f1f1">angewurzelt</font>
                ist.
            </p>
            <p>
                Man kann nach den Spaltenüberschriften auf- und absteigend sortieren.</p>
            <ul>
                <li>nach den meisten Urhebern, Empfängern oder Antworten</li>
                <li>nach den teuerst bezahlten&nbsp;oder bald abgelaufenen Nachrichten </li>
                <li>nach den jüngsten und wertvollsten</li></ul>
            <p>
                Wenn man in der Tabelle auf die Zahlen klickt, kommt man direkt in die gewünschte
                Unteransicht.</p>
            <p>
                Wenn man auf die Nachricht klickt, kann man sehen</p>
            <ul>
                <li>wer sie geschrieben hat </li>
                <li>wie sie markiert ist, an wen sie geht </li>
                <li>und welche Antworten gegeben wurden</li></ul>
        </div>
    </asp:Panel>
</asp:Content>
