<%@ Register TagPrefix="uc1" TagName="KlickBild" Src="~/Controls/Gimicks/KlickBild.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopLabTopLabGrid" Src="~/Controls/Koerper/ViewGrids/TopLabTopLabGrid.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="TopLabOrgan.ascx.cs"
    Inherits="OliWeb.Controls.Koerper.Organ.TopLabOrgan" %>
<%@ Register TagPrefix="uc1" TagName="BildPicker" Src="~/Controls/Gimicks/BildPicker.ascx" %>
<div class="TopLab" id="TopLabOrganTable">
    <div class="meta">
        <asp:Label ID="DatumLabel" runat="server" CssClass="datum">Label</asp:Label><br />
        <asp:Label ID="LohnLabel" runat="server" ToolTip="Der Lohn für diese Antwort" CssClass="flowKooK"></asp:Label>
        <asp:Literal ID="UrlLiteral" runat="server"></asp:Literal>
    </div>
    <asp:LinkButton ID="TopImageButton" runat="server" ToolTip="Übergeordnete Antwort zeigen"
        CssClass="PostItButton" OnClick="TopImageButton_Click">
        <asp:Image runat="server" ImageUrl="~/images/icons/Symbole/TopLab.png" />
        <asp:Label ID="LabelTopTopLab" runat="server"> parent</asp:Label></asp:LinkButton>
    <h2>
        <span style="float: left">
            <uc1:KlickBild ID="KlickBild1" runat="server"></uc1:KlickBild>
        </span>
        <asp:Label ID="TitelLabel" runat="server" Font-Bold="True">Label</asp:Label></h2>
    <asp:Label ID="TopLabLabel" runat="server"></asp:Label>
    <uc1:TopLabTopLabGrid ID="TopLabTopLabGrid1" runat="server"></uc1:TopLabTopLabGrid>
</div>
