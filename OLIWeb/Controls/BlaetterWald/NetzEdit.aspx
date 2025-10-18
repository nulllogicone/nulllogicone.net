<%@ Page language="c#" Codebehind="NetzEdit.aspx.cs" AutoEventWireup="false" Inherits="OliWeb.Controls.BlaetterWald.NetzEdit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>NetzEdit</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="BlaetterWald.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bgColor="#dcdcdc" MS_POSITIONING="FlowLayout">
		<form id="NetzEdit" method="post" runat="server">
			<P class="netz">
				<TABLE id="Table1" cellSpacing="0" cellPadding="10" width="100%" border="0" bgColor="whitesmoke">
					<TR>
						<TD style="WIDTH: 115px"><STRONG>Netz</STRONG></TD>
						<TD><asp:textbox id="NetzTextBox" runat="server" Width="100%" CssClass="Netz" Font-Bold="True"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 115px">Beschreibung</TD>
						<TD>
							<P><asp:textbox id="BeschreibungTextBox" runat="server" Width="100%" TextMode="MultiLine"></asp:textbox></P>
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 115px">Bild</TD>
						<TD><asp:textbox id="BildTextBox" runat="server" Width="100%"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 115px">
							<P>&nbsp;</P>
						</TD>
						<TD>
							<P align="right">&nbsp;<asp:button id="UpdateButton" runat="server" Text="update"></asp:button>&nbsp;
								<asp:Button id="CancelButton" runat="server" Text="cancel"></asp:Button></P>
						</TD>
					</TR>
				</TABLE>
			</P>
			<P><asp:datagrid id="KnotenDataGrid" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="10">
					<EditItemStyle BackColor="WhiteSmoke"></EditItemStyle>
					<HeaderStyle Font-Bold="True" HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
					<Columns>
						<asp:TemplateColumn Visible="False">
							<ItemTemplate>
								<asp:Label id="KnotenGuidLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.KnotenGuid") %>'>
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Knoten / Beschreibung">
							<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id=Label1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Knoten") %>' CssClass="Knoten" Font-Bold="True">
								</asp:Label><BR>
								<asp:Label id=Label3 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.KnotenBeschreibung") %>' CssClass="kommentar" NAME="Label2">
								</asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id=KnotenTextBox runat="server" Width="100%" Text='<%# DataBinder.Eval(Container, "DataItem.Knoten") %>' CssClass="Knoten" Font-Bold="True">
								</asp:TextBox>
								<asp:TextBox id=KnotenBeschreibungTextbox runat="server" Width="100%" TextMode="MultiLine" Text='<%# DataBinder.Eval(Container, "DataItem.KnotenBeschreibung") %>' CssClass="Kommentar">
								</asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Vorgabe / weiter">
							<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<asp:Image id=OlisImage runat="server" ImageUrl='<%# OliEngine.OliCommon.PunkteOrdner + "/OLIs/" + DataBinder.Eval(Container, "DataItem.VgbOlis") + ".gif" %>'>
								</asp:Image>
								<asp:Image id=GetImage runat="server" ImageUrl='<%# OliEngine.OliCommon.PunkteOrdner + "/OLIs/" + DataBinder.Eval(Container, "DataItem.VgbGet") + ".gif" %>'>
								</asp:Image>
								<asp:Image id=IlosImage runat="server" ImageUrl='<%# OliEngine.OliCommon.PunkteOrdner + "/ILOs/" + DataBinder.Eval(Container, "DataItem.VgbIlos") + ".gif" %>'>
								</asp:Image>
								<asp:Image id=FitImage runat="server" ImageUrl='<%# OliEngine.OliCommon.PunkteOrdner + "/ILOs/" + DataBinder.Eval(Container, "DataItem.VgbFit") + ".gif" %>'>
								</asp:Image><br>
								weiter ...
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id=VgbOlisTextBox runat="server" Width="20" Text='<%# DataBinder.Eval(Container, "DataItem.VgbOlis") %>'>
								</asp:TextBox>
								<asp:TextBox id=VgbGetTextBox runat="server" Width="20" Text='<%# DataBinder.Eval(Container, "DataItem.VgbGet") %>'>
								</asp:TextBox>
								<asp:TextBox id=VgbIlosTextBox runat="server" Width="20" Text='<%# DataBinder.Eval(Container, "DataItem.VgbIlos") %>'>
								</asp:TextBox>
								<asp:TextBox id=VgbFitTextBox runat="server" Width="20" Text='<%# DataBinder.Eval(Container, "DataItem.VgbFit") %>'>
								</asp:TextBox><br>
								<asp:DropDownList id="DropDownList1" runat="server" CssClass="Netz"></asp:DropDownList>
								<asp:DropDownList id="DropDownList2" runat="server" CssClass="Baum"></asp:DropDownList>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<asp:Button id="Button1" runat="server" Text="edit" CausesValidation="false" CommandName="Edit"></asp:Button>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:Button id="Button3" runat="server" Text="update" CommandName="Update"></asp:Button>&nbsp;
								<asp:Button id="Button2" runat="server" Text="cancel" CausesValidation="false" CommandName="Cancel"></asp:Button>
							</EditItemTemplate>
						</asp:TemplateColumn>
					</Columns>
				</asp:datagrid></P>
		</form>
	</body>
</HTML>
