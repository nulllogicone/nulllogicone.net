<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="UserEinstellungen.ascx.cs"
    Inherits="OliWeb.Sites.Edit.UserEinstellungen" %>
<div class="rechtsobenrund">
    <asp:HyperLink ID="ExitHyperLink" runat="server" NavigateUrl="~/Sites/StammSite.aspx"
        ToolTip="abbrechen und zurück zu Stamm" CssClass="exitButton"></asp:HyperLink>
    <h3>weitere Einstellungen
    </h3>
    <table id="UserEinstellungenTable" cellspacing="2" cellpadding="10" border="0">
        <tr>
            <th bgcolor="#f5f5f5" colspan="2">
                <p>
                    Web-Oberfläche
                </p>
                <th bgcolor="#f5f5f5" colspan="2">
                    <p>
                        Email Benachrichtigung
                    </p>
                </th>
            <tr>
                <td width="150" bgcolor="#f5f5f5">Sprache<br />
                    <font size="1">zielgruppenangepasste Oberflächenbeschriftung</font>
                </td>
                <td width="400" bgcolor="white">
                    <p>
                        <asp:DropDownList ID="QDropDownList" runat="server" DataTextField="Q" DataValueField="QID">
                        </asp:DropDownList>
                        <font size="1">(metapher)</font>
                    </p>
                </td>
                <td width="126" bgcolor="#f5f5f5" style="width: 126px" rowspan="3">
                    <p>
                        Ereignis
                    </p>
                    <p>
                        <font size="1">bei nebenstehenden Ereignissen möchte ich eine Emailbenachrichtigung
                                    erhalten</font>
                    </p>
                </td>
                <td width="400" bgcolor="#ffffff" rowspan="3">
                    <p>
                        <asp:CheckBox ID="NeueAntwortenCheckBox" runat="server" Checked="True" Text="neuen Antworten auf meine Nachrichten (Inbox)"
                            Font-Size="XX-Small"></asp:CheckBox><br />
                        <br />
                        <asp:CheckBox ID="NeueNachrichtenCheckBox" runat="server" Text="neue Nachrichten an meine Filterprofile (News)"
                            Font-Size="XX-Small"></asp:CheckBox><br />
                        <br />
                        <asp:CheckBox ID="FristAblaufCheckBox" runat="server" Checked="True" Text="Fristablauf meiner Markierungen"
                            Font-Size="XX-Small"></asp:CheckBox><br />
                        <br />
                        <asp:CheckBox ID="GutschriftCheckBox" runat="server" Checked="True" Text="Gutschriften auf mein Konto"
                            Font-Size="XX-Small"></asp:CheckBox><br />
                        <br />
                        <asp:CheckBox ID="NewsletterCheckBox" runat="server" Font-Size="XX-Small" Text="Newsletter von OLI-it"
                            Checked="True"></asp:CheckBox>
                    </p>
                </td>
            </tr>
        <tr>
            <td width="150" bgcolor="#f5f5f5">Hilfe<br />
                <font size="1">Seitenbezogene Kontexthilfe im unteren Bereich</font>
            </td>
            <td width="400" bgcolor="#ffffff">
                <asp:CheckBox ID="HilfeCheckBox" runat="server" Text="Hilfetexte sollen angezeigt werden"
                    Font-Size="XX-Small"></asp:CheckBox>
            </td>
        </tr>
        <tr>
            <td width="150" bgcolor="#f5f5f5">closed<br />
                <font size="1">abgerechnet und abgeschlossene Nachrichten </font>
            </td>
            <td width="400" bgcolor="#ffffff">
                <asp:CheckBox ID="ClosedCheckBox" runat="server" Text="abgeschlossene Nachrichten sollen angezeigt werden "
                    Font-Size="XX-Small"></asp:CheckBox>
            </td>
        </tr>
        <tr>
            <td width="150" bgcolor="#f5f5f5">Werbefrei<br />
                <font size="1">beim Markieren und Filtern</font>
            </td>
            <td width="400" bgcolor="#ffffff">
                <asp:CheckBox ID="WerbefreiCheckBox" runat="server" Text="es sollen keine Bilder im Wortraum angezeigt werden "
                    Font-Size="XX-Small"></asp:CheckBox>
            </td>
            <td width="126" bgcolor="#f5f5f5" style="width: 126px">Emailformat
            </td>
            <td width="400" bgcolor="#ffffff">
                <asp:RadioButton ID="HtmlRadioButton" runat="server" Checked="True" Text="HTML" GroupName="EmailFormatGroup"></asp:RadioButton><br />
                <br />
                <asp:RadioButton ID="TextRadioButton" runat="server" Text="Text" GroupName="EmailFormatGroup"></asp:RadioButton>
            </td>
        </tr>
        <tr>
            <td width="150" bgcolor="#f5f5f5">Freak Mode<br />
                <font size="1">Logik</font>
            </td>
            <td width="400" bgcolor="#ffffff">
                <asp:CheckBox ID="FreakModeCheckBox" runat="server" Font-Size="XX-Small" Text="ich darf bunte Punkte einstellen"></asp:CheckBox>
            </td>
            <td width="400" bgcolor="#f5f5f5">Newsletter
            </td>
            <td width="400" bgcolor="#ffffff" colspan="1"></td>
        </tr>
        <tr>
            <td width="150" bgcolor="#f5f5f5">
                <p>
                    TxtOnly<br />
                    <font size="1">Typ der Nachrichten</font>
                </p>
            </td>
            <td width="400" bgcolor="#ffffff">
                <asp:CheckBox ID="TxtOnlyCheckBox" runat="server" Font-Size="XX-Small" Text="ich darf nur Textnachrichten verfassen"></asp:CheckBox>
            </td>
            <td width="400" bgcolor="#f5f5f5"></td>
            <td width="400" bgcolor="#ffffff"></td>
        </tr>
        <tr>
            <td width="150" bgcolor="#f5f5f5">ZeilenZahl
            </td>
            <td width="400" bgcolor="#ffffff">
                <asp:TextBox ID="ZeilenZahlTextBox" runat="server" Width="50px"></asp:TextBox>&nbsp;<font
                    size="1">in den Tabellenansichten</font>
            </td>
            <td width="400" bgcolor="#f5f5f5"></td>
            <td width="400" bgcolor="#ffffff">
                <font size="1"></font>
            </td>
        </tr>
        <tr>
            <td width="150">&nbsp;
            </td>
            <td width="400"></td>
            <td style="width: 126px" width="126"></td>
            <td width="400"></td>
        </tr>
        <tr>
            <td bgcolor="#f5f5f5" align="center" colspan="4">
                <asp:Image ID="Image1" runat="server" Width="25px" BorderStyle="None" ImageUrl="../../images/icons/ok.gif"></asp:Image>
                <asp:LinkButton ID="UpdateLinkButton" runat="server" CssClass="BigButton" OnClick="UpdateLinkButton_Click">übernehmen</asp:LinkButton>
                <asp:Image ID="Image2" runat="server" Width="25px" BorderStyle="None" ImageUrl="../../images/icons/cancel.gif"></asp:Image>
                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="BigButton" NavigateUrl="~/Sites/StammSite.aspx">abbrechen</asp:HyperLink>
            </td>
        </tr>
    </table>
    <p>
        &nbsp;
    </p>
    <h3>Bankverbindung, IBAN, BitCoin
        <img src="https://www.coinwarz.com/content/images/BitcoinCash-64x64.png" />
    </h3>
    <p>
        <ul>
            <li>You can always exchange your <b>flowKook:</b>
                <asp:Label runat="server" ID="flowKookLabel" CssClass="flowKooK">KooK</asp:Label>
                <br />
                to any other currency (EUR, USD, BIC, ..)</li>
            <li>or pay real money in for more <b>boundKooK:</b>
                <asp:Label runat="server" ID="boundKookLabel" CssClass="boundKooK">KooK</asp:Label>
            </li>
        </ul>
    </p>
    <p>
        Dieser Bereich ist&nbsp;so vertraulich, daß er persönlich&nbsp;<br />
        behandelt wird: Bitte eine Email an <a href="mailto:info@oli-it.com">info@oli-it.com</a>
    </p>
    <p>
        &nbsp;
    </p>
    <table id="UserEinstellungenTable" cellspacing="2" cellpadding="10" border="0">
        <tr>
            <td width="150" bgcolor="#f5f5f5">Bank
            </td>
            <td width="400" bgcolor="#f5f5f5">
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="150" bgcolor="#f5f5f5">BLZ
            </td>
            <td width="400" bgcolor="#f5f5f5">
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="150" bgcolor="#f5f5f5">Kto
            </td>
            <td width="400" bgcolor="#f5f5f5">
                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
</div>
