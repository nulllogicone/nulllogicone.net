<%@ Control Language="c#" AutoEventWireup="True" Codebehind="BuntePunkte.ascx.cs" Inherits="OliWeb.Controls.Wortraum.BuntePunkte"  %>
<asp:ImageButton id="OlisImageButton" runat="server" ImageAlign="Middle" borderwidth="2px" BorderColor="White" CssClass="BuntePunkt" onclick="PunktImageButton_Click"></asp:ImageButton>
<asp:ImageButton id="GetImageButton" runat="server" ImageAlign="Middle" borderwidth="2px" BorderColor="White" CssClass="BuntePunkt" onclick="PunktImageButton_Click"></asp:ImageButton>
<asp:ImageButton id="IlosImageButton" runat="server" ImageAlign="Middle" borderwidth="2px" BorderColor="White" CssClass="BuntePunkt" onclick="PunktImageButton_Click"></asp:ImageButton>
<asp:ImageButton id="FitImageButton" runat="server" ImageAlign="Middle" borderwidth="2px" BorderColor="White" CssClass="BuntePunkt" onclick="PunktImageButton_Click"></asp:ImageButton>
<asp:ListBox id="SelectListBox" runat="server" AutoPostBack="True" Rows="5" onselectedindexchanged="SelectListBox_SelectedIndexChanged"></asp:ListBox>
