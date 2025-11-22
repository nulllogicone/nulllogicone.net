<%@ Register TagPrefix="uc1" TagName="CloseHyperLink" Src="../../Command/GetCommand/SpecialCommand/CloseHyperLink.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="PostItAnglerGrid.ascx.cs"
    Inherits="OliWeb.Controls.Koerper.ViewGrids.PostItAnglerGrid" %>
<%@ Import Namespace="OliEngine" %>
<div class="rechtsuntenrund">
    <div align="right">
        <uc1:CloseHyperLink ID="CloseHyperLink1" runat="server" NavigateUrl="~/Sites/PostItSite.aspx"
            ToolTip="Empfänger schließen"></uc1:CloseHyperLink>
    </div>
<h3>
    <asp:Label ID="TitleLabel" runat="server"></asp:Label></h3>
</div>
<asp:DataGrid ID="PostItDataGrid" runat="server" AutoGenerateColumns="False" AllowSorting="True" EnableViewState="True"
    CellSpacing="3" GridLines="None" DataKeyField="StammGuid" PageSize="5" AllowPaging="True" OnPageIndexChanged="PostItDataGrid_PageIndexChanged">
    <HeaderStyle CssClass="TableHead"></HeaderStyle>
    <Columns>
        <asp:TemplateColumn SortExpression="Stamm" HeaderText="Q.S">
            <ItemTemplate>
                <a class="Stamm" runat="server" href='<%# "~/S/" + Eval("StammGuid") + ".aspx" %>'>
                    <%# OliUtil.MakeImageHtml(Eval("SDatei").ToString(), 30) %>
                    <%# Eval("Stamm") %>
                </a>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn SortExpression="Angler" HeaderText="Q.A">
            <ItemStyle Width="80%" VerticalAlign="Top"></ItemStyle>
            <ItemTemplate>
                <a class="Angler" href='<%# "~/Sites/AnglerPSite.aspx?aguid=" + Eval("AnglerGuid") %>' runat="server">
                    <%# Eval("Angler") %>
                    <span class="id">
                        <%# Eval("Beschreibung") %>
                    </span></a>
            </ItemTemplate>
        </asp:TemplateColumn>
    </Columns>
    <PagerStyle PageButtonCount="20" CssClass="pager" Mode="NumericPages"></PagerStyle>
</asp:DataGrid>
