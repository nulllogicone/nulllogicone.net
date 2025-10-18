<%@ Page ValidateRequest="false" Language="c#" CodeBehind="StammEdit.aspx.cs" AutoEventWireup="True"
    Inherits="OliWeb.Sites.Edit.StammEdit" MasterPageFile="~/Site.Master" %>

<%@ Register TagPrefix="uc1" TagName="BildPicker" Src="~/Controls/Gimicks/BildPicker.ascx" %>
<%@ Register TagPrefix="uc1" TagName="KlickBild" Src="~/Controls/Gimicks/KlickBild.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="~/Controls/Koerper/StammKoerper.ascx" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>StammEdit</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel runat="server" DefaultButton="UpdateButton">
        <uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper><br />
        <table id="StammEditRumpfTable" cellspacing="10" cellpadding="10"  border="0"
            class="StammOrgan">
            <tr>
                <td class="Stamm" style="width: 145px" bgcolor="whitesmoke">
                    Stamm *<br />
                    <font size="1">Pseudonym oder realname</font>
                </td>
                <td bgcolor="white">
                    <asp:TextBox ID="StammTextBox" runat="server" Width="300px"></asp:TextBox><br />
                    <font size="1">(Bsp.: Max Müller, MaxiLein, M8A8X, max1970)</font>
                </td>
            </tr>
            <tr>
                <td style="width: 145px" bgcolor="whitesmoke">
                    kurze Beschreibung<br />
                    <font size="1">Slogan, Weisheit, Steckbrief ... was ein und gefällt ...</font>
                </td>
                <td bgcolor="white">
                    <asp:TextBox ID="BeschreibungTextBox" runat="server" Width="300px" TextMode="MultiLine"
                        Height="73px"></asp:TextBox><br />
                    <font size="1">Beruf, Familie, Freizeit, Wohnort, Alter, Hobbies, Anschrift (wer will)</font>
                </td>
            </tr>
            <tr>
                <td style="width: 145px" bgcolor="whitesmoke">
                    Kennwort<br />
                    <font size="1">wenn es leer bleibt, kann sich jeder einloggen</font>
                </td>
                <td bgcolor="white">
                    <p>
                        <asp:TextBox ID="KennwortTextBox" runat="server" Width="300px" TextMode="Password"></asp:TextBox><br />
                        <font size="1">Nur eingeloggt kann man neue Nachrichten und Antworten erstellen und
                                    ändern.</font>
                    </p>
                </td>
            </tr>
            <tr>
                <td style="width: 145px" bgcolor="whitesmoke">
                    Bild<br />
                    <font size="1">hochladen oder Internetlink</font>
                </td>
                <td bgcolor="white">
                    <uc1:KlickBild ID="KlickBild1" runat="server" Breite="30"></uc1:KlickBild>
                    <asp:TextBox ID="BildTextBox" runat="server" Width="300px"></asp:TextBox><br />
                    <font size="1">(<a href="http://domain/bild.jpg">http://domain/bild.jpg</a> oder /IhreGuid/Bild.jpg)<br />
                            </font>
                    <asp:LinkButton ID="BildPickerLinkButton" runat="server" Font-Bold="True" CssClass="cmdbutton"
                        OnClick="BildPickerLinkButton_Click">BildPicker</asp:LinkButton>
                    <uc1:BildPicker ID="BildPicker1" runat="server"></uc1:BildPicker>
                </td>
            </tr>
            <tr>
                <td style="width: 145px" bgcolor="whitesmoke">
                    Emailadresse<br />
                    <font size="1">für automatische Benachrichtigungen von OLI-it (s. Einstellungen - Extras)<br />
                                oder für persönliche Nachrichten von anderen.</font>
                </td>
                <td bgcolor="white">
                    <p>
                        <asp:TextBox ID="EmailTextBox" runat="server" Width="300px"></asp:TextBox><br />
                        <font size="1">Die Adresse wird auf den Seiten von OLI-it <strong>nicht</strong> angezeigt.
                                    Das schützt vor Adressdieben. </font>
                    </p>
                </td>
            </tr>
            <tr>
                <td style="width: 145px" bgcolor="#f5f5f5">
                    Homepage<br />
                    <font size="1">oder persönlicher Link</font>
                </td>
                <td bgcolor="#ffffff">
                    <asp:TextBox ID="LinkTextBox" runat="server" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td bgcolor="whitesmoke" colspan="2" align="center">
                    <asp:Image ID="Image1" runat="server" Width="25px" BorderStyle="None" ImageUrl="../../images/icons/ok.gif">
                    </asp:Image>
                    <asp:Button ID="UpdateButton" runat="server" Text="update" OnClick="UpdateButton_Click">
                    </asp:Button>&nbsp;
                    <asp:Image ID="Image2" runat="server" Width="25px" BorderStyle="None" ImageUrl="../../images/icons/cancel.gif">
                    </asp:Image>
                    <asp:Button ID="CancelButton" runat="server" Text="cancel" OnClick="CancelButton_Click">
                    </asp:Button>
                </td>
            </tr>
            <tr>
                <td align="center" bgcolor="#f5f5f5">
                    <span class="commandbutton">
                        <asp:HyperLink ID="ExtrasHyperLink" runat="server" CssClass="BigButton" NavigateUrl="StammExtras.aspx"
                            Font-Bold="True">Extras</asp:HyperLink></span>
                </td>
                <td align="left" bgcolor="#ffffff">
                    <p>
                        <font size="1">Optionen und weitere Einstellungen für diesen Stamm.</font>
                    </p>
                    <p>
                        <font size="1">Die oben vorgenommenen Änderungen müssen vorher gespeichert werden.</font>
                    </p>
                </td>
            </tr>
        </table>
        <br />
    </asp:Panel>
    <asp:Panel ID="HilfePanel" runat="server" CssClass="rechtsobenrund Hilfe" EnableViewState="False">
        <span style="text-align: right"><a title="online dokumentation" href="http://doku.oli-it.com/Oberflaeche/stamm.htm#edit"
            target="doku">
            <img class="fragezeichen" style="border-right: 0px; border-top: 0px; border-left: 0px;
                border-bottom: 0px" alt="weitere Hilfe" src="../../images/icons/fragezeichen.gif"
                align="right">
        </a></span>&nbsp;&nbsp;<h2>
            Stamm editieren</h2>
        <p>
            Hier können sie ihre Stammdaten bearbeiten und Extras-Optionen einstellen</p>
    </asp:Panel>
</asp:Content>
