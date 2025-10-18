<%@ Register TagPrefix="uc1" TagName="KlickBild" Src="~/Controls/Gimicks/KlickBild.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="StammOrgan.ascx.cs"
    Inherits="OliWeb.Controls.Koerper.Organ.StammOrgan" %>
<div class="StammOrgan" typeof="nlos:Stamm" resource="nlo:Stamm/?<%= StammGuid %>">
      <meta  property="nlos:name" content="<%= StammName %>"/>
    <meta  property="nlos:stammGuid" content="<%= StammGuid %>"/>
    <div class="meta">
        <asp:Label ID="DatumLabel" runat="server" CssClass="datum" property="nlos:datum"></asp:Label>
        <br />
        <asp:Label ID="KooKLabel" runat="server" CssClass="boundKooK" ToolTip="KooK das Vermögen des Stamm" property="nlos:boundKook"></asp:Label>
        <br />
        <asp:HyperLink ID="MailHyperLink" runat="server" NavigateUrl="~/Sites/Elemente/MailSchreiben.aspx"
            ImageUrl="~/images/mailme.gif" ToolTip="diesem Stamm eine persönliche Nachricht schreiben"
            Visible="False">HyperLink</asp:HyperLink>
    </div>
    <div>
        <h1 class="Stamm" style="padding: 0.5em;">
            <span style="float: left;">
                <uc1:KlickBild ID="KlickBild1" runat="server" Breite="48"></uc1:KlickBild>
            </span>
          
            <asp:HyperLink ID="StammHyperLink" runat="server" NavigateUrl="#"></asp:HyperLink></h1>
        <asp:Label ID="BeschreibungLabel" runat="server" CssClass="beschreibung" property="nlos:beschreibung"></asp:Label><br />
        <asp:HyperLink ID="LinkHyperLink" runat="server" Font-Size="Smaller"></asp:HyperLink>
    </div>
</div>
