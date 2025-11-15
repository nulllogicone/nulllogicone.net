<%@ Page Language="c#" CodeBehind="BildUpload.aspx.cs" AutoEventWireup="True"
    MasterPageFile="~/Site.Master" Inherits="OliWeb.Sites.Elemente.BildUpload" Description="Image upload" %>
<%@ Import Namespace="Azure.Storage.Blobs.Models" %>
<%@ Import Namespace="Azure.Storage.Blobs" %>


<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="~/Controls/Koerper/StammKoerper.ascx" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>OLI-it: Antworten von
        <% = MyTitle %>
    </title>
    <meta name="keywords" content="Antwort, feedback, kommentar" />
    <meta name="Description" content="Antworten, die dieser Stamm geschrieben hat" />
    <link rel="alternate" type="application/rdf+xml" title="RDF Version" href="<% = StammRdfLink %>" />
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
                <uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper>
                <br/>
                <table id="BildUploadTable" cellspacing="1" cellpadding="1" width="535" border="1">
                    <tr>
                        <td valign="top" bgcolor="whitesmoke">
                            <h3>
                                <asp:Image ID="Image1" runat="server" ImageUrl="../../images/icons/ordner.gif"></asp:Image>mein
                                Bilderordner
                                <asp:Label ID="OrdnerLabel" runat="server"></asp:Label>
                            </h3>
                            <p>
                                &nbsp;&nbsp;
                                <asp:DataGrid ID="BilderDataGrid" runat="server" ShowHeader="False" AutoGenerateColumns="False"
                                              Width="95%" BackColor="White">
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <img src="<%#  BlobContainer.GetBlobClient(((BlobItem)Container.DataItem).Name).Uri.AbsoluteUri %>" width="100">
                                                <asp:Label ID="DateiName" Text="<%# ((BlobItem)Container.DataItem).Name.Substring(37) %>" CssClass="id"
                                                           runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" Text="Löschen" CommandName="Delete" CausesValidation="false"
                                                                CssClass="Button" ID="Linkbutton1" name="Linkbutton1">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </p>
                            <p>
                                &nbsp;
                            </p>
                        </td>
                        <td valign="top" bgcolor="gainsboro">
                            <h3>
                                <br/>
                                neues Bild auf OLI-it Server laden:
                            </h3>
                            <p align="right">
                                <input id="File1" type="file" size="20" name="File1" runat="server"/>
                                <br/>
                                <asp:Image ID="Image2" runat="server" ImageUrl="../../images/icons/ok.gif" Width="25px"
                                           BorderStyle="None">
                                </asp:Image>
                                <input id="Button1" type="button" value="Uploaden" name="Button1" runat="server"
                                       class="Button" onserverclick="Button1_ServerClick"/>
                            </p>
                            <p align="right">
                                <asp:Image ID="Image3" runat="server" ImageUrl="../../images/icons/cancel.gif" Width="25px"
                                           BorderStyle="None">
                                </asp:Image>
                                <asp:HyperLink ID="CancelHyperLink" runat="server" CssClass="cancelbutton" NavigateUrl="../StammSite.aspx">abbrechen</asp:HyperLink>
                            </p>
                            <p align="left">
                                <font size="1"></font>&nbsp;
                            </p>
                            <p align="left">
                                <font size="1">
                                    Wählen sie über den <strong>Durchsuchen</strong> Button eine Bilddatei
                                    (bmp, jpg, gif)
                                    <br/>
                                    von ihrer Festplatte und klicken anschließend auf <strong>Uploaden</strong>.
                                </font>
                            </p>
                            <p align="left">
                                <font size="1">
                                    Auf der linken Seite sehen sie ihre Dateien mit Namen und können einzelne
                                    <strong>löschen</strong>
                                </font><font size="1">
                                    (bachten sie, daß in Nachrichten und
                                    Antworten die diese Bildverweise noch&nbsp;verwenden, ein rotes <font color="red">X</font>
                                    erscheint).
                                </font>
                            </p>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="HilfePanel" runat="server" CssClass="rechtsobenrund Hilfe" EnableViewState="False"
                           Width="600">
                    <div>
                        <span style="text-align: right"><a title="online dokumentation" href="#" target="doku">
                                                                    <img class="fragezeichen" style="border-bottom: 0px; border-left: 0px; border-right: 0px; border-top: 0px;" alt="weitere Hilfe" src="../../images/icons/fragezeichen.gif"
                                                                         align="right">
                                                                </a></span>
                        &nbsp;&nbsp;&nbsp;&nbsp;<h4>
                            Bilderverwaltung
                        </h4>
                        <p>
                            Hier können Sie ihre Bilder verwalten
                        </p>
                    </div>
                </asp:Panel>
</asp:Content>