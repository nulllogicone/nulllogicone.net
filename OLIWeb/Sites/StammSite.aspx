<%@ Page Language="c#" CodeBehind="StammSite.aspx.cs" AutoEventWireup="True" Inherits="OliWeb.Sites.StammSite"
    EnableViewState="False" MasterPageFile="~/Site.Master" EnableViewStateMac="False"
    Description="Die persönliche Startseite eines Stammes" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="~/Controls/Koerper/StammKoerper.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>OLI-it:[S]
        <% = MyTitle %>
    </title>
    <link rel="alternate" type="application/rdf+xml" title="RDF Version" href="<% = StammRdfLink %>" />
    <script type="text/javascript">
        function toggleKonto() {
            $("#DivKontoGrid").toggle("slow");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper>
    <div class="rechtsobenrund Hilfe" style="background-color:white;">
        <span style="text-align: right">
            <img style="border-right: 0; border-top: 0px; border-left: 0px; border-bottom: 0px"
                alt="weitere Hilfe" src="../images/icons/fragezeichen.gif" align="right" /></span>
        <h2>
            <asp:Label Text="Startseite von" runat="server" meta:resourcekey="LabelH2" />
            <asp:HyperLink ID="StammHyperLink" runat="server" CssClass="Stamm" meta:resourcekey="StammHyperLinkResource1" Text="[StammHyperLink]"></asp:HyperLink>
            <asp:HyperLink ID="RdfHyperLink" runat="server" ToolTip="RDF Metadaten zu diesem Stamm" meta:resourcekey="RdfHyperLinkResource1">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/rdf_klein.jpg" meta:resourcekey="Image1Resource1" />


                <span style="font-weight: normal; font-size: 10px;">.rdf</span>
            </asp:HyperLink>
        </h2>
        <br />
        <asp:Panel ID="KennwortPanel" runat="server" meta:resourcekey="KennwortPanelResource1">
            <p>
                <asp:LinkButton ID="KennwortVergessenLinkButton" runat="server" Font-Bold="True"
                    OnClick="KennwortVergessenLinkButton_Click" meta:resourcekey="KennwortVergessenLinkButtonResource1" Text="Kennwort vergessen"></asp:LinkButton>?&nbsp;
            </p>
            <p>
                <asp:Localize meta:resourcekey="AnonymUserOptionsParagraph" runat="server">anonym can do...</asp:Localize>
            </p>
            <p>
                <asp:Localize meta:resourcekey="LoginUserOptionsParagraph" runat="server">loged in can do...</asp:Localize>
            </p>
        </asp:Panel>
        <asp:Panel ID="EingeloggtPanel" runat="server" meta:resourcekey="EingeloggtPanelResource1">

                <strong><asp:Localize meta:resourcekey="EingeloggtPanelParagraph" runat="server">Du bist eingeloggt</asp:Localize></strong> und kannst
            <asp:LinkButton ID="BilderLinkButton" runat="server" Font-Bold="True" OnClick="BilderLinkButton_Click" meta:resourcekey="BilderLinkButtonResource1" Text="Bilderordner"></asp:LinkButton>&nbsp;editieren.
             <p>
                Cool wenn du &nbsp;
                <asp:LinkButton ID="FreundLinkButton" runat="server" Font-Bold="True" OnClick="FreundLinkButton_Click" meta:resourcekey="FreundLinkButtonResource1" Text="Freunde wirbst :-) "></asp:LinkButton>!&nbsp;Für
                jeden neu geworbenen
                <br />
                Stamm werden Dir
                <asp:Label ID="Label1" runat="server" CssClass="boundKooK" meta:resourcekey="Label1Resource1" Text="10,00"></asp:Label>&nbsp;KooK
                gutgeschrieben.
            </p>
            <h4 onclick="toggleKonto()" style="cursor: pointer; text-decoration: underline;">
                <asp:Label ID="KontoLabel" runat="server" Visible="False" meta:resourcekey="KontoLabelResource1" Text="Konto "></asp:Label>
                Summe:
                <asp:Label ID="SummeLabel" runat="server" CssClass="boundKooK" meta:resourcekey="SummeLabelResource1"></asp:Label></h4>
            <div id="DivKontoGrid" style="display: none;">
                <asp:DataGrid ID="KontoDataGrid" runat="server" Font-Size="XX-Small" AutoGenerateColumns="False" meta:resourcekey="KontoDataGridResource1">
                    <Columns>
                        <asp:BoundColumn DataField="Datum" ReadOnly="True" HeaderText="Datum"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="Kommentar">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="KommentarLabel" Text='<%# Eval("Kommentar") %>' meta:resourcekey="KommentarLabelResource1"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="Betrag" ReadOnly="True" HeaderText="Betrag" DataFormatString="{0:0.00}">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="Nachrichten">
                            <ItemTemplate>
                                <asp:HyperLink runat="server" ID="Hyperlink1" NavigateUrl='<%# "../P/" + Eval("PostItGuid") + ".aspx" %>'
                                    Text='Nachricht' Visible='<%# Eval("PostItGuid").ToString().Length > 0 %>' meta:resourcekey="Hyperlink1Resource1"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid><br />
            </div>
        </asp:Panel>
    </div>
</asp:Content>
