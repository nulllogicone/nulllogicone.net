<%@ Control Language="c#" AutoEventWireup="false" Codebehind="NewsGrid.ascx.cs" Inherits="OliWeb.Controls.Koerper.ViewGrids.NewsGrid" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="CloseHyperLink" Src="../../Command/GetCommand/SpecialCommand/CloseHyperLink.ascx" %>
<LINK href="../../OliWeb.css" type="text/css" rel="stylesheet">
<div class="rechtsuntenrund">
	<uc1:CloseHyperLink id="CloseHyperLink1" runat="server" NavigateUrl="~/Sites/StammSite.aspx" ToolTip="meine Nachrichten schließen"></uc1:CloseHyperLink>
	<h3><asp:Label id="TitleLabel" runat="server"></asp:Label></h3></div>
<asp:DataGrid id="NewsDataGrid" runat="server" AutoGenerateColumns="False" AllowSorting="True"
	DataKeyField="PostItGuid" CellSpacing="3" borderStyle="None" borderwidth="0px" PageSize="5"
	AllowPaging="True" width="600px">
	<HeaderStyle CssClass="TableHead"></HeaderStyle>
	<Columns>
		<asp:TemplateColumn SortExpression="Angler" HeaderText="Q.A">
			<ItemTemplate>
				<asp:Label id=AnglerGuidLabel runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.AnglerGuid") %>'>
				</asp:Label>
				<asp:LinkButton id=AnglerLinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Angler") %>' CommandName="AnglerLinkButton" CssClass="Angler">
				</asp:LinkButton><BR>
				<asp:Button id="AlleNewsGelesenButton" runat="server" Visible="False" Text="alles gelesen" CommandName="AlleNewsGelesen"
					Font-Size="8"></asp:Button>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="Datum" HeaderText="gefischt">
			<ItemTemplate>
				<asp:Label id=Label2 runat="server" Text='<%# OliEngine.OliUtil.MakeDateTimeDiff(DataBinder.Eval(Container, "DataItem.Datum").ToString()) %>' CssClass="datum">
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="PostIt" HeaderText="Q.P">
			<ItemStyle CssClass="PostIt"></ItemStyle>
			<ItemTemplate>
				<asp:LinkButton id="LinkButton1" runat="server">
					<%# OliEngine.OliUtil.MakeImageHtml(DataBinder.Eval(Container, "DataItem.Datei").ToString(),40) %>
					<b>
						<%# DataBinder.Eval(Container, "DataItem.Titel") %>
					</b>
					<%# OliEngine.OliUtil.MakeHtmlLineBreak(OliEngine.OliUtil.FirstXWords(DataBinder.Eval(Container, "DataItem.PostIt").ToString(),20)) %>
				</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="KooK" HeaderText="KooK">
			<ItemTemplate>
				<asp:Label id="Label3" runat="server" Text='<%# OliEngine.OliUtil.MakeRedKook(DataBinder.Eval(Container, "DataItem.KooK").ToString()) %>' CssClass="flowKooK">
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="AnzT" HeaderText="&lt;img border=0 alt=Antworten width=40 src=../images/icons/Symbole/TopLab.jpg&gt;">
			<ItemStyle HorizontalAlign="center" CssClass="TopLab"></ItemStyle>
			<ItemTemplate>
				<asp:LinkButton CommandName="zeigT" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AnzT") %>'>
				</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="PDatum" HeaderText="erstellt">
			<ItemTemplate>
				<asp:Label id="Label1" runat="server" Text='<%# OliEngine.OliUtil.MakeDateTimeDiff(DataBinder.Eval(Container, "DataItem.PDatum").ToString()) %>' CssClass="datum">
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="gelesen">
			<ItemStyle HorizontalAlign="center"></ItemStyle>
			<ItemTemplate>
				<asp:Button id="Button1" runat="server" Text="x" CommandName="gelesen" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.NewsGuid") %>'>
				</asp:Button>
				<asp:Label Runat="server" ID="NewsGuidLabel" Visible="False" Text='<%# DataBinder.Eval(Container.DataItem, "NewsGuid") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
	<PagerStyle Position="TopAndBottom" PageButtonCount="20" CssClass="pager" Mode="NumericPages"></PagerStyle>
</asp:DataGrid>
