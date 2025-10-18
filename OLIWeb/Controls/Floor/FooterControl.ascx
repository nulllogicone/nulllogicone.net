<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FooterControl.ascx.cs" Inherits="OliWeb.Controls.Floor.FooterControl" %>
<asp:HyperLink ID="HomeLink" NavigateUrl="~/default.aspx" runat="server" ImageUrl="~/images/oli-it_36.jpg"></asp:HyperLink>
&copy; 1994 -
<asp:Label ID="LabelNow" runat="server"></asp:Label>
|  
<asp:HyperLink ID="CopyrighHyperlink" NavigateUrl="~/nutzungsbedingungen.aspx" Text="copyright" runat="server" />
| 
<asp:HyperLink ID="ImpressumHyperlink" NavigateUrl="~/Impressum.aspx" Text="imprint" runat="server"></asp:HyperLink>
| 
comments:
<asp:HyperLink ID="MailtoHperlink" NavigateUrl="mailto:info@oli-it.com" Text="info@oli-it.com" runat="server"></asp:HyperLink>
<br />
<asp:Label runat="server" id="LabelVersion"></asp:Label> 
<br />
<br />
<div style="float: right;">
</div>
<br />
