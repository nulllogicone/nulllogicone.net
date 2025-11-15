<%@ Control Language="c#" AutoEventWireup="false" Codebehind="StammAnglerGrid.ascx.cs" Inherits="OliWeb.Controls.Koerper.ViewGrids.StammAnglerGrid" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="CloseHyperLink" Src="../../Command/GetCommand/SpecialCommand/CloseHyperLink.ascx" %>
<div class="rechtsuntenrund">
	<uc1:closehyperlink id="CloseHyperLink1" tooltip="meine Angler schließen" navigateurl="~/Sites/StammSite.aspx"
		runat="server"></uc1:closehyperlink>
	<h3><asp:label id="TitleLabel" runat="server"></asp:label></h3>
</div>
<table id="StammAnglerTable" cellspacing="1" cellpadding="1" border="0">
	<tr>
		<td width="20"></td>
		<td>
			<asp:datagrid id="AnglerDataGrid" runat="server" borderwidth="1px" datakeyfield="AnglerGuid" gridlines="Horizontal"
				autogeneratecolumns="False" allowsorting="True" width="500">
				<itemstyle cssclass="angler"></itemstyle>
				<headerstyle cssclass="TableHead"></headerstyle>
				<columns>
					<asp:templatecolumn sortexpression="Angler" headertext="Q.A">
						<itemtemplate>
							<a class="angler" href='<%# OliWeb.Klassen.Helper.MakeBaseLink() + "A/" + DataBinder.Eval(Container.DataItem, "AnglerGuid") + ".aspx" %>'>
								<%# DataBinder.Eval(Container.DataItem, "Angler") %>
								<span class="klein">
									<%# DataBinder.Eval(Container.DataItem, "Beschreibung") %>
								</span></a>
						</itemtemplate>
					</asp:templatecolumn>
					<asp:templatecolumn sortexpression="AnzP" headertext="Fische">
						<itemstyle cssclass="PostIt"></itemstyle>
						<itemtemplate>
							<a href='<%# OliWeb.Klassen.Helper.MakeBaseLink() + "Sites/AnglerPostItSite.aspx?aguid=" + DataBinder.Eval(Container.DataItem, "AnglerGuid").ToString() %>'>
								<%# DataBinder.Eval(Container, "DataItem.AnzP") %>
							</a>
						</itemtemplate>
					</asp:templatecolumn>
					<asp:templatecolumn sortexpression="SumT" headertext="Wert">
						<itemstyle backcolor="White"></itemstyle>
						<itemtemplate>
							<asp:Label id="Label2" runat="server" Text='<%# OliEngine.OliUtil.MakeRedKook(DataBinder.Eval(Container, "DataItem.SumT").ToString() ) %>' CssClass="flowKooK">
							</asp:Label>
						</itemtemplate>
					</asp:templatecolumn>
					<asp:buttoncolumn visible="False" text="del" buttontype="PushButton" commandname="Delete">
						<itemstyle></itemstyle>
					</asp:buttoncolumn>
				</columns>
			</asp:datagrid>
		</td>
	</tr>
</table>
