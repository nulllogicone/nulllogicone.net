<%@ Register TagPrefix="uc1" TagName="CloseHyperLink" Src="../../Command/GetCommand/SpecialCommand/CloseHyperLink.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="StammTopLabGrid.ascx.cs" Inherits="OliWeb.Controls.Koerper.ViewGrids.StammTopLabGrid" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="TollChart" Src="~/Controls/Gimicks/TollChart.ascx" %>
<DIV class="rechtsuntenrund">
	<uc1:CloseHyperLink id="CloseHyperLink1" runat="server" NavigateUrl="~/Sites/StammSite.aspx" ToolTip="meine Antworten schließen"></uc1:CloseHyperLink>
	<h3><asp:Label id="TitleLabel" runat="server"></asp:Label></h3></DIV>
<asp:DataGrid id="TopLabDataGrid" runat="server" AllowSorting="True" AutoGenerateColumns="False"
	CellSpacing="3" GridLines="None" DataKeyField="PostItGuid" width="600px" PageSize="5" AllowPaging="True">
	<HeaderStyle CssClass="TableHead"></HeaderStyle>
	<Columns>
		<asp:TemplateColumn SortExpression="PDatum" HeaderText="erstellt">
			<ItemTemplate>
				<asp:Label id="Label1" runat="server" Text='<%# OliEngine.OliUtil.MakeDateTimeDiff(DataBinder.Eval(Container, "DataItem.PDatum").ToString()) %>' CssClass="Datum">
				</asp:Label><br>
				<%# OliEngine.OliUtil.MakeLinkHtml(HttpUtility.HtmlDecode(DataBinder.Eval(Container, "DataItem.PURL").ToString())) %>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="PostIt" HeaderText="Q.P">
			<ItemStyle CssClass="PostIt"></ItemStyle>
			<ItemTemplate>
				<a href='<%# OliWeb.Klassen.Helper.MakeBaseLink() + "P/" + DataBinder.Eval(Container.DataItem, "PostItGuid") + ".aspx" %>'>
					<%# OliEngine.OliUtil.MakeImageHtml(DataBinder.Eval(Container, "DataItem.PDatei").ToString(),30) %>
					<b>
						<%# DataBinder.Eval(Container, "DataItem.PTitel") %>
					</b>
					<%# OliEngine.OliUtil.FirstXWords (DataBinder.Eval(Container.DataItem, "PostIt").ToString(),20) %>
				</a>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="TopLab" HeaderText="Q.T">
			<ItemStyle CssClass="TopLab"></ItemStyle>
			<ItemTemplate>
				<a href='<%# OliWeb.Klassen.Helper.MakeBaseLink() + "T/" + DataBinder.Eval(Container.DataItem, "TopLabGuid") + ".aspx" %>'>
					<%# OliEngine.OliUtil.MakeImageHtml(DataBinder.Eval(Container, "DataItem.TDatei").ToString(),50) %>
					<b>
						<%# DataBinder.Eval(Container, "DataItem.TTitel") %>
					</b>
					<%# OliEngine.OliUtil.FirstXWords (DataBinder.Eval(Container.DataItem, "TopLab").ToString(),20) %>
				</a>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="TDatum" HeaderText="erstellt">
			<ItemTemplate>
				<asp:Label id=Label3 runat="server" Text='<%# OliEngine.OliUtil.MakeDateTimeDiff(DataBinder.Eval(Container, "DataItem.TDatum").ToString()) %>' CssClass="Datum">
				</asp:Label><br>
				<%# OliEngine.OliUtil.MakeLinkHtml(HttpUtility.HtmlDecode(DataBinder.Eval(Container, "DataItem.TURL").ToString())) %>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="Lohn" HeaderText="Lohn">
			<ItemTemplate>
				<asp:Label id=Label7 runat="server" Text='<%# OliEngine.OliUtil.MakeRedKook(DataBinder.Eval(Container, "DataItem.Lohn").ToString() ) %>' CssClass="flowKooK">
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="DurchToll" HeaderText="Toll">
			<ItemTemplate>
				<uc1:TollChart id="Tollchart2" runat="server" TollWert='<%# DataBinder.Eval(Container.DataItem,"DurchToll").ToString().Length > 0 ? int.Parse(DataBinder.Eval(Container.DataItem,"DurchToll").ToString()) : -1 %>' Breite='50' Hoehe='10'>
				</uc1:TollChart>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
	<PagerStyle Position="TopAndBottom" PageButtonCount="20" CssClass="pager" Mode="NumericPages"></PagerStyle>
</asp:DataGrid>
