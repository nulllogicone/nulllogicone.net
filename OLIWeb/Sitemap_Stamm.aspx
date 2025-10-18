<%@ Page language="c#" Codebehind="Sitemap_Stamm.aspx.cs" AutoEventWireup="True" Inherits="OliWeb.Sitemap_Stamm" contentType="text/xml" %><?xml version="1.0" encoding="UTF-8" ?>
<urlset xmlns="http://www.google.com/schemas/sitemap/0.84">
<asp:repeater id="Repeater" runat="server">
<itemtemplate>
<url>
<loc>https://www.oli-it.com/S/<%# Eval("StammGuid").ToString() %>.aspx</loc>
<priority>0.2</priority>
</url>
</itemtemplate>
</asp:repeater>
</urlset>
