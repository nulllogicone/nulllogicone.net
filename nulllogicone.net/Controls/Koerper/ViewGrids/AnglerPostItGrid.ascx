<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AnglerPostItGrid.ascx.cs" Inherits="OliWeb.Controls.Koerper.ViewGrids.AnglerPostItGrid" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="CloseHyperLink" Src="../../Command/GetCommand/SpecialCommand/CloseHyperLink.ascx" %>
<div class=rechtsuntenrund>
	<div align=right><uc1:closehyperlink id=CloseHyperLink1 ToolTip="meine Fische schließen" NavigateUrl="~/Sites/AnglerSite.aspx"
			runat="server"></uc1:closehyperlink></div>
	<h3><asp:label id=TitleLabel runat="server"></asp:label></h3>
</div>
<asp:datagrid id=AnglerDataGrid runat="server" AutoGenerateColumns="False" AllowSorting="True"
	CellSpacing="3" GridLines="None" DataKeyField="PostItGuid" width="100%" PageSize="5" BackColor="#E0E0E0">
	<ItemStyle BackColor="White"></ItemStyle>
	<HeaderStyle CssClass="TableHead"></HeaderStyle>
	<Columns>
		<asp:TemplateColumn SortExpression="anzS" HeaderText="&lt;img border=0 alt='Anzahl Urheber' width=40 src=../images/icons/Symbole/Stamm.jpg&gt;">
			<ItemStyle HorizontalAlign="Center"></ItemStyle>
			<ItemTemplate>
				<a class="stamm"
					href='<%# "http://www.oli-it.com/Sites/PostItStammSite.aspx?pguid=" + 
					DataBinder.Eval(Container.DataItem, "PostItGuid") %>' 
					title="zeigt die Nachricht mit Urhebern">
					<%# DataBinder.Eval(Container, "DataItem.anzS") %>
				</a>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="PostIt" HeaderText="Q.P">
			<ItemStyle CssClass="PostIt"></ItemStyle>
			<ItemTemplate>
				<a href='<%# "http://nulllogicone.net/PostIt/?" + 
					DataBinder.Eval(Container.DataItem, "PostItGuid") %>' 
					title="zeigt die Instanzansicht der Nachricht an">
					<%# OliEngine.OliUtil.MakeImageHtml(DataBinder.Eval(Container, "DataItem.Datei").ToString(),50) %>
					<b>
						<%# DataBinder.Eval(Container, "DataItem.Titel") %>
					</b>
					<%# OliEngine.OliUtil.MakeHtmlLineBreak(OliEngine.OliUtil.FirstXWords(DataBinder.Eval(Container, "DataItem.PostIt").ToString(),30)) %>
				</a>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="Datum" HeaderText="erstellt">
			<ItemTemplate>
				<asp:Label id="Label5" runat="server" Text='<%# OliEngine.OliUtil.MakeDateTimeDiff(DataBinder.Eval(Container, "DataItem.Datum").ToString()) %>' CssClass="Datum">
				</asp:Label><br>
				<%# OliEngine.OliUtil.MakeLinkHtml(HttpUtility.HtmlDecode(DataBinder.Eval(Container, "DataItem.URL").ToString())) %>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="KooK" HeaderText="kook">
			<ItemTemplate>
				<asp:Label id="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.KooK", "{0:0.00}" ) %>' CssClass="flowKooK">
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="AnzA" HeaderText="&lt;img border=0 alt='Anzahl Empf&#228;nger' width=40 src=../images/icons/Symbole/Angler.jpg&gt;">
			<ItemStyle HorizontalAlign="Center"></ItemStyle>
			<ItemTemplate>
				<a class="angler"
					href='<%# "http://www.oli-it.com/Sites/PostItAnglerSite.aspx?pguid=" + 
					DataBinder.Eval(Container.DataItem, "PostItGuid") %>' 
					title="zeigt die Nachricht mit ihren Empfängern">
					<%# DataBinder.Eval(Container, "DataItem.AnzA") %>
				</a>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="AnzT" HeaderText="&lt;img border=0 alt='Anzahl Antworten' width=40 src=../images/icons/Symbole/TopLab.jpg&gt;">
			<ItemStyle HorizontalAlign="Center" CssClass="TopLab"></ItemStyle>
			<ItemTemplate>
				<a 	href='<%# "http://www.oli-it.com/Sites/PostItTopLabSite.aspx?pguid=" + 
					DataBinder.Eval(Container.DataItem, "PostItGuid") %>' 
					title="zeigt die Nachricht mit ihren Antworten">
					<%# DataBinder.Eval(Container, "DataItem.AnzT") %>
				</a>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False">
			<ItemTemplate>
				<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CodeGuid") %>' ID="CodeGuidLabel" Visible="False">
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="PostItZust">
			<ItemTemplate>
				<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PostItZust") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
	<PagerStyle PageButtonCount="20" CssClass="pager" Mode="NumericPages"></PagerStyle>
</asp:datagrid>
