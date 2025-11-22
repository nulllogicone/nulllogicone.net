<%@ Register TagPrefix="uc1" TagName="KlickBild" Src="~/Controls/Gimicks/KlickBild.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="PostItOrgan.ascx.cs"
    Inherits="OliWeb.Controls.Koerper.Organ.PostItOrgan" %>
<div class="PostIt" id="PostItOrganTable" typeof="nlos:PostIt" resource="nlo:PostIt/?<%= PostItGuid %>">
    <meta property="nlos:titel" content="<%= PostItTitel %>" />
    <meta property="nlos:postItGuid" content="<%= PostItGuid %>" />
    <div class="meta">
        <asp:Label ID="DatumLabel" runat="server" CssClass="datum" ToolTip="Nachricht erstellt am" property="nlos:datum"></asp:Label><br />
        <asp:Label ID="KooKLabel" runat="server" CssClass="flowKooK" ToolTip="kOOk - Der Wert der Nachricht" property="nlos:flowKook"></asp:Label><br />
        <div style="float: left;">
            <asp:Literal ID="UrlLiteral" runat="server"></asp:Literal>
        </div>
        <asp:Label ID="HitsLabel" CssClass="hits" runat="server" ToolTip="Hits"></asp:Label><br />

        <asp:HyperLink ID="HyperLinkSinglePostIt" runat="server" Text="[*]" />
    </div>
    <h2>
        <span style="float: left; margin-right: 0.5em;">
            <uc1:KlickBild ID="KlickBild1" runat="server"></uc1:KlickBild>
        </span>
        <asp:Label ID="TitelLabel" runat="server"></asp:Label></h2>
    <asp:HyperLink ID="PostItHyperLink" runat="server" NavigateUrl="~/Sites/PostItSite.aspx"></asp:HyperLink>
    <%-- <iframe id="UriIframe" runat="server"></iframe>--%>
    <asp:PlaceHolder runat="server" ID="PlaceHolderIFrame" />
</div>
