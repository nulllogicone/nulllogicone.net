<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="AnonymIntro.ascx.cs"
    Inherits="OliWeb.Controls.Floor.AnonymIntro" %>
<%@ Register TagPrefix="uc1" TagName="ZufallsPostItmitTopLab" Src="~/Controls/Gimicks/ZufallsPostItmitTopLab.ascx" %>
<asp:Image ID="Image1" runat="server" ImageAlign="Right" ImageUrl="../../images/icons/muss_bin.gif"
    ToolTip="muss - bin" Visible="False" meta:resourcekey="Image1Resource1"></asp:Image>
<h1>
    <asp:Literal ID="LieteralH1" Text="OLI-it vermittelt auf geniale Weise&lt;br />Nachrichten zwischen Menschen." runat="server" meta:resourcekey="LiteralH1Resource1" />
</h1>
<h2>
    <asp:Literal ID="LiteralH2" Text="Ein intelligentes Netzwerk individueller Kommunikation."
        runat="server" meta:resourcekey="LiteralH2Resource1" /></h2>
<p>
    <asp:Literal ID="LiteralPara1" Text="Man kann Fragen stellen, Kleinanzeigen aufgeben oder Partner suchen. Privat, 
					beruflich und professionell. Ganz allgemein und sehr speziell." runat="server"
        meta:resourcekey="LiteralPara1Resource1" /></p>
<p>
    <asp:Literal ID="LiteralPara2" Text=" Die Urheber einer Nachricht beschreiben sich, den Inhalt der Nachricht (Motivation
                    &amp; Thema) und die Eigenschaften der gewünschten Empfänger. In einem hierarchischen
                    Wortraum mit bunten Punkten." runat="server" meta:resourcekey="LiteralPara2Resource1" /></p>
<asp:Panel runat="server" ID="PanelRandomPostIt" Style="text-align: center;" CssClass="RandomPostIt" >
    <asp:Label Style="font-size: 0.7em; font-weight: bold;" ID="LiteralRandomMessage"
        Text=" Zufallsnachricht mit	Antworten" runat="server" meta:resourcekey="LiteralRandomMessageResource1" />
    &nbsp;
    <asp:Button Text="next >" runat="server" OnClick="Unnamed1_Click" />
    <br />
    <uc1:ZufallsPostItmitTopLab ID="ZufallsPostItmitTopLab1" runat="server"></uc1:ZufallsPostItmitTopLab>
    <br />
</asp:Panel>
<asp:Panel ID="HilfePanel" runat="server" CssClass="rechtsobenrund Hilfe" meta:resourcekey="HilfePanelResource1">
    <span style="text-align: right"><a title="online dokumentation" href="https://doku.oli-it.com/Oberflaeche/Oberflaeche.htm"
        target="doku">
        <img style="border: 0;"
            height="42" alt="hilfe" src="images/icons/fragezeichen.gif" width="42" align="right"
            name="Hilfe" /></a></span>
    <asp:Localize ID="LocalizeHelp" runat="server" meta:resourcekey="LocalizeHelpResource1"></asp:Localize>
</asp:Panel>
