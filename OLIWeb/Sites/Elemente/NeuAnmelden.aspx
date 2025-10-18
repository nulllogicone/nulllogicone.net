<%@ Page Language="c#" MasterPageFile="~/Site.Master" CodeBehind="NeuAnmelden.aspx.cs"
    AutoEventWireup="True" Inherits="OliWeb.Sites.Elemente.NeuAnmelden" Culture="auto"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register TagPrefix="uc1" TagName="AnonymMenu" Src="~/Controls/Floor/AnonymMenu.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Kopf" Src="~/Controls/Koerper/Kopf.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="~/Controls/Koerper/StammKoerper.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper>
    <asp:Panel ID="RechtPanel" runat="server" meta:resourcekey="RechtPanelResource1">
        <h1>
            <asp:Literal ID="LiteralH1" Text="Anmeldung" runat="server" meta:resourcekey="LiteralH1Resource1" /></h1>
        <asp:Localize ID="LocalizeP1" runat="server" meta:resourcekey="LocalizeP1Resource1"
            Text="     
                  
                    &lt;p&gt;
                        Sie wollen sich bei &lt;font face=&quot;Times New Roman&quot; size=&quot;3&quot;&gt;&lt;strong&gt;OLI-it&lt;/strong&gt;&lt;/font&gt;
                        anmelden.
                        &lt;br /&gt;
                        Das ist kostenlos, unverbindlich und kann jederzeit widerrufen werden.&lt;/p&gt;
                    &lt;p&gt;
                        Sie erhalten 10 KooK als Startpunktestand.&lt;/p&gt;
                    &lt;h2&gt;
                        Nutzungsbedingungen&lt;/h2&gt;
                    &lt;p&gt;
                        Die Urheber von Nachrichten aller Art sind für ihre Inhalte verantwortlich und mit
                        einer freien Verwendung einverstanden.&lt;br /&gt;
                        Verboten sind ausdrücklich gewaltverherrlichende, pornographische und&amp;nbsp;radikale
                        Äusserungen jeglicher Art.&lt;/p&gt;
                    &lt;p&gt;
                        &lt;a href=&quot;../../Nutzungsbedingungen.aspx&quot;&gt;Vollständige Nutzungsbedingungen&lt;/a&gt;&lt;/p&gt;
                    &lt;h2&gt;
                        Datenschutzbestimmungen&lt;/h2&gt;
                    &lt;p&gt;
                        OLI-it speichert personenbezogene Daten nur für die eigene Verwendung.
                        &lt;br /&gt;
                        Diese werden nicht an Dritte ausgehändigt.&lt;br /&gt;
                        Während einer Sitzung wird Ihre IP Adresse gespeichert.&lt;/p&gt;
                    &lt;h2&gt;
                        Gewährleistungsausschluss&lt;/h2&gt;
                    &lt;p&gt;
                        Dieser Dienst wird kostenlos angeboten und schließt jeden Anspruch auf Korrektheit,
                        Verfügbarkeit und Schadensersatz&amp;nbsp;aus.&lt;/p&gt;"></asp:Localize>
        <!-- FreundschaftsPanel - ob das noch gebraucht wird? TODO: muss jedenfalls überarbeitet werden -->
        <asp:Panel ID="Panel1" runat="server" BackColor="WhiteSmoke" meta:resourcekey="Panel1Resource1">
            <h2>
                <asp:Literal ID="LiteralH2_1x" runat="server" /></h2>
            <asp:Localize ID="LocalizeP2" runat="server" meta:resourcekey="LocalizeP2Resource1"
                Text="Wenn sie von einem Freund geworben wurden, so kann er ebenfalls 10 KooK erhalten.&lt;br /&gt;"></asp:Localize>
            <asp:Panel ID="FreundGuidPanel" runat="server" meta:resourcekey="FreundGuidPanelResource1">
                Freund&nbsp;
                <asp:Label ID="FreundLabel" runat="server" CssClass="Stamm" meta:resourcekey="FreundLabelResource1"></asp:Label></asp:Panel>
            <asp:Panel ID="FreundEmailPanel" runat="server" meta:resourcekey="FreundEmailPanelResource1">
                Geben sie die Emailadresse an, die er bei OLI-it verwendet:
                <asp:TextBox ID="FreundEmailTextBox" runat="server" meta:resourcekey="FreundEmailTextBoxResource1"></asp:TextBox>
                <asp:Button ID="SuchFreundButton" runat="server" Text="suchen" OnClick="SuchFreundButton_Click"
                    meta:resourcekey="SuchFreundButtonResource1"></asp:Button></asp:Panel>
            <br />
        </asp:Panel>
        <p>
            <!-- Checkbox und Button zum Abschluss -->
            &nbsp;</p>
        <p>
            <asp:CheckBox ID="OkCheckBox" runat="server" Text="gelesen, verstanden und akzeptiert"
                Font-Bold="True" OnCheckedChanged="CheckBox1_CheckedChanged" meta:resourcekey="OkCheckBoxResource1">
            </asp:CheckBox>
            <asp:Label ID="OkLabel" runat="server" ForeColor="Red" meta:resourcekey="OkLabelResource1"></asp:Label></p>
        <p>
            <asp:Button ID="CancelButton" runat="server" Text="zurück" ToolTip="bricht die Neuanmeldung ab"
                OnClick="CancelButton_Click" meta:resourcekey="CancelButtonResource1"></asp:Button>&nbsp;
            <asp:Button ID="OkButton" runat="server" Text="weiter" ToolTip="zu persönliche Daten eingeben"
                OnClick="OkButton_Click" meta:resourcekey="OkButtonResource1"></asp:Button></p>
    </asp:Panel>
</asp:Content>
