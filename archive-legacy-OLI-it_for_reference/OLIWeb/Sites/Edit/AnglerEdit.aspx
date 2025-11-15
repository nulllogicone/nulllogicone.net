<%@ Page ValidateRequest="false" Language="c#" CodeBehind="AnglerEdit.aspx.cs" AutoEventWireup="True"
    Inherits="OliWeb.Sites.Edit.AnglerEdit" MasterPageFile="~/Site.Master" %>

<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="~/Controls/Koerper/StammKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AnglerOrgan" Src="~/Controls/Koerper/Organ/AnglerOrgan.ascx" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>AnglerEdit</title>
    <link href="../../OliWeb.css" type="text/css" rel="stylesheet">
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper>
    <uc1:AnglerOrgan ID="AnglerOrgan1" runat="server" Visible="false"></uc1:AnglerOrgan>
    <div class="rechtsobenrund">
    <table id="AnglerEditTable" class="Angler">
        <tr>
      
            <td >
                Filterprofil
            </td>
            <td >
                <asp:TextBox ID="AnglerTextBox" runat="server" ToolTip="geben sie einen Namen für dieses Filterprofil an"
                    Width="361px"></asp:TextBox>
            </td>
        </tr>
        <tr>
      
            <td >
                Beschreibung
            </td>
            <td >
                <asp:TextBox ID="BeschreibungTextBox" runat="server" Width="368px" Height="82px"
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
          
            <td >
            </td>
            <td >
                <asp:Image ID="Image1" runat="server" Width="25px" ImageUrl="../../images/icons/ok.gif"
                    BorderStyle="None"></asp:Image>
                <asp:Button ID="UpdateButton" runat="server" Text="update" CssClass="Button" OnClick="UpdateButton_Click">
                </asp:Button>&nbsp;
                <asp:Image ID="Image2" runat="server" Width="25px" ImageUrl="../../images/icons/cancel.gif"
                    BorderStyle="None"></asp:Image>
                <asp:Button ID="CancelButton" runat="server" Text="cancel" OnClick="CancelButton_Click">
                </asp:Button>
            </td>
        </tr>
    </table>
    </div>
    <asp:Panel ID="HilfePanel" runat="server" CssClass="rechtsobenrund Hilfe" EnableViewState="False">
        <span style="text-align: right"><a title="online dokumentation" href="http://doku.oli-it.com/Oberflaeche/stamm.htm#edit"
            target="doku">
            <img class="fragezeichen" style="border-right: 0px; border-top: 0px; border-left: 0px;
                border-bottom: 0px" alt="weitere Hilfe" src="../../images/icons/fragezeichen.gif"
                align="right">
        </a></span>
        <h2>
            Filterprofil&nbsp;editieren</h2>
        <p>
            Bearbeten Sie den Namen und die Beschreibung dieses Filterprofiles.</p>
    </asp:Panel>
</asp:Content>
