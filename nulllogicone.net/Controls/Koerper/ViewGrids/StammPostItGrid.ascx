<%@ Register TagPrefix="uc1" TagName="CloseHyperLink" Src="../../Command/GetCommand/SpecialCommand/CloseHyperLink.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Legende" Src="../../../Sites/Elemente/Legende.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="StammPostItGrid.ascx.cs" Inherits="OliWeb.Controls.Koerper.ViewGrids.StammPostItGrid" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="True"%>
<div class="rechtsuntenrund">
	<uc1:closehyperlink id="CloseHyperLink1" runat="server" navigateurl="~/Sites/StammSite.aspx" tooltip="meine Nachrichten schließen"></uc1:closehyperlink>
	<h3><asp:label id="TitleLabel" runat="server"></asp:label></h3>
</div>
<asp:datagrid id="PostItDataGrid" runat="server" pagesize="5" allowpaging="True" datakeyfield="PostItGuid"
	cellspacing="3" gridlines="None" autogeneratecolumns="False" allowsorting="True" width="600px">
	<HeaderStyle Font-Bold="True" CssClass="TableHead"></HeaderStyle>
	<Columns>
		<asp:TemplateColumn SortExpression="AnzS" HeaderText="&lt;img border='0' alt='Urheber' width='40' src='../images/icons/Symbole/Stamm.jpg'&gt;">
			<ItemTemplate>
				<a class="stamm" style="text-align:center; display:block;"
						href='<%# OliWeb.Klassen.Helper.MakeBaseLink() + 
						"Sites/PostItStammSite.aspx?pguid=" + 
						DataBinder.Eval(Container.DataItem, "PostItGuid") %>'>
					<%# DataBinder.Eval(Container, "DataItem.anzS") %>
				</a>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="Datum" HeaderText="erstellt">
			<ItemTemplate>
				<asp:Label id="Label4" runat="server" CssClass="Datum" Text='<%# OliEngine.OliUtil.MakeDateTimeDiff(DataBinder.Eval(Container, "DataItem.Datum").ToString()) %>'>
				</asp:Label><br>
				<%# OliEngine.OliUtil.MakeLinkHtml(HttpUtility.HtmlDecode(DataBinder.Eval(Container, "DataItem.URL").ToString())) %>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="bezahlt" HeaderText="zahlt">
			<ItemTemplate>
				<asp:Label CssClass="flowKooK" runat="server" Text='<%# OliEngine.OliUtil.MakeRedKook(DataBinder.Eval(Container, "DataItem.bezahlt").ToString() ) %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="PostIt" HeaderText="Q.P">
			<ItemStyle CssClass="PostIt"></ItemStyle>
			<ItemTemplate>
				<a href='<%# OliWeb.Klassen.Helper.MakeBaseLink() + "Sites/PostItSite.aspx?pguid=" + DataBinder.Eval(Container.DataItem, "PostItGuid") %>'>
					<%# OliEngine.OliUtil.MakeImageHtml(DataBinder.Eval(Container, "DataItem.Datei").ToString(),50) %>
					<b>
						<%# DataBinder.Eval(Container, "DataItem.Titel") %>
					</b>
					<%# OliEngine.OliUtil.FirstXWords (DataBinder.Eval(Container.DataItem, "PostIt").ToString(),20) %>
				</a>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="Frist" HeaderText="Frist">
			<ItemTemplate>
				<asp:Label id="Label1" runat="server" Text='<%# OliEngine.OliUtil.MakeDateTimeDiff(DataBinder.Eval(Container, "DataItem.Frist").ToString()) %>' CssClass="datum">
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="KooK" HeaderText="kook">
			<ItemTemplate>
				<asp:Label id="KooKLabel" CssClass="flowKooK" runat="server" Text='<%# OliEngine.OliUtil.MakeRedKook(DataBinder.Eval(Container, "DataItem.KooK").ToString()) %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="AnzA" HeaderText="&lt;img border='0' alt='Empf&#228;nger' width='40' src='../images/icons/Symbole/Angler.jpg'&gt;">
			<ItemStyle CssClass="angler"></ItemStyle>
			<ItemTemplate>
				<a class="angler" style="text-align:center; display:block;"
						href='<%# OliWeb.Klassen.Helper.MakeBaseLink() + 
						"Sites/PostItAnglerSite.aspx?pguid=" + 
						DataBinder.Eval(Container.DataItem, "PostItGuid") %>'>
					<%# DataBinder.Eval(Container, "DataItem.anzA") %>
				</a>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="AnzT" HeaderText="&lt;img border='0' alt='Antworten' width='40' src='../images/icons/Symbole/TopLab.jpg'&gt;">
			<ItemStyle CssClass="TopLab"></ItemStyle>
			<ItemTemplate>
				<a 	style="text-align:center; display:block;"
						href='<%# OliWeb.Klassen.Helper.MakeBaseLink() + 
						"Sites/PostItTopLabSite.aspx?pguid=" + 
						DataBinder.Eval(Container.DataItem, "PostItGuid") %>'>
					<%# DataBinder.Eval(Container, "DataItem.anzT") %>
				</a>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
	<PagerStyle NextPageText="weiter" PrevPageText="zur&#252;ck" Position="TopAndBottom" PageButtonCount="20"
		CssClass="pager" Mode="NumericPages"></PagerStyle>
</asp:datagrid>
<uc1:legende id="Legende1" runat="server"></uc1:legende>
