<%@ Page Language="c#" CodeBehind="CodeSite.aspx.cs" MasterPageFile="~/Site.Master" AutoEventWireup="True" Inherits="OliWeb.Sites.CodeSite"
    SmartNavigation="False" %>


<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="../Controls/Koerper/StammKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PostItKoerper" Src="../Controls/Koerper/PostItKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CodeKoerper" Src="../Controls/Koerper/CodeKoerper.ascx" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>CodeSite</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
                <uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper>
                <uc1:PostItKoerper ID="PostItKoerper1" runat="server"></uc1:PostItKoerper>
                <uc1:CodeKoerper ID="CodeKoerper1" runat="server"></uc1:CodeKoerper>
                <asp:Panel ID="HilfePanel" runat="server" CssClass="rechtsobenrund Hilfe">
                    <h4>
                        <span style="text-align: right"><a title="online dokumentation" href="http://doku.oli-it.com/Oberflaeche/Nachricht.htm#PostItCode"
                            target="doku">
                            <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                alt="weitere Hilfe" src="../images/icons/fragezeichen.gif" align="right">
                        </a></span>Code
                        <asp:HyperLink ID="RdfHyperLink" runat="server" ImageUrl="~/images/rdf_klein.jpg"
                            ToolTip="RDF Metadaten zu diesem Code"></asp:HyperLink></h4>
                    <p>
                        Die Markierung einer Nachricht.</p>
                    <p>
                        Der Urheber beschreibt sich (freiwillig) und markiert die Kategorien und Inhalte
                        der Nachricht.
                        <br />
                        Dann kann er die gewünschten Eigenschaften der Empfänger seiner Nachricht beschreiben.</p>
                    <h4>
                        ShortCuts</h4>
                    <p>
                        Sind Favoritenmarkierungen, die häufig gebraucht werden. Einfach einen neuen erstellen
                        oder bestehenden selektieren, dann kann er auf den aktiven Code kopiert werden.</p>
                    <h4>
                        Wortraum</h4>
                    <p>
                        Jeder Klick gilt!
                        <br />
                        Wenn man eingeloggt ist, kann man Markierungen löschen&nbsp;[x] (bitte von 'unten
                        nach oben') und neu erstellen.<br />
                        Jede Verzweigung in ein Netz und aus einem Baum&nbsp;wird gespeichert. (mit bunten
                        Punkten)</p>
                    <h4>
                        Senden</h4>
                    <p>
                        Wenn der Code fertig erstellt ist, muß er gegen alle Filterprofile abgeglichen werden.
                        Das ist die Funktion 'Filtern' mit den bunten Punkten.</p>
                    <h4>
                        RDF</h4>
                    <p>
                        stellt die RDF Repräsentation der Markierung als Datei zur Verfügung</p>
                </asp:Panel>
           
</asp:Content>