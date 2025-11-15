<%@ Control Language="c#" AutoEventWireup="false" Codebehind="PostItStammGrid.ascx.cs" Inherits="OliWeb.Controls.Koerper.ViewGrids.PostItStammGrid" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="CloseHyperLink" Src="../../Command/GetCommand/SpecialCommand/CloseHyperLink.ascx" %>
<div class="rechtsuntenrund">
	<div align="right">
		<uc1:CloseHyperLink id="CloseHyperLink1" runat="server" NavigateUrl="~/Sites/PostItSite.aspx" ToolTip="Urheber schließen"></uc1:CloseHyperLink>
	</div>
	<h3><asp:Label id="TitleLabel" runat="server"></asp:Label></h3></div>
<asp:DataGrid id="PostItDataGrid" runat="server" AutoGenerateColumns="False" AllowSorting="True"
	CellSpacing="3" GridLines="None" DataKeyField="StammGuid" width="600px" PageSize="5" AllowPaging="True">
	<HeaderStyle CssClass="TableHead"></HeaderStyle>
	<Columns>
		<asp:TemplateColumn SortExpression="Stamm" HeaderText="Q.S">
			<ItemTemplate>
				<%# OliEngine.OliUtil.MakeImageHtml(DataBinder.Eval(Container, "DataItem.Datei").ToString(),50) %>
				<a class="stamm" href='<%# OliWeb.Klassen.Helper.MakeBaseLink() + "Sites/PostItSite.aspx?sguid=" + DataBinder.Eval(Container.DataItem, "StammGuid") %>'>
					<%# DataBinder.Eval(Container.DataItem, "Stamm") %>
				</a>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="bezahlt" HeaderText="zahlt">
			<ItemTemplate>
				<asp:Label id=Label3 runat="server" Text='<%# OliEngine.OliUtil.MakeRedKook(DataBinder.Eval(Container, "DataItem.bezahlt").ToString() ) %>' CssClass="flowKooK">
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="Frist" HeaderText="Frist">
			<ItemTemplate>
				<asp:Label id="Label5" runat="server" Text='<%# OliEngine.OliUtil.MakeDateTimeDiff(DataBinder.Eval(Container, "DataItem.Frist").ToString()) %>' CssClass="Datum">
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False">
			<ItemTemplate>
				<asp:Label runat="server" ID="StammZustLabel" Text='<%# DataBinder.Eval(Container, "DataItem.StammZust") %>'>
				</asp:Label>
				<asp:Label runat="server" ID="ClosedLabel" Text='<%# DataBinder.Eval(Container.DataItem, "closed") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
	<PagerStyle Position="TopAndBottom" PageButtonCount="20" CssClass="pager" Mode="NumericPages"></PagerStyle>
</asp:DataGrid>
