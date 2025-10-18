<%@ Control Language="c#" AutoEventWireup="false" Codebehind="PostItTopLabGrid.ascx.cs" Inherits="OliWeb.Controls.Koerper.ViewGrids.PostItTopLabGrid" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="CloseHyperLink" Src="../../Command/GetCommand/SpecialCommand/CloseHyperLink.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopLabTopLabGrid" Src="TopLabTopLabGrid.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TollChart" Src="~/Controls/Gimicks/TollChart.ascx" %>
<div class="rechtsuntenrund">
	<uc1:CloseHyperLink id="CloseHyperLink1" runat="server" NavigateUrl="~/Sites/PostItSite.aspx" ToolTip="Antworten schließen"></uc1:CloseHyperLink>
	<h3><asp:Label id="TitleLabel" runat="server"></asp:Label></h3></div>
<asp:DataGrid id="PostItDataGrid" runat="server" AutoGenerateColumns="False" AllowSorting="True"
	CellSpacing="3" GridLines="None" DataKeyField="TopLabGuid" width="600px" PageSize="5" AllowPaging="True">
	<HeaderStyle CssClass="TableHead"></HeaderStyle>
	<Columns>
		<asp:TemplateColumn SortExpression="Stamm" HeaderText="Q.S">
			<ItemTemplate>
				<a class="stamm" href='<%# OliWeb.Klassen.Helper.MakeBaseLink() + "Sites/PostItTopLabSite.aspx?sguid=" + DataBinder.Eval(Container.DataItem, "StammGuid")  %>'>
					<%# OliEngine.OliUtil.MakeImageHtml(DataBinder.Eval(Container, "DataItem.sdatei").ToString(),50) %>
					<%# DataBinder.Eval(Container.DataItem, "Stamm") %>
				</a>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="TopLab" HeaderText="Q.T">
			<ItemStyle CssClass="topLab"></ItemStyle>
			<ItemTemplate>
				<a href='<%# OliWeb.Klassen.Helper.MakeBaseLink() + "T/" + DataBinder.Eval(Container.DataItem, "TopLabGuid") + ".aspx" %>'>
					<%# OliEngine.OliUtil.MakeImageHtml(DataBinder.Eval(Container, "DataItem.tdatei").ToString(),50) %>
					<b>
						<%# DataBinder.Eval(Container, "DataItem.Titel") %>
					</b>
					<%# DataBinder.Eval(Container.DataItem, "TopLab") %>
				</a>
				<uc1:TopLabTopLabGrid id="TopLabTopLabGrid1" runat="server" ParentTopLabGuidString='<%# DataBinder.Eval(Container.DataItem,"TopLabGuid") %>'>
				</uc1:TopLabTopLabGrid>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="Lohn" HeaderText="Lohn">
			<ItemTemplate>
				<asp:Label id=Label4 runat="server" Text='<%# OliEngine.OliUtil.MakeRedKook(DataBinder.Eval(Container, "DataItem.Lohn").ToString() ) %>' CssClass="flowKooK">
				</asp:Label><br>
				<%# OliEngine.OliUtil.MakeLinkHtml(HttpUtility.HtmlDecode(DataBinder.Eval(Container, "DataItem.TURL").ToString())) %>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="DurchToll" HeaderText="Toll">
			<ItemTemplate>
				<uc1:TollChart id="Tollchart2" runat="server" TollWert='<%# DataBinder.Eval(Container.DataItem,"DurchToll").ToString().Length > 0 ? int.Parse(DataBinder.Eval(Container.DataItem,"DurchToll").ToString()) : -1 %>' Breite='50' Hoehe='10'>
				</uc1:TollChart>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False">
			<ItemTemplate>
				<asp:Label ID="StammGuidLabel" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"StammGuid") %>'>
				</asp:Label>
				<asp:Label ID="TopLabGuidLabel" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TopLabGuid") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
	<PagerStyle Position="TopAndBottom" PageButtonCount="20" CssClass="pager" Mode="NumericPages"></PagerStyle>
</asp:DataGrid>
