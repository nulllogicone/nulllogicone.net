<%@ Page ValidateRequest="false" Language="c#" CodeBehind="PostItMaker.aspx.cs" AutoEventWireup="True"
    Inherits="OliWeb.Sites.Edit.PostItMaker" MasterPageFile="~/Site.Master"   %>

<%@ Register TagPrefix="uc1" TagName="KlickBild" Src="~/Controls/Gimicks/KlickBild.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PostItCodeGrid" Src="~/Controls/Koerper/ViewGrids/PostItCodeGrid.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BildPicker" Src="~/Controls/Gimicks/BildPicker.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="~/Controls/Koerper/StammKoerper.ascx" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>PostItMaker</title>
    <link href="PostItMaker.css" type="text/css" rel="stylesheet" />
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="ContentPlaceHolder1"
    runat="server">
    <uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper>
    <div class="rechtsobenrund">
        <h2>Nachricht bearbeiten</h2>
        <p>
            <asp:LinkButton ID="MarkierLinkButton" runat="server" Font-Bold="True" OnClick="MarkierLinkButton_Click">5. Markieren</asp:LinkButton>
            |
            <asp:LinkButton ID="FormulierenLinkButton" runat="server" Enabled="False" Font-Bold="True"
                OnClick="FormulierenLinkButton_Click">1. Formulieren</asp:LinkButton>&nbsp;|
            <asp:LinkButton ID="IllustrierenLinkButton" runat="server" Font-Bold="True" OnClick="IllustrierenLinkButton_Click">2. Illustrieren</asp:LinkButton>&nbsp;|
            <asp:LinkButton ID="FrankierenLinkButton" runat="server" CssClass="panel" Font-Bold="True"
                OnClick="FrankierenLinkButton_Click">3. Frankieren</asp:LinkButton>&nbsp;|
            <asp:LinkButton ID="FristLinkButton" runat="server" Font-Bold="True" OnClick="FristLinkButton_Click">4. Frist</asp:LinkButton>&nbsp;|
        </p>
        <font size="1">
                    <p align="right">
                        <asp:Image ID="Image1" runat="server" BorderStyle="None" ImageUrl="../../images/icons/ok.gif"
                            Width="25px"></asp:Image>
                        <asp:Button ID="OkButton" runat="server" Text=" OK " OnClick="OkButton_Click"></asp:Button>&nbsp;
                        <asp:Image ID="Image3" runat="server" Width="25px" ImageUrl="../../images/icons/cancel.gif"
                            BorderStyle="None"></asp:Image>
                        <asp:Button ID="CancelButton" runat="server" Text="cancel" OnClick="CancelButton_Click">
                        </asp:Button></p>
                </font>
        <asp:Panel ID="FormulierenPanel" runat="server" CssClass="panel">
            <h1>1. Formulieren</h1>
            <h2>PostIt</h2>
            <div class="PostIt">
                <asp:TextBox ID="TitelTextBox" runat="server" Font-Size="XX-Large" Font-Bold="True" Width="99%" ToolTip="Bitte geben Sie einen Titel für die Nachricht ein"></asp:TextBox><br />

                <asp:TextBox ID="PostItTextBox" runat="server" Rows="5" TextMode="MultiLine" Width="99%"
                    Style="border: solid 1px yellow;"></asp:TextBox>
                Typ:
                <asp:DropDownList ID="TypDropDownList" runat="server">
                    <asp:ListItem Value="txt">txt</asp:ListItem>
                    <asp:ListItem Value="htm">htm</asp:ListItem>
                    <asp:ListItem Value="xml">xml</asp:ListItem>
                    <asp:ListItem Value="uri">uri</asp:ListItem>
                </asp:DropDownList>
            </div>
        </asp:Panel>
        <asp:Panel ID="IllustrierenPanel" runat="server" Visible="False">
            <table class="tabelle" id="IllustrierenTable">
                <tr>
                    <td class="titel">2. Illustrieren
                    </td>
                </tr>
                <tr>
                    <td class="mainframe" valign="top">
                        <p>
                            <asp:Image ID="Image2" runat="server" Width="30px" ImageUrl="../../images/weltrotring.jpg"></asp:Image>Link
                            <asp:TextBox ID="UrlTextBox" runat="server" Width="400px"></asp:TextBox><br />
                            <font size="1">(Bsp.: <a href="http://www.domain.com/seite.htm?id=123">http://www.domain.com/seite.htm?id=123</a>)</font>
                        </p>
                        <p>
                            <uc1:KlickBild ID="KlickBild1" runat="server"></uc1:KlickBild>
                            <br />
                            Bild
                            <asp:TextBox ID="DateiTextBox" runat="server" Width="400px"></asp:TextBox><br />
                            <font size="1">(Bsp.: http://domain.abc/bild.jpg&nbsp;oder unten ein Bild auswählen)</font>
                            <br />
                            <uc1:BildPicker ID="BildPicker1" runat="server"></uc1:BildPicker>
                            <p>
                            </p>
                        </p>
                    </td>
                </tr>
                <tr class="rechtsobenrund Hilfe">
                    <td bgcolor="#ffffff">
                        <p>
                            &nbsp;
                        </p>
                        <h4>Illustrieren</h4>
                        <p>
                            Sie können der Nachricht einen Link auf eine&nbsp;Seite im Web&nbsp;hinzufügen
                        </p>
                        <p>
                            und einen Link auf ein Bild angeben oder &nbsp;ein hochgeladenes&nbsp;auswählen!
                        </p>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="FrankierenPanel" runat="server" CssClass="panel" Visible="False">
            <table class="tabelle" id="FrankierenTable" cellspacing="1" cellpadding="1" width="100%"
                border="0">
                <tr>
                    <td class="titel">3. Frankieren
                    </td>
                </tr>
                <tr>
                    <td class="mainframe" valign="top" align="center">
                        <p>
                            &nbsp;
                        </p>
                        <p>
                            &nbsp;
                        </p>
                        <table id="PostItMakerKooKTable" bordercolor="#003300" cellspacing="1" cellpadding="1"
                            bgcolor="#ffffff" border="1">
                            <tr>
                                <td>Gesamtwert der Nachricht
                                </td>
                                <td align="center">
                                    <asp:Label ID="GesamtKooKLabel" runat="server" CssClass="flowKooK"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Von mir vergebene KooK
                                </td>
                                <td align="center">
                                    <asp:Label ID="KooKLabel" runat="server" CssClass="flowKooK"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>zu übertragender Betrag<br />
                                    <font size="1">(z.B. 3,75 oder -7.11)</font>

                                </td>
                                <td align="center">Betrag:     <span class="flowKooK">
                                    <asp:TextBox ID="BetragTextBox" runat="server" Font-Bold="True" Width="93px" Font-Size="Larger">0,00</asp:TextBox></span>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td align="center"></td>
                            </tr>
                        </table>
                        <p>
                            &nbsp;
                        </p>
                        <p>
                            &nbsp;
                        </p>
                    </td>
                </tr>
                <tr class="rechtsobenrund Hilfe">
                    <td bgcolor="#ffffff" height="50">
                        <p>
                            &nbsp;
                        </p>
                        <h4>Frankieren</h4>
                        <p>
                            Wenn Sie die Nachricht mit ein paar KooK aus ihrem Vermögen frankieren, erhöhen
                            sie die Motivation der Empfänger zu Antworten.
                        </p>
                        <p>
                        </p>
                        <p>
                            <font face="Arial">Neue Nachrichten erhalten automatisch 1 KooK.</font>
                        </p>
                        <p>
                            Abgerechnet wird nach Fristablauf.
                        </p>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="FristPanel" runat="server" CssClass="panel" Visible="False">
            <table class="tabelle" id="FristTable">
                <tr>
                    <td class="titel">4. Frist
                        <asp:Label ID="FristLabel" runat="server" Font-Size="XX-Small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="mainframe" valign="top" align="center">
                        <p>
                            &nbsp;
                        </p>
                        <p>
                            Datum
                            <asp:Calendar ID="Calendar1" runat="server" Width="200px" Height="180px" Font-Size="8pt"
                                DayNameFormat="FirstLetter" ForeColor="Black" Font-Names="Verdana" BorderColor="#999999"
                                CellPadding="4" BackColor="White">
                                <TodayDayStyle ForeColor="Black" BackColor="#E0E0E0"></TodayDayStyle>
                                <SelectorStyle BackColor="#CCCCCC"></SelectorStyle>
                                <NextPrevStyle VerticalAlign="Bottom"></NextPrevStyle>
                                <DayHeaderStyle Font-Size="7pt" Font-Bold="True" BackColor="#CCCCCC"></DayHeaderStyle>
                                <SelectedDayStyle Font-Bold="True" BorderWidth="2px" ForeColor="White" BorderStyle="Solid"
                                    BorderColor="Red"></SelectedDayStyle>
                                <TitleStyle Font-Bold="True" BorderColor="Black" BackColor="#999999"></TitleStyle>
                                <WeekendDayStyle BackColor="#FFFFCC"></WeekendDayStyle>
                                <OtherMonthDayStyle ForeColor="#808080"></OtherMonthDayStyle>
                            </asp:Calendar>
                            &nbsp;<br />
                            Uhrzeit
                            <asp:TextBox ID="UhrzeitTextBox" runat="server"></asp:TextBox>
                        </p>
                        <p>
                            &nbsp;
                        </p>
                    </td>
                </tr>
                <tr class="rechtsobenrund Hilfe">
                    <td bgcolor="#ffffff" height="50">
                        <p>
                            &nbsp;
                        </p>
                        <h4>Frist</h4>
                        <p>
                            Setzen Sie eine Frist! Bis wann wollen sie eine Antwort. Nach Fristablauf wird abgerechnet.
                        </p>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="MarkierPanel" runat="server" CssClass="panel" Visible="False">
            <table class="tabelle" id="MarkierTable" cellspacing="1" cellpadding="1" width="100%"
                border="0">
                <tr>
                    <td class="titel">5.&nbsp;Markieren
                    </td>
                </tr>
                <tr>
                    <td class="mainframe" valign="top" align="center">
                        <p>
                            &nbsp;
                        </p>
                        <p>
                            &nbsp;<uc1:PostItCodeGrid ID="PostItCodeGrid1" runat="server"></uc1:PostItCodeGrid>
                            <p>
                            </p>
                            <p>
                                &nbsp;
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                        </p>
                    </td>
                </tr>
                <tr class="rechtsobenrund Hilfe">
                    <td bgcolor="#ffffff" height="50">
                        <p>
                            &nbsp;
                        </p>
                        <h4>Markieren</h4>
                        <p>
                            Die Markierung der Nachricht bestimmt ihre Empfänger.
                        </p>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div class="rechtsobenrund Hilfe">
            <h4>Formulieren</h4>
            <p>
                Formulieren Sie im oberen Feld einen aussagekräftigen <strong>Titel</strong> und
                in dem Textfeld Ihre <strong>Nachricht.</strong>
            </p>
            <p>
                Sie soll den Wunschempfängern in's Auge&nbsp;stechen!
            </p>
            <p>
                Schreiben Sie salopp, umgangssprachlich, wie einem Vertrauten gegenüber.
            </p>
            <p>
                Alle anderen Punkte, Kriterien&nbsp;und Chemie werden logisch intelligent verknüpft.&nbsp;&nbsp;&nbsp;
            </p>
        </div>
    </div>
</asp:Content>
