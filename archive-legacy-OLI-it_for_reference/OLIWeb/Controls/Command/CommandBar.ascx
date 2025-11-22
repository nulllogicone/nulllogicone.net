<%@ Register TagPrefix="uc1" TagName="StammTopLabCommand" Src="GetCommand/DetailCommand/StammTopLabCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammPostItCommand" Src="GetCommand/DetailCommand/StammPostItCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammAnglerCommand" Src="GetCommand/DetailCommand/StammAnglerCommand.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="CommandBar.ascx.cs" Inherits="OliWeb.Controls.Command.CommandBar"  %>
<div style="PADDING-LEFT: 10px">
	<uc1:StammAnglerCommand id="StammAnglerCommand1" CssClass="TableButton" runat="server"></uc1:StammAnglerCommand>
	<uc1:StammPostItCommand id="StammPostItCommand1" CssClass="TableButton"  runat="server"></uc1:StammPostItCommand>
	<uc1:StammTopLabCommand id="StammTopLabCommand1" CssClass="TableButton"  runat="server"></uc1:StammTopLabCommand>
</div>
