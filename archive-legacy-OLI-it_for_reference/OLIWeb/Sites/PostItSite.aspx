<%@ Page Language="c#" CodeBehind="PostItSite.aspx.cs" AutoEventWireup="True" Inherits="OliWeb.Sites.PostItSite"
    EnableViewState="False" MasterPageFile="~/Site.Master" Description="Eine Frage, Nachricht, Angebot," %>

<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="~/Controls/Koerper/StammKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PostItKoerper" Src="~/Controls/Koerper/PostItKoerper.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>OLI-it:[P]
        <% = MyTitle %>
    </title>
    <link rel="alternate" type="application/rdf+xml" title="RDF Version" href="<% = PostItRdfLink %>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper>
    <uc1:PostItKoerper ID="PostItKoerper1" runat="server"></uc1:PostItKoerper>
<%--	<br/>
	SinglePostItPage: <asp:HyperLink ID="HyperlinkToSinglePage" runat="server" />--%>
    <asp:Panel ID="HilfePanel" runat="server" CssClass="rechtsobenrund Hilfe" >
        <h4>
            <span style="text-align: right"><a title="online dokumentation" href="http://doku.oli-it.com/Oberflaeche/Nachricht.htm"
                target="doku">
                <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                    alt="weitere Hilfe" src="../images/icons/fragezeichen.gif" align="right">
            </a></span>Nachricht
            <%--Antworten Link--%>
            <asp:HyperLink ID="AntwortenHyperLink" runat="server" CssClass="TopLab" NavigateUrl="#"
                Visible="False"></asp:HyperLink>
            <%--RDF Metadata Link--%>
            <asp:HyperLink ID="RdfHyperLink" runat="server" ImageUrl="~/images/rdf_klein.jpg"
                ToolTip="RDF Metadaten zu dieser Nachricht"></asp:HyperLink><span class="id">.rdf</span>
        </h4>
            <asp:Localize meta:resourcekey="PostItHelpDescription" runat="server"></asp:Localize>
        <h2>
            <asp:Label ID="KontoLabel" runat="server" Visible="False">Konto</asp:Label></h2>
        <asp:DataGrid ID="KontoDataGrid" runat="server" Font-Size="XX-Small" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundColumn DataField="Datum" ReadOnly="True" HeaderText="Datum"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="Kommentar">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="KommentarLabel" Text='<%# Eval("Kommentar") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="Betrag" ReadOnly="True" HeaderText="Betrag" DataFormatString="{0:0.00}">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="Stamm">
                    <ItemTemplate>
                        <asp:HyperLink runat="server" ID="Hyperlink1" NavigateUrl='<%# "../S/" + Eval("StammGuid").ToString() + ".aspx" %>'
                            Text='Stamm' Visible='<%# Eval("StammGuid").ToString().Length > 0 %>'>
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Antwort">
                    <ItemTemplate>
                        <asp:HyperLink runat="server" ID="Hyperlink2" NavigateUrl='<%# "../T/" + Eval("TopLabGuid").ToString() + ".aspx" %>'
                            Text='Antwort' Visible='<%# Eval("TopLabGuid").ToString().Length > 0 %>'>
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid><br />
        Summe:
        <asp:Label ID="SummeLabel" runat="server" CssClass="flowKooK"></asp:Label>
    </asp:Panel>
</asp:Content>
