<%@ Register TagPrefix="uc1" TagName="TollChart" Src="~/Controls/Gimicks/TollChart.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="TollisOrgan.ascx.cs" Inherits="OliWeb.Controls.Koerper.Organ.TollisOrgan" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<p>Bewertung (0-100)
	<asp:textbox id="TollTextBox" runat="server" width="55px"></asp:textbox>&nbsp;
	<uc1:tollchart id="TollChart1" runat="server"></uc1:tollchart><br>
	Kommentar
	<asp:textbox id="TollTextTextBox" runat="server" height="72px" textmode="MultiLine"></asp:textbox>&nbsp;
	<asp:image id="Image1" runat="server" width="25px" imageurl="../../../images/icons/ok.gif"
		borderstyle="None"></asp:image>
	<asp:button id="AbgebenButton" runat="server" cssclass="Button" text="abgeben"></asp:button><br>
</p>
