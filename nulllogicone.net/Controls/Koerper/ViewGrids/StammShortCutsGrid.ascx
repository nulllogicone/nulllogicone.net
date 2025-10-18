<%@ Control Language="c#" AutoEventWireup="false" Codebehind="StammShortCutsGrid.ascx.cs" Inherits="OliWeb.Controls.Koerper.ViewGrids.StammShortCutsGrid" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="WortraumController" Src="../../Wortraum/WortraumController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CloseHyperLink" Src="../../Command/GetCommand/SpecialCommand/CloseHyperLink.ascx" %>
<div class="rechtsuntenrund"><uc1:closehyperlink id="CloseHyperLink1" tooltip="meine Nachrichten schließen" navigateurl="~/Sites/StammSite.aspx"
		runat="server"></uc1:closehyperlink>
	<h3><asp:label id="TitleLabel" runat="server"></asp:label></h3>
</div>
<asp:datagrid id="ShortCutsDataGrid" runat="server" cellspacing="2" backcolor="#DEBA84" borderstyle="None"
	width="400px" datakeyfield="ShortCutsGuid" cellpadding="3" borderwidth="1px" bordercolor="#DEBA84"
	autogeneratecolumns="False" font-size="Smaller">
	<footerstyle forecolor="#8C4510" backcolor="#F7DFB5"></footerstyle>
	<selecteditemstyle font-bold="True" backcolor="#738A9C"></selecteditemstyle>
	<edititemstyle backcolor="FloralWhite"></edititemstyle>
	<itemstyle forecolor="#8C4510" backcolor="#FFF7E7"></itemstyle>
	<headerstyle font-bold="True" forecolor="White" backcolor="#A55129"></headerstyle>
	<columns>
		<asp:templatecolumn headertext="Favoriten Markierungen">
			<itemtemplate>
				<asp:linkbutton id="LinkButton3" runat="server" font-bold="True" forecolor="Sienna" commandname="select">
					<%# DataBinder.Eval(Container, "DataItem.ShortCut") %>
				</asp:linkbutton>
			</itemtemplate>
			<edititemtemplate>
				<asp:TextBox id="ShortCutsTextBox" runat="server" width="200" Text='<%# DataBinder.Eval(Container, "DataItem.ShortCut") %>'>
				</asp:textbox>
			</edititemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="auto">
			<itemtemplate>
				<asp:CheckBox id="CheckBox2" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.auto") %>' Enabled="False">
				</asp:checkbox>
			</itemtemplate>
			<edititemtemplate>
				<asp:CheckBox id="AutoCheckBox" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.auto") %>'>
				</asp:checkbox>
			</edititemtemplate>
		</asp:templatecolumn>
		<asp:templatecolumn>
			<itemtemplate>
			</itemtemplate>
			<edititemtemplate>
				<asp:linkbutton id="LinkButton4" runat="server" text="speichern" commandname="Update"></asp:linkbutton>&nbsp;
				<asp:linkbutton runat="server" text="Löschen" commandname="del" causesvalidation="false" id="Linkbutton1"
					name="Linkbutton1"></asp:linkbutton>
			</edititemtemplate>
		</asp:templatecolumn>
	</columns>
	<pagerstyle horizontalalign="center" forecolor="#8C4510" mode="NumericPages"></pagerstyle>
</asp:datagrid>
<p>
	<asp:linkbutton id="NeuShortCutsButton" title="neue ShortCuts" runat="server" cssclass="BigButton"
		font-bold="True">neuer ShortCut</asp:linkbutton><br>
	<uc1:wortraumcontroller id="WortraumController1" runat="server" visible="False"></uc1:wortraumcontroller></p>
