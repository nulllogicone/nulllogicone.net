<%@ Control Language="c#" AutoEventWireup="false" Codebehind="InboxGrid.ascx.cs" Inherits="OliWeb.Controls.Koerper.ViewGrids.InboxGrid" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="CloseHyperLink" Src="../../Command/GetCommand/SpecialCommand/CloseHyperLink.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TollChart" Src="~/Controls/Gimicks/TollChart.ascx" %>
<div class="rechtsuntenrund">
	<uc1:CloseHyperLink id="CloseHyperLink1" runat="server" NavigateUrl="~/Sites/StammSite.aspx" ToolTip="meine Nachrichten schließen"></uc1:CloseHyperLink>
	<h3><asp:Label id="TitleLabel" runat="server"></asp:Label></h3></div>
<asp:datagrid id="InboxDataGrid" AutoGenerateColumns="False" AllowSorting="True" runat="server"
	DataKeyField="TopLabGuid" CellSpacing="3" borderStyle="None" borderwidth="0px" width="600px">
	<HeaderStyle CssClass="TableHead"></HeaderStyle>
	<Columns>
		<asp:TemplateColumn SortExpression="datum" HeaderText="Datum">
			<ItemTemplate>
				<asp:Label id=Label3 runat="server" Text='<%# OliEngine.OliUtil.MakeDateTimeDiff(DataBinder.Eval(Container, "DataItem.Datum").ToString()) %>' CssClass="datum">
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="PostIt" HeaderText="Q.P">
			<ItemStyle CssClass="PostIt"></ItemStyle>
			<ItemTemplate>
				<asp:LinkButton id="LinkButton2" runat="server">
					<%# OliEngine.OliUtil.MakeImageHtml(DataBinder.Eval(Container, "DataItem.PDatei").ToString(),30) %>
					<b>
						<%# DataBinder.Eval(Container, "DataItem.PTitel") %>
					</b>
					<%# OliEngine.OliUtil.FirstXWords (DataBinder.Eval(Container, "DataItem.PostIt").ToString(), 15) %>
				</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="TopLab" HeaderText="Q.T">
			<ItemStyle CssClass="TopLab"></ItemStyle>
			<ItemTemplate>
				<asp:LinkButton id="LinkButton1" runat="server">
					<%# OliEngine.OliUtil.MakeImageHtml(DataBinder.Eval(Container, "DataItem.TDatei").ToString(),30) %>
					<b>
						<%# DataBinder.Eval(Container, "DataItem.TTitel") %>
					</b>
					<%# DataBinder.Eval(Container, "DataItem.TopLab") %>
				</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="DurchToll" HeaderText="Toll">
			<ItemTemplate>
				<uc1:TollChart id="Tollchart2" runat="server" TollWert='<%# DataBinder.Eval(Container.DataItem,"DurchToll").ToString().Length > 0 ? int.Parse(DataBinder.Eval(Container.DataItem,"DurchToll").ToString()) : -1 %>' Breite='50' Hoehe='10'>
				</uc1:TollChart>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="gelesen">
			<ItemStyle HorizontalAlign="center"></ItemStyle>
			<ItemTemplate>
				<asp:Button id="Button1" runat="server" Text="x" CommandName="gelesen" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.InboxGuid") %>'>
				</asp:Button>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
</asp:datagrid>
