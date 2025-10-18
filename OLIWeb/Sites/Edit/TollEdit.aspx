<%@ Page ValidateRequest="false" Language="c#" CodeBehind="TollEdit.aspx.cs" AutoEventWireup="True"
    Inherits="OliWeb.Sites.Edit.TollEdit" MasterPageFile="~/Site.Master" %>

<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="../../Controls/Koerper/StammKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopLabKoerper" Src="../../Controls/Koerper/TopLabKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TollisOrgan" Src="../../Controls/Koerper/Organ/TollisOrgan.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PostItKoerper" Src="../../Controls/Koerper/PostItKoerper.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>TollisEdit</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
        <uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper>
        <uc1:PostItKoerper ID="PostItKoerper1" runat="server"></uc1:PostItKoerper>
        <uc1:TopLabKoerper ID="TopLabKoerper1" runat="server"></uc1:TopLabKoerper>
        <div class="rechtsobenrund">
         <uc1:TollisOrgan ID="TollisOrgan1" runat="server"></uc1:TollisOrgan>
         </div>
    <asp:Panel ID="HilfePanel" runat="server" CssClass="rechtsobenrund Hilfe" EnableViewState="False"
        Width="600">
        <div>
            <span style="text-align: right"><a title="online dokumentation" href="#" target="doku">
                <img class="fragezeichen" style="border-right: 0px; border-top: 0px; border-left: 0px;
                    border-bottom: 0px" src="../../images/icons/fragezeichen.gif" align="right" alt="weitere Hilfe">
            </a></span>
            <h2>
                Bewerten</h2>
            <p>
                Geben sie einen Prozentwert und ein kurzes Kommentar ab - wie ihnen die Antwort
                gefallen hat</p>
        </div>
    </asp:Panel>
</asp:Content>
