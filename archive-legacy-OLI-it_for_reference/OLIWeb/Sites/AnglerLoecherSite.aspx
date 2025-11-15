<%@ Page Language="c#" CodeBehind="AnglerLoecherSite.aspx.cs" MasterPageFile="~/Site.Master"
    AutoEventWireup="True" Inherits="OliWeb.Sites.AnglerLoecherSite" SmartNavigation="False" %>

<%@ Register TagPrefix="uc1" TagName="WortraumController" Src="../Controls/Wortraum/WortraumController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AjaxWortraumControl" Src="../Controls/AjaxWortraum/AjaxWortraumControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AjaxWortraumControlFlip" Src="../Controls/AjaxWortraum/AjaxWortraumControlFlip.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AnglerKoerper" Src="~/Controls/Koerper/AnglerKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="~/Controls/Koerper/StammKoerper.ascx" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>OLI-it:<% = MyTitle %></title>
    <meta name="keywords" content="angler, experte, löcher, filter, antworter, supporter" />
    <meta name="Description" content="Filterprofil eines Stammes" />
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper>
    <uc1:AnglerKoerper ID="AnglerKoerper1" runat="server"></uc1:AnglerKoerper>
    <asp:Panel ID="FischenPanel" runat="server">
    
            <asp:LinkButton ID="FischenImageButton" runat="server" CssClass="MatchButton"
                ToolTip="nach Nachrichten filtern" OnClick="FischenImageButton_Click">
                <asp:Image ImageUrl="~/images/Ringe_trans.gif" runat="server" /> match
            </asp:LinkButton>
            
     
    </asp:Panel>
    <div style="clear:left;">
    <uc1:WortraumController ID="WortraumController1" runat="server"></uc1:WortraumController>
    <uc1:AjaxWortraumControlFlip ID="AjaxWortraumControlFlip1" runat="server"></uc1:AjaxWortraumControlFlip></div>
    <br />
    <asp:Panel ID="HilfePanel" runat="server" CssClass="rechtsobenrund Hilfe">
        <span style="text-align: right"><a title="online dokumentation" href="http://doku.oli-it.com/Oberflaeche/Angler.htm"
            target="doku">
            <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                alt="weitere Hilfe" src="../images/icons/fragezeichen.gif" align="right">
        </a></span>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<h4>
            Filterprofil&nbsp;
            <asp:HyperLink ID="RdfHyperLink" runat="server" ToolTip="RDF Metadaten zu diesem Filterprofil"
                ImageUrl="~/images/rdf_klein.jpg"></asp:HyperLink></h4>
        <p>
            Das Filterprofil, um Nachrichten zu empfangen.
        </p>
        <p>
            Hier markiert der Empfänger komplementär zu den Codes, welche Nachrichten er erhalten
            möchte. Man kann das Filterprofil jederzeit anpassen - die Treffer sind temporär
            und dynamisch. Nach Änderungen muss man auf die bunten Punkte klicken um nach neuen
            Nachrichten zu <strong>filtern</strong>.</p>
    </asp:Panel>
</asp:Content>
