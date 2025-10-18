<%@ Page Language="c#" CodeBehind="AnAbWurzeln.aspx.cs" AutoEventWireup="True" Inherits="OliWeb.Sites.Edit.AnAbWurzeln"
    MasterPageFile="~/Site.Master" %>

<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="~/Controls/Koerper/StammKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CommandTree" Src="~/Controls/Command/CommandTree.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Kopf" Src="~/Controls/Koerper/Kopf.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PostItKoerper" Src="~/Controls/Koerper/PostItKoerper.ascx" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>AnAbMelden</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper>
    <uc1:PostItKoerper ID="PostItKoerper1" runat="server"></uc1:PostItKoerper>
    <asp:Panel ID="AnWurzelnPanel" runat="server" BorderWidth="2px" BorderStyle="Solid"
        BorderColor="maroon" Style="padding: 1em;">
        <h2>
            <asp:Localize meta:resourcekey="AnwurzelnHeader" runat="server"></asp:Localize>
        </h2>
        <div>
            <asp:Label ID="StammLabel" runat="server" CssClass="Stamm"></asp:Label>
            =>
            <asp:Label ID="Label1" runat="server" CssClass="flowKooK">1 KooK</asp:Label>
            =>
            <span class="PostIt"><b>diese Nachricht</b></span><br />
        </div>
        <asp:Localize meta:resourcekey="AnwurzelnIntro" runat="server"></asp:Localize>
        <p>
            <asp:Button ID="AnWurzelnButton" runat="server" Text="anwurzeln" OnClick="AnWurzelnButton_Click"></asp:Button><br />
        </p>
    </asp:Panel>
    <asp:Panel ID="AbWurzelnPanel" runat="server" BorderWidth="1px" BorderStyle="Solid"
        BorderColor="Red">
         <asp:Localize meta:resourcekey="AbwurzelnIntro" runat="server"></asp:Localize>
        
        <%--        <h2>Abwurzeln</h2>
        <p>
            Sie können sich von dieser Nachricht&nbsp;wieder abwurzeln.
        </p>
        <p>
            Dann sind Sie kein Urheber mehr und<br />
            können die Nachricht nicht mehr bearbeiten.
        </p>
        <p>
            Die Nachricht wird aus ihrer PostIt-Tabelle entfernt und
            <br />
            Sie erhalten keine Benachrichtigungen über Antworten mehr.
        </p>
        <p>
            Die vergebenen KooK können nicht zurückübertragen werden.
        </p>--%>

        <p>
            <asp:Button ID="AbWurzelnButton" runat="server" Text="abwurzeln" OnClick="AbWurzelnButton_Click"></asp:Button>
        </p>
        <p>
    </asp:Panel>
    <asp:Panel ID="HilfePanel" runat="server" CssClass="rechtsobenrund Hilfe" EnableViewState="False">
        <div>
            <span style="text-align: right"><a title="online dokumentation" href="#" target="doku">
                <img class="fragezeichen" style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                    alt="weitere Hilfe" src="../../images/icons/fragezeichen.gif"
                    align="right" />
            </a></span>
            <h2>An- oder Ab- Wurzeln</h2>
            <p>
                Sie wollen sich an diese Nachricht&nbsp;An- oder<br />
                von dieser Nachricht&nbsp;wieder Ab- wurzeln.
            </p>
        </div>
    </asp:Panel>
</asp:Content>
